using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace laba_graph_1
{
    public partial class Form2 : Form
    {
        private Graphics g;
        //Form1 frm1;
        float x1, x2;
        //string func;
        Func<double, double> func;
        //List<double> values;
        double h = 0.1;
        // private int top, bottom, left, right;
        float graph_h;

        public Form2(string x1, string x2, Func<double, double> func)
        {
            InitializeComponent();
            g = CreateGraphics();
            //frm1.ShowDialog(this);
            this.x1 = float.Parse(x1);
            this.x2 = float.Parse(x2);
            this.func = func;
            //g.TranslateTransform(Width/2, graph_h/2);
            //Matrix myMatrix = new Matrix(1, 0, 0, -1, 0, 0);
            //g.Transform = myMatrix;
            graph_h = (float)(graph_h - 10);
        }

        private void Form2_Resize(object sender, EventArgs e)
        {
            graph_h = (float)(Height - 50);
             Invalidate();
        }

        private void Form2_ResizeEnd(object sender, EventArgs e)
        {
           // Location = new Point(Width / 2, graph_h / 2);
            ShowGraph(g, func);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
             ShowGraph(g,func);
            //g.TranslateTransform(Width / 2, graph_h / 2, MatrixOrder.Append);
        }

        private void ShowGraph(Graphics g, Func<double, double> func)
        {
           // g.TranslateTransform(Width / 2, graph_h / 2, MatrixOrder.Append);
            Pen p = new Pen(Color.Black);
            g.Clear(Color.White);
            //Оси
            Font font = new Font("Courier New", 12, FontStyle.Bold);
            SolidBrush b = new SolidBrush(Color.Blue);

           // g.DrawLine(p, 0, graph_h, Width, graph_h);
            //g.DrawLine(p, new Point(Width / 2, 0), new PointF(Width / 2, Height));
            label1.Text = graph_h.ToString();
            label2.Text = Width.ToString();
          //  if (x1 != 0)
           //     g.DrawString(x1.ToString(), font, b, 0f, graph_h / 2 + 3);
           // g.DrawString("0", font, b, Width / 2, graph_h / 2);
           // g.DrawString(x2.ToString(), font, b, Width - 30, graph_h / 2 + 3);

            //Точки
            p.Color = Color.Red;
            double curx = 0;
            //double cury = func(x1);

            //double max_y = cury;
            //double min_y = cury;
            //double offx = GetOffsetX(x1, x2);

            List<PointF> points = new List<PointF>();
            List<double> points_x = new List<double>();
            List<double> points_y = new List<double>();

            //points_x.Add(curx);
            //points_y.Add(cury);

            for (double i = x1; i <= x2; i += h)
            {

                //PointF p1 = new PointF((float)curx, (float)cury);
                //double res = func(i);
                //curx += offx*h;
                //cury += res+offy;// +offx;
                //points.Add(p1);
                ////Debug.WriteLine(curx);
                //Debug.WriteLine(cury);
                ////Debug.WriteLine(offx);
                ///

                var res = func(i);
                //if (res > max_y)
                //    max_y = res;
                //if (res < min_y)
                //    min_y = res;

                points_x.Add(i);
                points_y.Add(res);

            }

            //double offy = GetOffsetY(min_y, max_y);
            double rangeX = Math.Abs(Math.Max(x1, x2) - Math.Min(x1, x2));
            double rangeY = points_y.Max() - points_y.Min();

            // Debug.WriteLine(points_y.Max());

            double scaleX = Width / rangeX;
            double scaleY = graph_h / rangeY;

           
            double y_t=points_y.Max()/graph_h;

            //Debug.WriteLine(offx+" "+offy);

            PointF[] pnts = new PointF[points_x.Count];
            bool isDrawedX = false;
            bool isDrawedY = false;
            for (int i = 0; i < points_x.Count; i++)
            {
                PointF pnt = new PointF((float)((points_x[i] - points_x.Min())* scaleX), (float)(graph_h * (1 - (points_y[i]-points_y.Min())/rangeY)));
                //PointF pnt1 = new PointF((float)points_x[i], (float)points_y[i]);
                //pnts[i] = pnt1;
                points.Add(pnt);
                if (points_x[i] <= h && points_x[i] >= -h) {
                    if (!isDrawedX)
                    {
                        g.DrawLine(new Pen(Color.Black), pnt.X, 0, pnt.X, Height);
                        isDrawedX = true;
                    }
                }
                if (points_y[i] <= h && points_y[i] >= -h)
                {
                    if (!isDrawedY)
                    {
                        g.DrawLine(new Pen(Color.Black), 0, pnt.Y, Width, pnt.Y);
                        isDrawedY = true;
                    }
                }
                Debug.WriteLine(points_x[i]);
                //g.DrawRectangle(p, pnt.X, pnt.Y, 10, 10);
            }

           // g.DrawLine(p, new PointF(0f, (float)(graph_h + 2 * (1 - points_y.Min() / rangeY))), new PointF(0f, (float)(graph_h + 2 * (1 - points_y.Min() / rangeY))));
            //g.Transform.Scale((float)rangeX, (float)rangeY);
            g.DrawLines(p, points.ToArray());

        }


        private double GetOffsetX(double x1, double x2)
        {
            return Width / (Math.Round(x2) - Math.Round(x1));
        }

        private double GetOffsetY(double y1, double y2)
        {
            return graph_h / (Math.Round(y2) - Math.Round(y1));
        }

        private void Form2_Click(object sender, EventArgs e)
        {

            //ShowGraph(g);
        }

        private void Form2_Paint(object sender, PaintEventArgs e)
        {

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            ShowGraph(e.Graphics, func);
        }
    }
}
