namespace computationalGeometry_cSharp;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        // GOAL: ray casting but faster, using SIMD and other techniques to speed up the process of determining if a point is inside a polygon.


    }
    public bool IsPointInsidePolyline(Point3d point, Polyline polyline)
    {
        // Implement ray casting algorithm to determine if the point is inside the polygon
        int n = polyline.Vertices.Count;
        bool inside = false;
        for (int i = 0, j = n - 1; i < n; j = i++)
        {
            if ((polyline.Vertices[i].Y > point.Y) != (polyline.Vertices[j].Y > point.Y) &&
                (point.X < (polyline.Vertices[j].X - polyline.Vertices[i].X) * (point.Y - polyline.Vertices[i].Y) / (polyline.Vertices[j].Y - polyline.Vertices[i].Y) + polyline.Vertices[i].X))
            {
                inside = !inside;
            } 
        }
        return inside;
    }
}

class Point3d
{
    public double X { get; set; }
    public double Y { get; set; }
    public double Z { get; set; }
    public Point3d(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
    }
}

class Polyline
{
    public List<Point3d>? Vertices { get; set; } = null;
    public Polyline()
    { Vertices = new ();
    }
}