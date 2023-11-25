using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace stefc.gatelib;

public class Graph<T>(ImmutableList<Node<T>> nodeSet) : IEnumerable<T> 
    where T : IEquatable<T> 
{
    private readonly ImmutableList<Node<T>> nodeSet = nodeSet ?? [];

    public Graph() : this([]) {}

    // adds a node to the graph
    public Graph<T> AddNode(Node<T> node) => new(this.nodeSet.Add(node));

    // adds a node to the graph
    public Graph<T> AddNode(T value) => new(nodeSet.Add(new Node<T>(value)));

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

    public IEnumerator<T> GetEnumerator() => new NodeEnumerator(nodeSet.GetEnumerator());

    IEnumerator IEnumerable.GetEnumerator() => nodeSet.GetEnumerator();

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

    private class NodeEnumerator(IEnumerator<Node<T>> enumerator) : IEnumerator<T> 
    {            
        public bool MoveNext() => enumerator.MoveNext();

        public void Reset() => enumerator.Reset();

        public void Dispose() => enumerator.Dispose();

        object IEnumerator.Current => Current;

        public T Current => enumerator.Current.Value;
    }

}

public class Node<T>(T data, ImmutableList<T> neighbors) where T : IEquatable<T>
{
    // Private member-variables
    
    public Node(T data) : this(data, ImmutableList<T>.Empty) {}

    public T Value => data;

    public ImmutableList<T> Neighbors => neighbors;

    public Node<T> AddNeighbor(T value) => new Node<T>(data, neighbors.Add(value));
    public Node<T> AddNeighbors(IEnumerable<T> values) => new(data, neighbors.AddRange(values));
}