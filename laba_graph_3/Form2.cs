using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace laba_graph_3
{
    public partial class Form2 : Form
    {
        private Point? _Previous = null;
        Pen p = new Pen(Color.Black);
        Pen z = new Pen(Color.Black);
        bool zalivka = false;
        bool imgZalivka = false;
        bool isimgBound;
        Bitmap bmp;
        Bitmap imgBmp;
        Bitmap imgBound;
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
           
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            _Previous = e.Location;
            pictureBox1_MouseMove(sender, e);
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (_Previous != null)
            {
                if (pictureBox1.Image == null)
                {
                    Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                    using (Graphics g = Graphics.FromImage(bmp))
                    {
                        g.Clear(Color.White);
                    }
                    pictureBox1.Image = bmp;
                }
                using (Graphics g = Graphics.FromImage(pictureBox1.Image))
                {
                    g.DrawLine(p, _Previous.Value, e.Location);
                }
                pictureBox1.Invalidate();
                _Previous = e.Location;
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            _Previous = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
            
            pictureBox1.Invalidate();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            zalivka = true;
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                z.Color = colorDialog1.Color;
            }
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (zalivka) {
                zalivka = false;
                FillColor(e);

            }

            if (imgZalivka)
            {
                bmp = new Bitmap(pictureBox1.Image);
                imgZalivka = false;            
                FillPicture(e.X, e.Y);

            }

            if (isimgBound) {
                isimgBound = false;
                FindBounds(e.X,e.Y);
            
            }

        }

        private void FindBounds(int x, int y) { 
            List<Point> points = new List<Point>();
            Color targetColor = imgBound.GetPixel(x, y);
            Point curr = new Point(x, y);

            Queue<Point> queue = new Queue<Point>();
            HashSet<Point> visited = new HashSet<Point>();
            queue.Enqueue(curr);
            
            while (queue.Count>0) { 
                curr=queue.Dequeue();

                if (!CheckCoords(curr.X, curr.Y) || curr.X==imgBound.Width || curr.Y == imgBound.Height)
                    continue;
                if (visited.Contains(curr))
                    continue;

                if (imgBound.GetPixel(curr.X,curr.Y).ToArgb() == targetColor.ToArgb())
                    points.Add(curr);

                visited.Add(curr);
                queue.Enqueue(new Point(curr.X+1,curr.Y));
                queue.Enqueue(new Point(curr.X - 1, curr.Y));
                queue.Enqueue(new Point(curr.X, curr.Y - 1));
                queue.Enqueue(new Point(curr.X, curr.Y + 1));

            }

            foreach (Point p in points)
            {
                imgBound.SetPixel(p.X, p.Y, Color.Red);
            }


            pictureBox1.Image = imgBound;
            pictureBox1.Invalidate();
        }


        private void FillColor(MouseEventArgs e) {

            Queue<Point> points = new Queue<Point>();
            Point curr = e.Location;
            points.Enqueue(curr);
            Bitmap bmp = (Bitmap)pictureBox1.Image;

            while (points.Count != 0)
            {
                curr = points.Dequeue();
                if (!CheckCoords(curr.X, curr.Y))
                    continue;
                if (bmp.GetPixel(curr.X, curr.Y).ToArgb() != z.Color.ToArgb() && bmp.GetPixel(curr.X, curr.Y).ToArgb() != p.Color.ToArgb())
                {
                    bmp.SetPixel(curr.X, curr.Y, z.Color);
                    points.Enqueue(new Point(curr.X + 1, curr.Y));
                    points.Enqueue(new Point(curr.X - 1, curr.Y));
                    points.Enqueue(new Point(curr.X, curr.Y + 1));
                    points.Enqueue(new Point(curr.X, curr.Y - 1));
                }

            }

            pictureBox1.Image = bmp;
            pictureBox1.Invalidate();
        }

        struct pnt {
            public Point bmp_p;
            public  Point pic_p;

            public pnt(Point p1, Point p2) { 
                bmp_p = p1;
                pic_p = p2;
            }



        }

        private void FillPicture(int x, int y)
        {
            Queue < pnt > points = new Queue<pnt>();
            points.Enqueue(new pnt ( new Point(x, y), new Point(0, 0) ));
            pnt curr = new pnt(new Point(x, y), new Point(0, 0));
 
          

            while (points.Count > 0)
            {
                curr = points.Dequeue();
                if (!CheckCoords(curr.bmp_p.X, curr.bmp_p.Y))
                    continue;
                if (bmp.GetPixel(curr.bmp_p.X, curr.bmp_p.Y).ToArgb() != imgBmp.GetPixel(curr.pic_p.X, curr.pic_p.Y).ToArgb() && bmp.GetPixel(curr.bmp_p.X, curr.bmp_p.Y).ToArgb() != p.Color.ToArgb())
                {
                    bmp.SetPixel(curr.bmp_p.X, curr.bmp_p.Y, imgBmp.GetPixel(curr.pic_p.X, curr.pic_p.Y));

                    if (curr.pic_p.X-1 == -1)
                    {
                            pnt newPoint = new pnt(new Point(curr.bmp_p.X - 1, curr.bmp_p.Y), new Point(imgBmp.Width-1, curr.pic_p.Y));
                        
                            points.Enqueue(newPoint);
               
                     
                    }
                    else
                    {
                        pnt newPoint = new pnt(new Point(curr.bmp_p.X - 1, curr.bmp_p.Y), new Point(curr.pic_p.X - 1, curr.pic_p.Y));
                       
                        points.Enqueue(newPoint);
                    }


                    if (curr.pic_p.X+1==imgBmp.Width)
                    {

                            pnt newPoint = new pnt(new Point(curr.bmp_p.X + 1, curr.bmp_p.Y), new Point(0, curr.pic_p.Y));
                       
                        points.Enqueue(newPoint);
                      
                    }
                    else
                    {
                        pnt newPoint = new pnt(new Point(curr.bmp_p.X + 1, curr.bmp_p.Y), new Point(curr.pic_p.X + 1, curr.pic_p.Y));
                        
                        points.Enqueue(newPoint);
                    }


                    if (curr.pic_p.Y-1==-1)
                    {
          
                            pnt newPoint = new pnt(new Point(curr.bmp_p.X, curr.bmp_p.Y - 1), new Point(curr.pic_p.X, imgBmp.Height-1));
                      
                        points.Enqueue(newPoint);
            
                     
                    }
                    else
                    {
                        pnt newPoint = new pnt(new Point(curr.bmp_p.X, curr.bmp_p.Y - 1), new Point(curr.pic_p.X, curr.pic_p.Y - 1));
                    
                        points.Enqueue(newPoint);
                    }


                    if (curr.pic_p.Y+1==imgBmp.Height)
                    {
                  
                            pnt newPoint = new pnt(new Point(curr.bmp_p.X, curr.bmp_p.Y+1), new Point(curr.pic_p.X, 0));
                        points.Enqueue(newPoint);
 
                    
                    }
                    else
                    {
                        pnt newPoint = new pnt(new Point(curr.bmp_p.X, curr.bmp_p.Y + 1), new Point(curr.pic_p.X, curr.pic_p.Y + 1));
                        points.Enqueue(newPoint);
                    }

                }
            }
            pictureBox1.Image = bmp;
            pictureBox1.Invalidate();
        }



        private bool CheckCoords(int x, int y)
        {
            return x > 0 && y > 0 && x < pictureBox1.Width && y < pictureBox1.Height;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;


                imgBmp = new Bitmap(filePath);
                imgZalivka = true;
                pictureBox2.Image = imgBmp;
            }

            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;


                imgBound= new Bitmap(filePath);
                isimgBound = true;
                pictureBox1.Image = imgBound;
            }

            //pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
        }
    }
}
