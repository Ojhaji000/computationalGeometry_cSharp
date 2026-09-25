using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

using MathSharedLib;

namespace Geometry.TestConsole;

internal class Program
{
    static void Main(string[] args)
    {
        //Console.WriteLine("Hello, World!");
        //Math
        BenchmarkRunner.Run<ConvexHullBenchmarks>();
    }
        
}

[MemoryDiagnoser]
public class ConvexHullBenchmarks
{
    private List<Point3D> _points = new();
    [GlobalSetup]
    public void Setup() 
    {

        Random rnd = new(42);

        _points = Enumerable.Range(0, 10000)
            .Select(_ => new Point3D(
                rnd.NextDouble() * 100,
                rnd.NextDouble() * 100,
                0))
            .ToList();

    }

    [Benchmark]
    public List<Edge> BruteForceConvexHull()
    {
        return SolveConvexHullProblem.BruteExecute(_points);
    }

    [Benchmark]
    public List<Edge> OptimisedConvexHull()
    {
        List<double> _points_Xs = new();
        List<double> _points_Ys = new();
        List<Edge> _edges = new();
        for (int i = 0; i<_points.Count; i++)
        {
            for (int j = i + 1; j<_points.Count; j++)
            {
                _edges.Add(new Edge(_points[i], _points[j]));
            }
        }


        return SolveConvexHullProblem.Execute_SIMD(_points_Xs.ToArray(), _points_Ys.ToArray(), _edges);
    }
}