using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
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
        public Form3()
        {
            InitializeComponent();
            g=CreateGraphics();
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
        }

       
     

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (isfirst)
            {
                isfirst = false;
                x1 = e.Location.X;
                y1 = e.Location.Y;
            }
            else
            {
                isfirst = true;
                x2 = e.Location.X;
                y2 = e.Location.Y;
                CheckMethod();
                pictureBox1.Invalidate();   
                
              
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {

           pictureBox1.Invalidate();
           pictureBox1.Image = bmp;
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
                Pen p = new Pen(Color.Red,1);

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
            else {
                    int xtemp1 = x1;
                    int xtemp2 = x2;
                    int ytemp1 = y1;
                    int ytemp2 = y2;
                    if (Math.Abs(x2 - x1) < Math.Abs(y2 - y1))
                    {
                        (x1, y1) = (y1, x1);
                        (x2,y2) = (y2,x2);
                    }
                    if (x1 > x2)
                    {
                        (x1, x2) = (x2, x1);
                        (y1, y2) = (y2, y1);
                    }
                    float dx = x2 - x1;
                    float dy = y2 - y1;
                    float gradient = dy / dx;
                    float y = y1 + gradient;
                    for (var x = x1 + 1; x <= x2 - 1; x++)
                    {
                        DrawPoint(Math.Abs(xtemp2 - xtemp1) < Math.Abs(ytemp2 - ytemp1) ? (int)y : x, Math.Abs(xtemp2 - xtemp1) < Math.Abs(ytemp2 - ytemp1) ? x : (int)y, 1 - (y - (int)y));
                        DrawPoint(Math.Abs(xtemp2 - xtemp1) < Math.Abs(ytemp2 - ytemp1) ? (int)y + 1 : x, Math.Abs(xtemp2 - xtemp1) < Math.Abs(ytemp2 - ytemp1) ? x : (int)y + 1, y - (int)y);
                        y += gradient;
                    }      
            }
        }


        void DrawPoint(int x, int y, float intensive)
        {
            Color col = Color.Black;
            bmp.SetPixel(x, y, Color.FromArgb(255, (int)(col.R * (1 - intensive)), (int)(col.G * (1 - intensive)), (int)(col.B * (1 - intensive))));
            pictureBox1.Image = bmp;
            pictureBox1.Invalidate();
        }

        private void CheckMethod() {
            if (radioButton1.Checked)
                isBresen = true;
            else
                isBresen = false;
            
        }



        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {

        }

       



    }
}
