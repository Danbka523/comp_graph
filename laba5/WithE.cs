
using laba4;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace laba5
{
    public partial class WithE : Form
    {
        private List<PointF> controlPoints = new List<PointF>();
        private List<PointF> points = new List<PointF>();
        private Pen controlPointPen = new Pen(Color.Blue, 5f);
        private Pen bezierCurvePen = new Pen(Color.Red, 2f);
        bool isMoving;
        laba4.Matrix BezMatrix;
        public WithE()
        {
            InitializeComponent();
            BezMatrix = new laba4.Matrix(4, 4);
            BezMatrix.Fill(1, -3, -3, -1, 0, 3, -6, 3, 0, 0, 3, -3, 0, 0, 0, 1);
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                controlPoints.Add(e.Location);
                points.Clear();
            }
            if (e.Button == MouseButtons.Middle)
            {
                int t = controlPoints.FindIndex(x => Math.Abs(x.X - e.Location.X) <= 5 && Math.Abs(x.Y - e.Location.Y) <= 5);
                if (t != -1)
                {
                    controlPoints.RemoveAt(t);
                    points.Clear();
                }
            }
            pictureBox1.Invalidate();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {

            DrawPointControls(e.Graphics);
            if (controlPoints.Count > 3)
            {
                DrawBezier(e.Graphics);
            }
        }

        private void DrawPointControls(Graphics g)
        {
            controlPoints.ForEach(x => { g.DrawEllipse(controlPointPen, x.X - 3, x.Y - 3, 6, 6); });
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int t = controlPoints.FindIndex(x => Math.Abs(x.X - e.Location.X) <= 5 && Math.Abs(x.Y - e.Location.Y) <= 5);
                if (t != -1)
                    isMoving = true;

                int t_d = points.FindIndex(x => Math.Abs(x.X - e.Location.X) <= 5 && Math.Abs(x.Y - e.Location.Y) <= 5);
                if (t_d != -1)
                    Debug.WriteLine(points[t_d]);

            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (isMoving)
                {
                    int t = controlPoints.FindIndex(x => Math.Abs(x.X - e.Location.X) <= 5 && Math.Abs(x.Y - e.Location.Y) <= 5);
                    if (t != -1)
                        controlPoints[t] = new PointF(e.X, e.Y);
                    pictureBox1.Invalidate();
                }
                points.Clear();
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isMoving = false;
        }

        private void DrawBezier(Graphics g)
        {
            List<PointF> newControl = new List<PointF>();

            newControl.Add(controlPoints[0]);
            newControl.Add(controlPoints[1]);
            newControl.Add(controlPoints[2]);

            for (int i = 3; i < controlPoints.Count; i++)
            {
                if (i != controlPoints.Count - 1)
                {
                    if (i % 2 == 0)
                        newControl.Add(controlPoints[i]);
                    else
                    {
                        PointF p = new PointF(controlPoints[i - 1].X + (controlPoints[i].X - controlPoints[i - 1].X) / 2, controlPoints[i - 1].Y + (controlPoints[i].Y - controlPoints[i - 1].Y) / 2);
                        newControl.Add(p);
                        newControl.Add(controlPoints[i]);
                    }   

                }
                else
                {
                    if (i % 2 == 0) {
                        newControl.Add(controlPoints[i]);
                        newControl.Add(controlPoints[i]);
                    }
                    else
                        newControl.Add(controlPoints[i]);
                
                }
            }

            List<PointF> drawingPoints = new List<PointF>();
            PointF p1, p2, p3, p4;

            for (int i = 0; i < newControl.Count-3; i+=3)
            {
                p1 = newControl[i];
                p2 = newControl[i+1];
                p3 = newControl[i+2];
                p4 = newControl[i+3];

                int N = 100;
                float dt = 1f / N;
                float t = 0f;

                for (int j = 0; j <= N; j++)
                {
                    drawingPoints.Add(GenPoint(t, p1, p2, p3, p4));
                    t += dt;
                }

            }
            g.DrawLines(bezierCurvePen, drawingPoints.ToArray());
       pictureBox1.Invalidate();
            

        }

        private PointF GenPoint(float t,PointF p0, PointF p1, PointF p2, PointF p3)
        {

            float x = (1 - t) * (1 - t) * (1 - t) * p0.X + (1 - t) * (1 - t) * 3 * t * p1.X + (1 - t) * t * 3 * t * p2.X + t * t * t * p3.X;
            float y = (1 - t) * (1 - t) * (1 - t) * p0.Y + (1 - t) * (1 - t) * 3 * t * p1.Y + (1 - t) * t * 3 * t * p2.Y + t * t * t * p3.Y;
            return new PointF(x, y);

            //}
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

    }
}
