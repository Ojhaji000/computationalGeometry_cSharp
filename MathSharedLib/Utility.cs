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
        // Implementation to determine which side of the line segment the point is on
        return Side.NoSide;
    }
}
public enum Side
{
    Left,
    Right,
    OnEdge,
    NoSide
}
