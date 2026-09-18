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
    private List<Point3D> _points;

    [GlobalSetup]
    public void Setup()
    {
        Random rnd = new(42);

        _points = Enumerable.Range(0, 1000)
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
        return SolveConvexHullProblem.OptimisedExecute(_points);
    }
}