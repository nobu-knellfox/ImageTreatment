using System.Windows;
using Microsoft.Win32;
using System;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.IO;
using System.Text;

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
        private System.Collections.Generic.List<Points> points = new System.Collections.Generic.List<Points>();
        private Point temp_p;
        private bool onclick = false;
        private System.Collections.Generic.List<Line[]> line = new System.Collections.Generic.List<Line[]>();
        string[] files;
        int fileindex = 0;
        System.IO.StreamWriter stream;

        public MainWindow()
        {
            InitializeComponent();
            stream = new StreamWriter(@"./info.dat", false, Encoding.GetEncoding("Shift_JIS"));
            stream.Close();
        }

        private void input_Click(object sender, RoutedEventArgs e)
        {
            files = System.IO.Directory.GetFiles(@"./img", "*", System.IO.SearchOption.AllDirectories);


            capture.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(System.IO.Path.GetFullPath(@files[fileindex])));
        }

        private void save_Click(object sender, RoutedEventArgs e)
        { 
            stream = new StreamWriter(@"./info.dat", true, Encoding.GetEncoding("Shift_JIS"));

            string writeline = "";
            writeline += files[fileindex] + " ";
            writeline += points.Count.ToString();

            foreach (Points pts in points)
            {

                writeline += " " + ((int)pts.start.X).ToString() + " " + ((int)pts.start.Y).ToString() + " ";
                writeline += ((int)(pts.end.X - pts.start.X)).ToString() + " " + ((int)(pts.end.Y - pts.start.Y)).ToString();
            }

            stream.WriteLine(writeline);

            foreach (Line[] lis in line)
                foreach (Line li in lis)
                    image.Children.Remove(li);

            line.Clear();
            points.Clear();
            stream.Close();

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
                li.StrokeThickness = 3;

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
            if(files.Length -1 == fileindex)
            {
                MessageBox.Show("aaaa");
                return;
            }

            ++fileindex;
            capture.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(System.IO.Path.GetFullPath(@files[fileindex])));
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
