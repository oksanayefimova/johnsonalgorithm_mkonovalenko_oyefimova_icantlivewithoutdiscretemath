namespace icantlivewithoutdiscretemath;

using System;
using System.Collections.Generic;
using System.Diagnostics;

public class Graph
{
    public int vertices;
    public List<(int, int)>[] AdjacencyList;
    public int[,] adjacencyMatrix;
    public bool useAdjacencyList;

    public Graph(int vertices, bool useList = true)
    {
        this.vertices = vertices;
        useAdjacencyList = useList;
        if (useAdjacencyList)
        {
            AdjacencyList = new List<(int, int)>[vertices];
            for (int i = 0; i < vertices; i++)
                AdjacencyList[i] = new List<(int, int)>();
        }
        else
        {
            adjacencyMatrix = new int[vertices, vertices];
            for (int i = 0; i < vertices; i++)
            for (int j = 0; j < vertices; j++)
            {
                adjacencyMatrix[i, j] = int.MaxValue / 2;
            }
        }
    }

    public void AddEdge(int u, int v, int weight)
    {
        if (u == v) return;
        if (useAdjacencyList)
            AdjacencyList[u].Add((v, weight));
        else
            adjacencyMatrix[u, v] = weight;
    }

    public void ConvertToMatrix()
    {
        adjacencyMatrix = new int[vertices, vertices];
        for (int i = 0; i < vertices; i++)
        for (int j = 0; j < vertices; j++)
        {
            adjacencyMatrix[i, j] = int.MaxValue / 2;
        }
        for (int u = 0; u < vertices; u++)
        {
            foreach (var (v, w) in AdjacencyList[u])
            {
                adjacencyMatrix[u, v] = w;
            }
        }
    }

    public void ConvertToList()
    {
        AdjacencyList = new List<(int, int)>[vertices];
        for (int i = 0; i < vertices; i++)
        {
            AdjacencyList[i] = new List<(int, int)>();
        }
        for (int i = 0; i < vertices; i++)
        {
            for (int j = 0; j < vertices; j++)
            {
                if (adjacencyMatrix[i, j] < int.MaxValue / 2)
                    AdjacencyList[i].Add((j, adjacencyMatrix[i, j]));
            }
        }
    }
}
public static class GraphGenerator
{
    private static Random rand = new Random();

    public static Graph Generate(int n, double density, bool useList)
    {
        Graph g = new Graph(n, useList);
        int maxEdges = n * (n - 1);
        int targetEdges = (int)(density * maxEdges);
        HashSet<(int, int)> added = new HashSet<(int, int)>();

        while (added.Count < targetEdges)
        {
            int u = rand.Next(n);
            int v = rand.Next(n);
            if (u != v && !added.Contains((u, v)))
            {
                int weight = rand.Next(-10, 21);
                g.AddEdge(u, v, weight);
                added.Add((u, v));
            }
        }
        return g;
    }
}
