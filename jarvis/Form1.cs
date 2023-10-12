using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace jarvis
{
    public partial class Form1 : Form
    {
        List<Point> points;
        List<Point> boundPoints;
        Bitmap bmp;
        Pen Black;
        SolidBrush BlackBrush;
        public Form1()
        {
            InitializeComponent();
            points= new List<Point>();
            boundPoints= new List<Point>();
            bmp = new Bitmap(pictureBox1.Width,pictureBox1.Height);
            Black = new Pen(Color.Black);
            BlackBrush= new SolidBrush(Color.Black);
            pictureBox1.Image = bmp;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GenPoints();
        }

        private void GenPoints()
        {
            int width = pictureBox1.Width;
            int height = pictureBox1.Height;
            int pointCount = int.Parse(textBox1.Text);
            Random r = new Random();
            for (int i = 0; i < pointCount; i++)
            {
                int x = r.Next(100, width - 100);
                int y = r.Next(100, height - 100);
                Point p = new Point(x, y);

                while (points.Contains(p))
                {
                    x = r.Next(100,width-100);
                    y = r.Next(100,height-100);
                    p = new Point(x, y);
                }

                points.Add(p);
            }

            
                points.ForEach(x => { bmp.SetPixel(x.X, x.Y, Color.Black); });
                pictureBox1.Invalidate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (points.Count < 3)
                throw new Exception("need more points");
            Jarvis();
        }

        private void Jarvis() { 
            Point start = points.Where(x=>x.X==points.Min(min=>min.X)).First();
            Point end = points[0];

           

            do {
                boundPoints.Add(start);
                for (int i = 1; i < points.Count; i++)
                {
                    if (start == end || Rotation(start, end, points[i])==-1)
                        end = points[i];
                }
                start = end;
            }

            while (start!=boundPoints.First());

            BlackBrush.Color = Color.Red;
            using (Graphics g = Graphics.FromImage(bmp))
            {
                boundPoints.Add(boundPoints.First());
                boundPoints.ForEach(x => { g.FillEllipse(BlackBrush,x.X-3,x.Y-3,3,3); bmp.SetPixel(x.X, x.Y, Color.Red); });
                g.DrawLines(Black, boundPoints.ToArray());
                pictureBox1.Invalidate();
            }
        }

        private int Rotation(Point p1, Point p2, Point p) {
            int rot = (p2.X - p1.X) * (p.Y - p1.Y) - (p.X - p1.X) * (p2.Y - p1.Y);

            if (rot> 0)
                return -1; 
            if (rot < 0)
                return 1;
            return 0; 
        }

        private void button3_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image= bmp;
            points.Clear();
            boundPoints.Clear();
            pictureBox1.Invalidate();
        }
    }
}
