using System;
using System.Collections.Generic;
using UnityEngine;
using CustomBehaviour;

[Serializable]
public class Edge
{
    public Node Start, End;
    public double Weight;
    public string Name;
    public Edge(Node Start, Node End)
    {
        this.Start = Start;
        this.End = End;
        Weight = (End.Position - Start.Position).magnitude;
        Name = "Start: " + Start.name + " End: " + End.name;
    }
}

[Serializable]
public class Graph
{
    public List<Node> VertexSet;
    public List<Edge> EdgeSet;

    public int VertexCount
    {
        get { return VertexSet.Count; }
    }
    public int EdgeCount
    {
        get { return EdgeSet.Count; }
    }

    public Graph() { }

    public Graph(List<Node> VertexSet)
    {
        this.VertexSet = VertexSet;
        EdgeSet = new List<Edge>();
    }

    public Graph(List<Edge> EdgeSet)
    {
        this.EdgeSet = EdgeSet;

        //Initializing the vertex set
        foreach (Edge edge in EdgeSet)
        {
            if (!VertexSet.Contains(edge.Start))
            {
                VertexSet.Add(edge.Start);
            }
            if (!VertexSet.Contains(edge.End))
            {
                VertexSet.Add(edge.End);
            }
        }
    }
    public void AddEdge(Node start, Node end)
    {
        if (VertexSet.Contains(start) && VertexSet.Contains(end))
        {
            start.AddAdjecentNode(end);
            end.AddAdjecentNode(start);
            //We have to chech if EdgeSet contains the end,start edge
            Edge edge = new Edge(start, end);
            EdgeSet.Add(edge);
        }
    }
    public void PrintVertexSet()
    {
        foreach (var item in VertexSet)
        {
            Debug.Log("Node number: " + item.name);
        }
    }
    public void PrintEdgeSet()
    {
        foreach (var item in EdgeSet)
        {
            Debug.Log(item.Name);
        }
    }
}
//1. Click and drag to create an graph edge OK!
//2. Show all edges on screen as lines maybe on canvas ?
//3. Calculate the shortest path Dijkstra
public class NodeManager : SingletonBehaviour<NodeManager>
{
    public Camera TagetCamera;

    private Graph graph;

    //Variavbles for adding a new edge
    private Node startNode;
    private Node endNode;

    public Graph Graph
    {
        get { return graph; }

        set { graph = value; }
    }

    public Node StartNode
    {
        get { return startNode; }

        set { startNode = value; }
    }

    public Node EndNode
    {
        get { return endNode; }

        set { endNode = value; }
    }

    private void Start()
    {
        Node[] nodes = FindObjectsOfType<Node>();
        List<Node> NodesList = new List<Node>();
        NodesList.AddRange(nodes);

        //Graph initialization
        Graph = new Graph(NodesList);
    }

    private void Update()
    {
        if (StartNode != null && EndNode != null)
        {
            if (StartNode != EndNode)
            {
                Graph.AddEdge(StartNode, EndNode);
                StartNode.AddAdjecentNode(EndNode);
                EndNode.AddAdjecentNode(StartNode);
            }
            //Clear the references
            StartNode.DefaultMaterial();
            EndNode.DefaultMaterial();
            StartNode = null;
            EndNode = null;
        }
    }
}
