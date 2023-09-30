using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace laba_graph_3
{
    public partial class Form3 : Form
    {
        bool isfirst = true;
        bool isBresen = true;
        int x1, x2, y1, y2;

        Graphics g;
        Bitmap bmp;
        private Color foreColor;
        public Form3()
        {
            InitializeComponent();
            g = CreateGraphics();
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
        }




        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            CheckMethod();
            if (isfirst)
            {
                isfirst = false;
                x1 = e.Location.X;
                y1 = e.Location.Y;
                //pictureBox1.Invalidate();
            }
            else
            {
                isfirst = true;
                x2 = e.Location.X;
                y2 = e.Location.Y;
                pictureBox1.Invalidate();
                // CheckMethod();


            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {

            pictureBox1.Invalidate();
            pictureBox1.Image = bmp;
        }

        int ipart(double x) { return (int)x; }

        int round(double x) { return ipart(x + 0.5); }

        double fpart(double x)
        {
            if (x < 0) return (1 - (x - Math.Floor(x)));
            return (x - Math.Floor(x));
        }

        double rfpart(double x)
        {
            return 1 - fpart(x);
        }



        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (isBresen)
            {

                int dx = Math.Abs(x2 - x1);
                int dy = Math.Abs(y2 - y1);
                int sx = (x1 < x2) ? 1 : -1;
                int sy = (y1 < y2) ? 1 : -1;
                int err = dx - dy;
                Pen p = new Pen(Color.Red, 1);

                while (true)
                {
                    bmp.SetPixel(x1, y1, Color.Red);
                    e.Graphics.DrawRectangle(p, x1, y1, 1, 1);
                    //SetPixel(x0, y0, Color.Black);

                    if (x1 == x2 && y1 == y2)
                    {
                        break;
                    }

                    int e2 = err * 2;

                    if (e2 > -dy)
                    {
                        err -= dy;
                        x1 += sx;
                    }

                    if (e2 < dx)
                    {
                        err += dx;
                        y1 += sy;
                    }
                }
                // pictureBox1.Image = bmp;
            }
            else
            {
                if (isfirst)
                {
                    bool steep = Math.Abs(y2 - y1) > Math.Abs(x2 - x1);
                    if (steep)
                    {
                        Swap(ref x1, ref y1);
                        Swap(ref x2, ref y2);
                    }

                    if (x1 > x2)
                    {
                        Swap(ref x1, ref x2);
                        Swap(ref y1, ref y2);
                    }

                    int dx = x2 - x1;
                    int dy = y2 - y1;
                    float gradient = (float)dy / dx;
                    float xgapg = (float)(1 - fpart(x1 + 0.5));
                    float y = y1 + gradient;

                    for (int x = x1 + 1; x < x2; x++)
                    {
                        if (steep)
                        {
                            DrawPixel(bmp, (int)y, x, GetInterpolation((float)fpart(y)));
                            DrawPixel(bmp, (int)y + 1, x, GetInterpolation((float)((1 - fpart(y)))));
                        }
                        else
                        {
                            DrawPixel(bmp, x, (int)y, GetInterpolation((float)(fpart(y))));
                            DrawPixel(bmp, x, (int)y + 1, GetInterpolation((float)((1 - fpart(y)))));
                        }

                        y += gradient;
                    }
                    pictureBox1.Image = bmp;
                    pictureBox1.Invalidate();
                }
            }

        }



        private void DrawPixel(Bitmap bitmap, int x, int y, float intensity)
        {
            if (x >= 0 && x < bmp.Width && y >= 0 && y < bmp.Height)
            {
                Color pixelColor = Color.FromArgb((int)(255 * (1 - intensity)), Color.Black);
                bmp.SetPixel(x, y, pixelColor);
            }
            
        }

        private float GetInterpolation(float value)
        {
            if (value <= 0.0)
            {
                return 0.0f;
            }
            else if (value >= 1.0)
            {
                return 1.0f;
            }
            else
            {
                return value;
            }
        }

        private void Swap(ref int x, ref int y)
        {
            int step = y;
            y = x;
            x = step;
        }





        private void CheckMethod()
        {
            if (radioButton1.Checked)
                isBresen = true;
            else
                isBresen = false;

        }

    }
}
