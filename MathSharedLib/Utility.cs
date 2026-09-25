namespace MathSharedLib;

public class Utility
{
    public static bool AreLineSegmentsParallel(Edge line1, Edge line2)
    {
        // Implementation to check if two line segments are parallel
        return false;
    }

    public static bool AreLineSegmentsIntersecting(Edge line1, Edge line2)
    {
        // Implementation to check if two line segments intersect
        return false;
    }

    public static bool AreLineSegmentsCollinear(Edge line1, Edge line2)
    {
        // Implementation to check if two line segments are collinear
        return false;
    }

    public static Side GetSideOfLineSegment(Edge line, Point3D point)
    {
        Vector3D lineVector = line.EndPoint - line.StartPoint;
        Vector3D pointVector = point - line.StartPoint;

        Vector3D crossProduct = Vector3D.Cross(lineVector, pointVector);

        const double _tol = 1e-6; // Tolerance for floating-point comparison
        if (crossProduct.Z > _tol)
        {
            return Side.Left;
        }
        if (crossProduct.Z < -_tol)
        {
            return Side.Right;
        }
            return Side.OnEdge;
    }

    public static List<Point3D> GetIntersectionPointsFromLineSegements_BRUTE_FORCE(List<Edge> lineSegments)
    {
        // Implementation to find intersection points from line segments using brute force
        return new List<Point3D>();
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
