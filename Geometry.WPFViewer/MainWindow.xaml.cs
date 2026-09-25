using MathSharedLib;

using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Geometry.WPFViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DrawExamples();
        }



        private void DrawExamples()
        {
            MyCanvas.Children.Add(new Line
            {
                X1 = 0,
                Y1 = 300,
                X2 = 600,
                Y2 = 300,
                Stroke = Brushes.Black
            });

            MyCanvas.Children.Add(new Line
            {
                X1 = 300,
                Y1 = 0,
                X2 = 300,
                Y2 = 600,
                Stroke = Brushes.Black
            });

            List<Point3D> points = new List<Point3D>
        {
            new Point3D(100, 100, 0),
            new Point3D(200, 270, 0),
            new Point3D(150, 150, 0),
            new Point3D(250, 80, 0),
            new Point3D(100, 70, 0)
        };
            double[] points_Xs = new double[points.Count];
            double[] points_Ys = new double[points.Count];
            List<Edge> _edges = new List<Edge>();
            for (int i = 0; i < points.Count; i++)
            {
                points_Xs[i] = points[i].X;
                points_Ys[i] = points[i].Y;
                for (int j = i + 1; j < points.Count; j++)
                {
                    _edges.Add(new Edge(points[i], points[j]));
                }
            }
            var convexHullEdges = MathSharedLib.SolveConvexHullProblem.Execute_SIMD(points_Xs, points_Ys,_edges);
            DrawConvexHull(convexHullEdges);
            DrawPoints(points);
        }

        private void MyCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point clickPosition = e.GetPosition(MyCanvas);
            var point = new Ellipse
            {
                Width = 8,
                Height = 8,
                Fill = Brushes.Red
            };
            Canvas.SetLeft(point, clickPosition.X - point.Width / 2);
            Canvas.SetTop(point, clickPosition.Y - point.Height / 2);
            MyCanvas.Children.Add(point);
        }

        private void DrawConvexHull(List<Edge> polygonEdges)
        {
            foreach (var e in polygonEdges)
            {
                var line = new Line
                {
                    X1 = e.StartPoint.X,
                    Y1 = e.StartPoint.Y,
                    X2 = e.EndPoint.X,
                    Y2 = e.EndPoint.Y,
                    Stroke = Brushes.Blue,
                    StrokeThickness = 2
                };

                MyCanvas.Children.Add(line);
            }
        }

        private void DrawPoints(List<Point3D> points)
        {
            foreach (var p in points)
            {
                var point = new Ellipse
                {
                    Width = 8,
                    Height = 8,
                    Fill = Brushes.Red
                };
                Canvas.SetLeft(point, p.X - point.Width / 2);
                Canvas.SetTop(point, p.Y - point.Height / 2);
                MyCanvas.Children.Add(point);
            }
        }
    }
}