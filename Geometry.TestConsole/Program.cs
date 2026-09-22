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
    private List<double> _points_Xs = new();
    private List<double> _points_Ys = new();
    private List<Edge> _edges = new();
    [GlobalSetup]
    public void Setup()
    {

        Point3D prev = new ();
        Point3D curr = new ();

        for (int i = 0; i < 1000; i++)
        {
            Random rnd = new(42);
            var tempPoint =
            new Point3D(
                rnd.NextDouble() * 100,
                rnd.NextDouble() * 100,
                0);
            _points.Add(tempPoint);
            _points_Xs.Add(tempPoint.X);
            _points_Ys.Add(tempPoint.Y);
        }

        for (int i = 0; i < _points.Count; i++)
        {
            for (int j = i + 1; j < _points.Count; j++)
            {
                _edges.Add(new Edge(_points[i], _points[j]));
            }
        }
    }

    [Benchmark]
    public List<Edge> BruteForceConvexHull()
    {
        return SolveConvexHullProblem.BruteExecute(_points);
    }

    [Benchmark]
    public List<Edge> OptimisedConvexHull()
    {
        return SolveConvexHullProblem.OptimisedExecute(_points_Xs.ToArray(), _points_Ys.ToArray(), _edges);
    }
}