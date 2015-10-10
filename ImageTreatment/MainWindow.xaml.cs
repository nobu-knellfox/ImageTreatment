using System.Windows;
using Microsoft.Win32;
using System;
using System.Windows.Shapes;
using System.Windows.Controls;

namespace ImageTreatment
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    /// 
    public struct Points{
        public Point start, end;
    }

    public partial class MainWindow : Window
    {
        private string filename;
        private System.Collections.Generic.List<Points> points = new System.Collections.Generic.List<Points>();
        private Point temp_p;
        private bool onclick = false;
        private System.Collections.Generic.List<Line[]> line = new System.Collections.Generic.List<Line[]>();


        public MainWindow()
        {
            InitializeComponent();
        }

        private void input_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd;
            ofd = new OpenFileDialog();
            ofd.FileName = "";
            ofd.DefaultExt = "*.*";
            if (ofd.ShowDialog() == true){
                filename = ofd.FileName;
            }
            capture.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(filename));
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {

        }

        private void image_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            temp_p = e.GetPosition(image);
            onclick = true;

            Line[] t_lines = new Line[4] { new Line(), new Line(), new Line(), new Line() };

            line.Add(t_lines);

            foreach (Line li in line[line.Count-1])
            {
                li.Stroke = System.Windows.Media.Brushes.Red;
                li.StrokeThickness = 1;

                image.Children.Add(li);
            }
        }

        private void image_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Points ps = new Points();
            ps.start = temp_p;
            ps.end = e.GetPosition(image);
            points.Add(ps);
            onclick = false;
            
            

        }

        private void image_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            
            if (onclick) {
                line[line.Count-1][0].X1 = temp_p.X;
                line[line.Count - 1][0].Y1 = temp_p.Y;
                line[line.Count - 1][0].X2 = e.GetPosition(image).X;
                line[line.Count - 1][0].Y2 = temp_p.Y;

                line[line.Count-1][1].X1 = temp_p.X;
                line[line.Count-1][1].Y1 = temp_p.Y;
                line[line.Count-1][1].X2 = temp_p.X;
                line[line.Count-1][1].Y2 = e.GetPosition(image).Y;

                line[line.Count-1][2].X1 = e.GetPosition(image).X;
                line[line.Count-1][2].Y1 = temp_p.Y;
                line[line.Count-1][2].X2 = e.GetPosition(image).X;
                line[line.Count-1][2].Y2 = e.GetPosition(image).Y;

                line[line.Count-1][3].X1 = temp_p.X;
                line[line.Count-1][3].Y1 = e.GetPosition(image).Y;
                line[line.Count-1][3].X2 = e.GetPosition(image).X;
                line[line.Count-1][3].Y2 = e.GetPosition(image).Y;
            }
        }

        private void next_Click(object sender, RoutedEventArgs e)
        {

        }

        private void reset_Click(object sender, RoutedEventArgs e)
        {
            if (line.Count > 0)
            {
                foreach(Line li in line[line.Count - 1])
                    image.Children.Remove(li);

                line.RemoveAt(line.Count - 1);
                points.RemoveAt(points.Count - 1);
            }
        }
    }
}
