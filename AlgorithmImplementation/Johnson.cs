namespace icantlivewithoutdiscretemath;

public static class Johnson
{
        public static int[,] Run(Graph g)
    {
        int n = g.vertices;
        g.ConvertToList();

        int[] h = BellmanFord(g, n);
        if (h == null) return null;
        
        var newGraph = new Graph(n, true);
        for (int u = 0; u < n; u++)
            foreach (var (v, w) in g.AdjacencyList[u])
                newGraph.AddEdge(u, v, w + h[u] - h[v]);
        
        int[,] dist = new int[n, n];
        for (int u = 0; u < n; u++)
        {
            int[] d = Dijkstra(newGraph, u);
            for (int v = 0; v < n; v++)
                dist[u, v] = d[v] + h[v] - h[u];
        }
        return dist;
    }

    private static int[] BellmanFord(Graph g, int n)
    {
        int[] dist = new int[n + 1];
        for (int i = 0; i <= n; i++) dist[i] = int.MaxValue / 2;
        dist[n] = 0;

        var extGraph = new Graph(n + 1, true);
        for (int u = 0; u < n; u++)
            foreach (var (v, w) in g.AdjacencyList[u])
                extGraph.AddEdge(u, v, w);
        for (int u = 0; u < n; u++)
            extGraph.AddEdge(n, u, 0);

        for (int i = 0; i < n; i++)
            for (int u = 0; u <= n; u++)
                foreach (var (v, w) in extGraph.AdjacencyList[u])
                    if (dist[u] + w < dist[v])
                        dist[v] = dist[u] + w;

        foreach (var (v, w) in extGraph.AdjacencyList[n])
            if (dist[n] + w < dist[v])
                return null;

        return dist[..n];
    }

    private static int[] Dijkstra(Graph g, int src)
    {
        int[] dist = new int[g.vertices];
        bool[] visited = new bool[g.vertices];
        for (int i = 0; i < g.vertices; i++) dist[i] = int.MaxValue / 2;
        dist[src] = 0;
        var pq = new SortedSet<(int dist, int v)>();
        pq.Add((0, src));

        while (pq.Count > 0)
        {
            var (d, u) = pq.Min; pq.Remove(pq.Min);
            if (visited[u]) continue;
            visited[u] = true;

            foreach (var (v, w) in g.AdjacencyList[u])
            {
                if (dist[u] + w < dist[v])
                {
                    dist[v] = dist[u] + w;
                    pq.Add((dist[v], v));
                }
            }
        }
        return dist;
    }
}