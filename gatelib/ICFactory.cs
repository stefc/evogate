using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace stefc.gatelib
{

    public static class ICFactory {

        public static IC CreateFromJson(string json) 
        {
            var root = JsonConvert.DeserializeObject<JObject>(json);

            string name = root.Value<string>(nameof(name));
            int input = root.Value<int>(nameof(input));
            int output = root.Value<int>(nameof(output));
            int gates = root.Value<int>(nameof(gates));

            IEnumerable<JObject> wiring = root
                .Value<JArray>(nameof(wiring))
                .Where( token => token.Type == JTokenType.Object)
                .Select( token => token.Value<JObject>())
                .ToList();

            var nodes = wiring.Aggregate(ImmutableList<Node<Gate>>.Empty, 
                (accu,current) => accu.Add(new Node<Gate>(CreateGateFrom(current))));

            var nodeLookUp = nodes.ToDictionary( gate => gate.Value.Id);

            var edges = wiring
                .Where( x => x.ContainsKey("edges"))
                .SelectMany(
                    x => x.Value<JArray>("edges"),
                    (x, xs) => new { 
                        From = nodeLookUp[x.Value<string>("id")].Value, 
                        To = nodeLookUp[xs.Value<string>()].Value }
                )
                .GroupBy( x => x.From, x => x.To );

            var graph = edges.Aggregate( new Graph<Gate>(nodes), 
                (accu,current) => 
                    accu.AddDirectedEdges( current.Key, current ) );

            return new IC(name, input, output, gates, graph);
        }

        private static Gate CreateGateFrom(JObject json) {
            string id = json.Value<string>(nameof(id));
            string op = json.Value<string>(nameof(op));

            GateOp gateOp = op == null ? GateOp.None : (GateOp) Enum.Parse(typeof(GateOp), op);

            return new Gate(id, gateOp);
        }
    }
}