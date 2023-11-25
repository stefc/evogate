using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text.Json;

namespace stefc.gatelib
{
    public static class ICFactory
    {


        public class JNode
        {
            public required string Id { get; set; }
            #nullable enable
            public string? Op { get; set; }
            #nullable disable
            public List<string> Edges { get; init; } = [];
        }

        public class JGraph
        {
            public required string Name { get; set; }
            public int Input { get; set; }
            public int Output { get; set; }
            public int Gates { get; set; }
            public List<JNode> Wiring { get; init; } = [];
        }

        public static IC CreateFromJson(string json)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            var graph = JsonSerializer.Deserialize<JGraph>(json, options);
         
            var nodes = graph.Wiring.Aggregate(ImmutableList<Node<Gate>>.Empty,
                (accu, current) => accu.Add(new Node<Gate>(CreateGateFrom(current))));

            var nodeLookUp = nodes.ToDictionary(gate => gate.Value.Id);

            var edges = graph.Wiring
                .SelectMany(
                    x => x.Edges,
                    (x, xs) => new
                    {
                        From = nodeLookUp[x.Id].Value,
                        To = nodeLookUp[xs].Value
                    }
                )
                .GroupBy(x => x.From, x => x.To);

            var wiring = edges.Aggregate(new Graph<Gate>(nodes),
                (accu, current) => accu.AddDirectedEdges(current.Key!, current!));

            return new IC(graph.Name, graph.Input, graph.Output, graph.Gates, wiring);
        }

        private static Gate CreateGateFrom(JNode json)
        {
            GateOp gateOp = json.Op == null ? GateOp.None : (GateOp)Enum.Parse(typeof(GateOp), json.Op);
            return new Gate(json.Id, gateOp);
        }
    }
}