namespace MathSharedLib;

public readonly struct Edge
{
    public Point3D StartPoint { get; }
    public Point3D EndPoint { get; }

    public Edge(Point3D startPoint, Point3D endPoint)
    {
        StartPoint = startPoint;
        EndPoint = endPoint;
    }
    public Side GetSide(Point3D point)
    {
        return Utility.GetSideOfLineSegment(this, point);
    }
}
