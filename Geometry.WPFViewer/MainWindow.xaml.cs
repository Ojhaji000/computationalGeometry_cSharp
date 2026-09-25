using MathSharedLib;

using System.ComponentModel.DataAnnotations;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Geometry.WPFViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Point3D> _points = new();
        private List<Edge> _edges = new();

        public MainWindow()
        {
            InitializeComponent();
            InitializePoints();
            this.Loaded += (s, e) => DrawAxes();
        }

        private void InitializePoints()
        {
            _points = new List<Point3D>
            {
                new Point3D(100, 100, 0),
                new Point3D(200, 270, 0),
                new Point3D(150, 150, 0),
                new Point3D(250, 80, 0),
                new Point3D(100, 70, 0)
            };

            for (int i = 0; i < _points.Count; i++)
            {
                for (int j = i + 1; j < _points.Count; j++)
                {
                    _edges.Add(new Edge(_points[i], _points[j]));
                }
            }
        }

        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            MyCanvas.Children.Clear();
            DrawAxes();

            if (BruteForceRadio.IsChecked == true)
            {
                DrawBruteForceConvexHull();
            }
            else if (SIMDRadio.IsChecked == true)
            {
                DrawSIMDConvexHull();
            }
            else if (LineIntersectionRadio.IsChecked == true)
            {
                DrawLineIntersection();
            }
        }

        //private void DrawExamples()
        //{
        //    DrawSIMDConvexHull();
        //    DrawPoints(_points);
        //}

        private void DrawAxes()
        {
            if(MyCanvas.ActualWidth is 0 || MyCanvas.ActualHeight is 0)
            {
                MessageBox.Show("Canvas size is not set yet. Please resize the window.", "Canvas Size Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            double centerX = MyCanvas.ActualWidth / 2;
            double centerY = MyCanvas.ActualHeight / 2;

            MyCanvas.Children.Add(new Line
            {
                X1 = 0,
                Y1 = centerY,
                X2 = MyCanvas.ActualWidth,
                Y2 = centerY,
                Stroke = Brushes.Black
            });

            MyCanvas.Children.Add(new Line
            {
                X1 = centerX,
                Y1 = 0,
                X2 = centerX,
                Y2 = MyCanvas.ActualHeight,
                Stroke = Brushes.Black
            });
        }

        private void DrawBruteForceConvexHull()
        {
            double[] points_Xs = new double[_points.Count];
            double[] points_Ys = new double[_points.Count];
            for (int i = 0; i < _points.Count; i++)
            {
                points_Xs[i] = _points[i].X;
                points_Ys[i] = _points[i].Y;
            }
            var convexHullEdges = MathSharedLib.SolveConvexHullProblem.BruteExecute(_points);
            DrawEdges(convexHullEdges);
            DrawPoints(_points);
        }

        private void DrawSIMDConvexHull()
        {
            double[] points_Xs = new double[_points.Count];
            double[] points_Ys = new double[_points.Count];
            for (int i = 0; i < _points.Count; i++)
            {
                points_Xs[i] = _points[i].X;
                points_Ys[i] = _points[i].Y;
            }
            var convexHullEdges = MathSharedLib.SolveConvexHullProblem.Execute_SIMD(points_Xs, points_Ys, _edges);
            DrawEdges(convexHullEdges);
            DrawPoints(_points);
        }

        private void DrawLineIntersection()
        {
            // TODO: Implement line intersection visualization
            _edges.Clear();
            _edges = new List<Edge>()
            {
                new Edge(new Point3D(10,10,0), new Point3D(-10,-10,0)),
                new Edge(new Point3D(-10,10,0), new Point3D(10,-10,0))

            };
            DrawEdges(_edges);
            var IntersectPoints = Utility.GetIntersectionPointsFromLineSegements_BRUTE_FORCE(_edges);
            DrawPoints(IntersectPoints, Brushes.Black);
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

        private void DrawEdges(List<Edge> polygonEdges)
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

        private void DrawPoints(List<Point3D> points, Brush? color = null)
        {
            color ??= Brushes.Red;
            foreach (var p in points)
            {
                var point = new Ellipse
                {
                    Width = 8,
                    Height = 8,
                    Fill = color
                };
                Canvas.SetLeft(point, p.X - point.Width / 2);
                Canvas.SetTop(point, p.Y - point.Height / 2);
                MyCanvas.Children.Add(point);
            }
        }
    }
}