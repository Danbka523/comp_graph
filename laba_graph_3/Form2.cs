using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
        Bitmap imgBmp;
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

                Queue<Point> points = new Queue<Point>();
                Point curr = e.Location;
                points.Enqueue(curr);
                Bitmap bmp = (Bitmap)pictureBox1.Image;

                while (points.Count != 0) { 
                    curr = points.Dequeue();
                    if (!CheckCoords(curr.X, curr.Y))
                        continue;
                    if (bmp.GetPixel(curr.X, curr.Y).ToArgb() != z.Color.ToArgb() && bmp.GetPixel(curr.X, curr.Y).ToArgb() != p.Color.ToArgb()  ) {
                        bmp.SetPixel(curr.X, curr.Y, z.Color);
                        points.Enqueue(new Point(curr.X+1,curr.Y));
                        points.Enqueue(new Point(curr.X - 1, curr.Y));
                        points.Enqueue(new Point(curr.X, curr.Y+1));
                        points.Enqueue(new Point(curr.X, curr.Y-1));
                    }
                
                }

                pictureBox1.Image = bmp;
                pictureBox1.Invalidate();

            }

            if (imgZalivka)
            {
                imgZalivka = false;
                FillPicture(e.X, e.Y);

            }


        }

        private void FillPicture(int x, int y)
        {

            int centX = x;
            int centY = y;
            int leftBorder;
            Bitmap bmp = (Bitmap)pictureBox1.Image;
            Color backColor = bmp.GetPixel(x, y);
            while (bmp.GetPixel(x, y).ToArgb() == backColor.ToArgb() && x > 0)
                x--;

            leftBorder = ++x;
            while (bmp.GetPixel(x, y).ToArgb() == backColor.ToArgb() && x < pictureBox1.Width - 1)
            {
                try { bmp.SetPixel(x, y, imgBmp.GetPixel(x - centX + imgBmp.Width / 2, y - centY + imgBmp.Height / 2)); }
                catch (Exception t) { bmp.SetPixel(x, y, p.Color); }

                x++;
            }
            pictureBox1.Image = bmp;
            pictureBox1.Invalidate();
            x = leftBorder;

            while ( (bmp.GetPixel(x, y).ToArgb() == imgBmp.GetPixel(x - centX + imgBmp.Width / 2, y - centY + imgBmp.Height / 2).ToArgb()) && y > 0 && y < pictureBox1.Height - 1)
            {
                if (bmp.GetPixel(x, y - 1).ToArgb() == backColor.ToArgb())
                    FillPicture(x, y - 1);

                if (bmp.GetPixel(x, y + 1).ToArgb() == backColor.ToArgb())
                    FillPicture(x, y + 1);

                ++x;
            }
          
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
            }

            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
