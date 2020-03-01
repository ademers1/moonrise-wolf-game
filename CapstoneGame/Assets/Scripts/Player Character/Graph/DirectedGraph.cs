using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class DirectedGraph<T> where T : class
{
    private readonly List<T> _nodes = new List<T>(); //the nodes
    private Int32 _e; //the number of edges;
    private readonly LinkedList<T>[] adj; //Use a LinkedList for the adjacency-list reperesentation

    public DirectedGraph(List<T> nodes){
        if(nodes.Count > 0)
        {
            //create the list of nodes
            _nodes = nodes;
            this._e = 0;
            //Create a new adjacency-list for each vertex
            adj = new LinkedList<T>[nodes.Count];
            for (Int32 n = 0; n < nodes.Count; n++){
                adj[n] = new LinkedList<T>();
            }
        }
    }

    public IEnumerable<T> GetNodes()
    {
        return _nodes;
    }

    public Int32 GetEdges()
    {
        return _e;
    }

    public void AddEdge(T start, T end)
    {
        if (_nodes.Contains(start) && _nodes.Contains(end))
        {
            adj[_nodes.IndexOf(start)].AddFirst(end);
            _e++;
        }
    }

    /*
     * Return the adjacency-list for the vertex v, 
     * which are the vertices connected to v pointing from v
     */
    public IEnumerable<T> Adj(T n)
    {
        if (!_nodes.Contains(n)) throw new Exception();
        return adj[_nodes.IndexOf(n)];       
    }
}