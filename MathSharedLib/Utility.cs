namespace MathSharedLib;

public class Utility
{
    public static bool AreLineSegmentsParallel(Edge line1, Edge line2)
    {
        // Implementation to check if two line segments are parallel
        return false;
    }

    //public static bool AreLineSegmentsIntersecting(Edge line1, Edge line2)
    //{
    //    // Implementation to check if two line segments intersect
    //    return false;
    //}

    public static bool AreLineSegmentsIntersecting(Edge line1, Edge line2, out Point3D intersectionPoint)
    {
        // Implementation to check if two line segments are intersecting
        intersectionPoint = new ();

        Point3D A = line1.StartPoint;
        Point3D B = line1.EndPoint;
        Point3D C = line2.StartPoint;
        Point3D D = line2.EndPoint;
        // Line AB represented as a1x + b1y = c1
        double a1 = B.Y - A.Y;
        double b1 = A.X - B.X;
        double c1 = a1 * A.X + b1 * A.Y;

        // Line CD represented as a2x + b2y = c2
        double a2 = D.Y - C.Y;
        double b2 = C.X - D.X;
        double c2 = a2 * C.X + b2 * C.Y;

        // Determinant
        double determinant = a1 * b2 - a2 * b1;

        // If determinant is 0, the lines are parallel
        if (Math.Abs(determinant) < Configuration.TOL3)
        {
            intersectionPoint = new Point3D();
            return false;
        }

        double x = (b2 * c1 - b1 * c2) / determinant;
        double y = (a1 * c2 - a2 * c1) / determinant;

        intersectionPoint = new Point3D(x, y, 0);
        return true;
    }

    public static Side GetSideOfLineSegment(Edge line, Point3D point)
    {
        Vector3D lineVector = line.EndPoint - line.StartPoint;
        Vector3D pointVector = point - line.StartPoint;

        Vector3D crossProduct = Vector3D.Cross(lineVector, pointVector);

        if (crossProduct.Z > Configuration.TOL6)
        {
            return Side.Left;
        }
        if (crossProduct.Z < -Configuration.TOL6)
        {
            return Side.Right;
        }
            return Side.OnEdge;
    }

    public static List<Point3D> GetIntersectionPointsFromLineSegements_BRUTE_FORCE(List<Edge> lineSegments)
    {
        // Implementation to find intersection points from line segments using brute force
        List<Point3D> intersectionPoints = new ();
        for (int i = 0; i < lineSegments.Count; i++)
        {
            for (int j = 0 + 1; j < lineSegments.Count; j++)
            {
                if (AreLineSegmentsIntersecting(lineSegments[i], lineSegments[j], out Point3D intersectionPoint))
                {
                    intersectionPoints.Add(intersectionPoint);
                }
            }
        }
        return intersectionPoints;
    }
    public static List<Point3D> GetIntersectionPointsFromLineSegements_PLANE_SWEEP(List<Edge> lineSegments)
    {
        // Implementation to find intersection points from line segments using plane sweep
        return new List<Point3D>();
    }

}
public enum Side
{
    Left,
    Right,
    OnEdge,
    NoSide
}
