using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace laba5
{
    public partial class Midpoint : Form
    {
     
        List<Point> points;
        bool isFirst;
        Pen p;
        Random rnd;
        public Midpoint()
        {
            InitializeComponent();
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            isFirst = true ;
            points = new List<Point>();
            p = new Pen(Color.Black);
            rnd= new Random();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (isFirst)
            {
                isFirst = false;
                points.Add(e.Location);
            }
            else
            {
                points.Add(e.Location);
                ReDraw();
            }
        }

        private void ReDraw() { 
            pictureBox1.Image= new Bitmap(pictureBox1.Width, pictureBox1.Height);
            using( Graphics g = Graphics.FromImage(pictureBox1.Image)){
                g.DrawLines(p,points.ToArray());
                pictureBox1.Invalidate();
            }
        }

        private int GetDistance(Point p1, Point p2) {
            return (int)Math.Sqrt(Math.Pow((p2.X-p1.X),2) +Math.Pow((p2.Y-p1.Y),2));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double r = double.Parse(textBox1.Text);
            int count = points.Count;
            for (int i = 0; i < count-1; i++)
            {
                int d = GetDistance(points[i], points[i + 1]);
                Point p = Point.Empty;
                p.X = (points[i + 1].X - points[i].X)/2 + points[i].X;
                p.Y = (points[i + 1].Y - points[i].Y) / 2 + points[i].Y + rnd.Next((int)-(r * d), (int)(r * d));
                points.Add(p);
            }
            points=points.OrderByDescending(p => p.X).ToList();
            ReDraw();
        }
    }
}
