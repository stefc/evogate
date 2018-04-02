using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;


namespace stefc.gatelib
{
    public class Graph<T> : IEnumerable<T> 
        where T : IEquatable<T> 
    {
        private readonly ImmutableList<Node<T>> nodeSet;

        public Graph() : this(ImmutableList<Node<T>>.Empty) {}
        public Graph(ImmutableList<Node<T>> nodeSet) => this.nodeSet = nodeSet ?? ImmutableList<Node<T>>.Empty;

        // adds a node to the graph
         public Graph<T> AddNode(Node<T> node) => new Graph<T>(this.nodeSet.Add(node));

        // adds a node to the graph
        public Graph<T> AddNode(T value) => new Graph<T>(nodeSet.Add(new Node<T>(value)));

        public Graph<T> AddDirectedEdge(T from, T to)
        {
            var fromNode = nodeSet.First(x => x.Value.Equals(from));
            var toNode = nodeSet.First(x => x.Value.Equals(to)).Value;

            return new Graph<T>(nodeSet.Remove(fromNode).Add(fromNode.AddNeighbor(toNode)));
        }
        public Graph<T> AddDirectedEdges(T from, IEnumerable<T> to)
        {
            var fromNode = nodeSet.First(x => x.Value.Equals(from));
            var toNodes = to.Select( t => nodeSet.First(x => x.Value.Equals(t)).Value);

            return new Graph<T>(nodeSet.Remove(fromNode).Add(fromNode.AddNeighbors(toNodes)));
        }

        public bool Contains(T value) => nodeSet.FirstOrDefault(x => x.Value.Equals(value)) != null;

        public IEnumerator<T> GetEnumerator()
        {
            return new NodeEnumerator(nodeSet.GetEnumerator());
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return nodeSet.GetEnumerator();
        }

        public ImmutableList<Node<T>> Nodes => nodeSet;

        // public bool Remove(T value)
        // {
        //     // first remove the node from the nodeset
        //     Node<T> nodeToRemove = nodeSet.FirstOrDefault(x => x.Value == value);
        //     if (nodeToRemove == null)
        //         // node wasn't found
        //         return false;

        //     // otherwise, the node was found
        //     nodeSet.Remove(nodeToRemove);

        //     // enumerate through each node in the nodeSet, removing edges to this node
        //     foreach (Node<T> gnode in nodeSet)
        //     {
        //         int index = gnode.Neighbors.IndexOf(nodeToRemove);
        //         if (index != -1)
        //         {
        //             // remove the reference to the node and associated cost
        //             gnode.Neighbors.RemoveAt(index);
        //             gnode.Costs.RemoveAt(index);
        //         }
        //     }

        //     return true;
        // }


        public int Count => nodeSet.Count;

        private class NodeEnumerator : IEnumerator<T> 
        {
            private readonly IEnumerator<Node<T>> enumerator;
            public NodeEnumerator(IEnumerator<Node<T>> enumerator)
            {
                this.enumerator = enumerator;
            }

            public bool MoveNext()
            {
                return enumerator.MoveNext();
            }

            public void Reset()
            {
                enumerator.Reset();
            }

            public void Dispose()
            {
                enumerator.Dispose();
            }

            object IEnumerator.Current
            {
                get
                {
                    return Current;
                }
            }

            public T Current
            {
                get
                {
                    return enumerator.Current.Value;
                }
            }
        }

    }

    public class Node<T> where T : IEquatable<T>
    {
        // Private member-variables
        private readonly T data;
        private readonly ImmutableList<T> neighbors = ImmutableList<T>.Empty;

        public Node(T data) : this(data, ImmutableList<T>.Empty) {}
        public Node(T data, ImmutableList<T> neighbors)
        {
            this.data = data;
            this.neighbors = neighbors;
        }

        public T Value => this.data;

        public ImmutableList<T> Neighbors => this.neighbors;


        public Node<T> AddNeighbor(T value) {
            return new Node<T>(this.data, this.neighbors.Add(value));
        }
        public Node<T> AddNeighbors(IEnumerable<T> values) {
            return new Node<T>(this.data, this.neighbors.AddRange(values));
        }
    }
}