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
            // Line
            var line = new Line
            {
                X1 = 50,
                Y1 = 50,
                X2 = 300,
                Y2 = 150,
                Stroke = Brushes.Blue,
                StrokeThickness = 2
            };

            MyCanvas.Children.Add(line);

            // Point
            var point = new Ellipse
            {
                Width = 8,
                Height = 8,
                Fill = Brushes.Red
            };

            Canvas.SetLeft(point, 100);
            Canvas.SetTop(point, 100);

            MyCanvas.Children.Add(point);

            // Polygon
            var polygon = new Polygon
            {
                Stroke = Brushes.Black,
                Fill = Brushes.LightBlue,
                StrokeThickness = 2
            };

            polygon.Points.Add(new Point(400, 50));
            polygon.Points.Add(new Point(600, 80));
            polygon.Points.Add(new Point(650, 200));
            polygon.Points.Add(new Point(450, 250));

            MyCanvas.Children.Add(polygon);

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

            SolveConvexHullProblem.Execute();
        }
    }
}