using System.Numerics;
namespace MathSharedLib;

public class SolveConvexHullProblem
{
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


    public static List<Edge> Execute_SIMD(in double[] points_Xs, in double[] points_Ys, in List<Edge> edges)
    {
        List<Edge> convexHullEdges = new();
        int width = Vector<double>.Count;
        int lastSIMD = points_Xs.Length - points_Xs.Length % width;

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
                var v1 = new Vector<double>(points_Xs, i);
                var v2 = new Vector<double>(points_Ys, i);

                Vector<double> numericsVector_B_VECTOR_Xs = v1 - axVector;
                Vector<double> numericsVector_B_VECTOR_Ys = v2 - ayVector;

                var cross_Z = numericsVector_A_VECTOR_Xs * numericsVector_B_VECTOR_Ys - numericsVector_A_VECTOR_Ys * numericsVector_B_VECTOR_Xs;

                var positiveMask = Vector.GreaterThan(cross_Z, new Vector<double>(Configuration.TOL6));

                var negativeMask = Vector.LessThan(cross_Z, new Vector<double>(-Configuration.TOL6));
                for (int j = 0; j < width; j++)
                {
                    if (positiveMask[j] != 0)
                        hasPos = true;

                    if (negativeMask[j] != 0)
                        hasNeg = true;
                }

                if (hasPos && hasNeg)
                {
                    tobeAdded = false;
                    break;
                }
            }

            for (int i = lastSIMD; i < points_Xs.Length; i++)
            {
                double c =
                    dx * (points_Ys[i] - ay)
                    - dy * (points_Xs[i] - ax);

                if (c > Configuration.TOL6)
                    hasPos = true;
                else if (c < -Configuration.TOL6)
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
