namespace icantlivewithoutdiscretemath;
using System;
using System.Collections.Generic;
using System.Diagnostics;

public static class Experiment
{
    public static void Run()
    {
        int[] sizes = { 20, 40, 60, 80, 100, 120, 140, 160, 180, 200 };
        double[] densities = { 0.1, 0.3, 0.5, 0.7, 0.9 };

        foreach (bool useList in new[] { true, false })
        {
            Console.WriteLine("\n==== " + (useList ? "List" : "Matrix") + " ====");
            foreach (var size in sizes)
            {
                foreach (var d in densities)
                {
                    long total = 0;
                    for (int i = 0; i < 20; i++)
                    {
                        var g = GraphGenerator.Generate(size, d, useList);
                        var sw = Stopwatch.StartNew();
                        var res = Johnson.Run(g);
                        sw.Stop();
                        total += sw.ElapsedMilliseconds;
                    }
                    Console.WriteLine($"Size: {size}, Dens: {d:F1}, Time: {total / 20.0:F2} ms");
                }
            }
        }
    }
}
 
