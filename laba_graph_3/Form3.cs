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
            /*
                if (x2 < x1)
                {
                    (x1, x2) = (x2, x1);
                    (y1, y2) = (y2, y1);
                }

                float dx = x2 - x1;
                float dy = y2 - y1;
                float gr = dy / dx;

                float xend = (float)Math.Round((double)x1);
                float yend = y1 + gr * (xend - x1);
                float xgapg = 1 - (x1 - (float)Math.Floor((double)x1));
                float xpxl1 = xend;
                float ypxl1 = (float)Math.Floor((double)yend);
                Color color = Color.FromArgb((int)(255 * (1 - (yend - (float)Math.Floor((double)yend)) * xgapg)), Color.Black);
                bmp.SetPixel((int)xpxl1, (int)ypxl1, color);
                e.Graphics.DrawRectangle(new Pen(color), xpxl1, ypxl1, 1, 1);
                bmp.SetPixel((int)xpxl1, (int)ypxl1 + 1, color);
                e.Graphics.DrawRectangle(new Pen(color), xpxl1, ypxl1 + 1, 1, 1);

                float intery = yend + gr;


                xend = (float)Math.Round((double)x2);
                yend = y2 + gr * (xend - x2);
                float xgap = (x2 - (float)Math.Floor((double)x2 + 0.5));
                float xpxl2 = xend;
                float ypxl2 = (float)Math.Floor((double)yend);

                bmp.SetPixel((int)xpxl2, (int)ypxl2, color);
                e.Graphics.DrawRectangle(new Pen(color), xpxl2, ypxl2, 1, 1);
                bmp.SetPixel((int)xpxl2, (int)ypxl2 + 1, color);
                e.Graphics.DrawRectangle(new Pen(color), xpxl2, ypxl2 + 1, 1, 1);


                Color color1 = Color.FromArgb((int)(255 * (1 - (yend - (float)Math.Floor((double)yend)) * xgap)), Color.Black);
                Color color2 = Color.FromArgb((int)(255 * ((yend - (float)Math.Floor((double)yend)) * xgap)), Color.Black);

                for (float x = xpxl1 + 1; x < xpxl2 - 1; x++)
                {
                    bmp.SetPixel((int)x, (int)(float)Math.Floor((double)intery), color1);
                    bmp.SetPixel((int)x, (int)(float)Math.Floor((double)intery) + 1, color2);
                    e.Graphics.DrawRectangle(new Pen(color1), x, (float)Math.Floor((double)intery), 1, 1);
                    e.Graphics.DrawRectangle(new Pen(color2), x, (float)Math.Floor((double)intery) + 1, 1, 1);
                    intery += gr;
                }

                //int x = x1;
                //int y = y1;
                //int Dx = x2 - x1;
                //int Dy = y2 - y1;
                //int e_t = 2 * Dy - Dx;
                //float d;
                //SolidBrush b1, b2;


                //for (int i = 1; i <= Dx; i++)
                //{
                //    d = -1F * e_t / (Dy + Dx) / 1.15F;
                //    if (e_t >= 0)
                //    {
                //        b1 = new SolidBrush(SetColor(1F / 2 - d));
                //        b2 = new SolidBrush(SetColor(1F / 2 + d));
                //        e.Graphics.FillRectangle(b1, x, y, 1, 1);
                //        e.Graphics.FillRectangle(b2, x, y + 1, 1, 1);


                //        bmp.SetPixel(x, y, b1.Color);
                //        bmp.SetPixel(x,y+1, b2.Color);

                //        y++;
                //        e_t += -2 * Dx + 2 * Dy;
                //    }
                //    else
                //    {
                //        b1 = new SolidBrush(SetColor(1F / 2 + d));
                //        b2 = new SolidBrush(SetColor(1F / 2 - d));
                //        e.Graphics.FillRectangle(b2, x, y, 1, 1);
                //        e.Graphics.FillRectangle(b1, x, y - 1, 1, 1);
                //        bmp.SetPixel(x, y, b1.Color);
                //        bmp.SetPixel(x, y - 1, b2.Color);
                //        e_t += 2 * Dy;
                //    }
                //    x++;
                //    b1.Dispose();
                //    b2.Dispose();
                //}
                */

            }
        }

        private Color SetColor(float t)
        {
            int c = Math.Min(Math.Max(0,Convert.ToInt32(255 * t)), 255);
            Color res = Color.FromArgb(c, c, c);
            return res;
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
