using MathSharedLib;
namespace Geometry.WPFViewer;

internal class SolveConvexHullProblem
{
    public static void Execute()
    {
        List<Point3D> points = new List<Point3D>
        {
            new Point3D(0, 0, 0),
            new Point3D(1, 0, 0),
            new Point3D(0, 1, 0),
            new Point3D(0, 0, 1),
            new Point3D(1, 1, 1)
        };

        List<Edge> edges = new();
        for (int i = 0; i < points.Count; i++) 
        {
            for(int j = 0; j < points.Count; j++)
            {
                if (i == j) continue;
                edges.Add(new Edge(points[i], points[j]));
            }
        }

        List<Edge> convexHullEdges = new();

        foreach (var edge in edges)
        {
            var predefinedSide = Side.NoSide;
            bool isOk = true;
            foreach(var p in points)
            {
                var currSide = edge.GetSide(p);

                predefinedSide = predefinedSide is Side.NoSide ? currSide : predefinedSide;

                if (currSide != predefinedSide)
                {
                    isOk = false;
                    break;
                }
            }

            if (isOk)
            {
                convexHullEdges.Add(edge);
            }
        }

        // task: look for the convex hull edges,
        // and return the list of edges that are part of the convex hull

        // take all the points, check for every line segment
        // possible for the following:
        // 1. all the points except the two endpoints of the line segment are on the same side of the line segment
        // 2. collect the edges
        // 3. sort them in counter-clockwise order


    }
}
