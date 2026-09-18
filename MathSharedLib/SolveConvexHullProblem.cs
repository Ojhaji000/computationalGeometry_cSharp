using System.Numerics;
namespace MathSharedLib;

public class SolveConvexHullProblem
{
    private const double _tol = 1e-6;
    public static List<Edge> BruteExecute(List<Point3D> points)
    {
        List<Edge> edges = new();
        for (int i = 0; i < points.Count; i++) 
        {
            for(int j = i+1; j < points.Count; j++)
            {
                edges.Add(new Edge(points[i], points[j]));
            }
        }

        List<Edge> convexHullEdges = new();

        foreach (var edge in edges)
        {
            Side side = Side.NoSide;
            bool isHullEdge = true;
            foreach(var p in points)
            {
                var currSide = edge.GetSide(p);

                if(currSide is Side.OnEdge)
                    continue;


                if(side is Side.NoSide)
                {
                    side = currSide;
                }
                else if (currSide != side)
                {
                    isHullEdge = false;
                    break;
                }
            }

            if (isHullEdge)
            {
                convexHullEdges.Add(edge);
            }
        }
        return convexHullEdges;
        //draw the lines

        // task: look for the convex hull edges,
        // and return the list of edges that are part of the convex hull

        // take all the points, check for every line segment
        // possible for the following:
        // 1. all the points except the two endpoints of the line segment are on the same side of the line segment
        // 2. collect the edges
        // 3. sort them in counter-clockwise order


    }


    public static List<Edge> OptimisedExecute(in List<Point3D> points)
    {
        // Implement a more efficient convex hull algorithm here, such as Graham's scan or QuickHull.
        List<Edge> edges = new();
        double[] points_Xs = new double[points.Count];
        double[] points_Ys = new double[points.Count];
        //double[] points_Zs = new double[points.Count];

        for (int i = 0; i < points.Count; i++)
        {
            // data collection for simd
            points_Xs[i] = points[i].X;
            points_Ys[i] = points[i].Y;

            // edge creation
            for (int j = i+1; j < points.Count; j++)
            {
                edges.Add(new Edge(points[i], points[j]));
            }
        }


        List<Edge> convexHullEdges = new();
        int width = Vector<double>.Count;
        int lastSIMD = points.Count - points.Count % width;

        foreach (var edge in edges)
        {
            bool hasPos = false;
            bool hasNeg = false;

            double ax = edge.StartPoint.X;
            Vector<double> axVector = new Vector<double>(ax);// all the elements are of ax value
            double ay = edge.StartPoint.Y;
            Vector<double> ayVector = new Vector<double>(ay);// all the elements are of ay value

            double dx = edge.EndPoint.X - ax;
            double dy = edge.EndPoint.Y - ay;

            Vector<double> numericsVector_A_VECTOR_Xs = new Vector<double>(dx);
            Vector<double> numericsVector_A_VECTOR_Ys = new Vector<double>(dy);
            bool tobeAdded = true;
            for (int i = 0; i < lastSIMD; i += width)
            {
                var point_Xs = new Vector<double>(points_Xs, i);
                var point_Ys = new Vector<double>(points_Ys, i);

                Vector<double> numericsVector_B_VECTOR_Xs = point_Xs - axVector;
                Vector<double> numericsVector_B_VECTOR_Ys = point_Ys - ayVector;

                var cross_Z = numericsVector_A_VECTOR_Xs * numericsVector_B_VECTOR_Ys - numericsVector_A_VECTOR_Ys * numericsVector_B_VECTOR_Xs;

                var positiveMask =
    Vector.GreaterThan(cross_Z, new Vector<double>(_tol));

                var negativeMask =
                    Vector.LessThan(cross_Z, new Vector<double>(-_tol));
                if (hasNeg && hasPos)
                {
                    tobeAdded = false;
                    break;
                }
            }

            for (int i = lastSIMD; i < points.Count; i++)
            {
                double c =
                    dx * (points_Ys[i] - ay)
                    - dy * (points_Xs[i] - ax);

                if (c > _tol)
                    hasPos = true;
                else if (c < -_tol)
                    hasNeg = true;

                if (hasPos && hasNeg)
                {
                    tobeAdded = false;
                    break;
                }
            }
            if (tobeAdded)
            {
                convexHullEdges.Add(edge);
            }
        }
        return convexHullEdges;
    }
}
