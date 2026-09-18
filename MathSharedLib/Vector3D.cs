namespace MathSharedLib;

public struct Vector3D
{
    public double X { get; }
    public double Y { get; }
    public double Z { get; }
    public Vector3D(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
    }
    //public static Vector3D operator -(Point3D a, Point3D b)
    //{
    //    return new Vector3D(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
    //}
    public static double Dot(Vector3D a, Vector3D b)
    {
        return a.X * b.X + a.Y * b.Y + a.Z * b.Z;
    }
    public static Vector3D Cross(Vector3D a, Vector3D b)
    {
        return new Vector3D(
            a.Y * b.Z - a.Z * b.Y,
            a.Z * b.X - a.X * b.Z,
            a.X * b.Y - a.Y * b.X
        );
    }
}
