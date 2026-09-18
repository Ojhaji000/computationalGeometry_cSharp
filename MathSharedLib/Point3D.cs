namespace MathSharedLib;

public class Point3D
{
    public double X { get; }
    public double Y { get; }
    public double Z { get; }
    public Point3D()
    {
        X = 0;
        Y = 0;
        Z = 0;
    }
    public Point3D(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
    }
    public static Vector3D operator -(Point3D a, Point3D b)
    {
        return new Vector3D(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
    }
}