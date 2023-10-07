using Microsoft.Win32;
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

namespace laba4
{
    public partial class Form1 : Form
    {

        bool isDrawing;
        bool isDot;
        bool isPol;
        bool isSec;
        List<PointF> points;
        bool isClear;
        bool secFirst;
        bool isRot;
        bool isScale;
        bool isCheckSec;
        bool isPolyClass;
        Pen blackPen;
        Brush blackBrush;
        Brush redBrush;

        public Form1()
        {
            InitializeComponent();
            blackPen = new Pen(Color.Black);
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            points = new List<PointF>();
            blackBrush = new SolidBrush(Color.Black);
            secFirst = true;
            redBrush = new SolidBrush(Color.Red);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            isDrawing = true;
            button2.Enabled = false;
            InitMode();

        }

        private void InitMode()
        {
            string t = comboBox1.Text;
            switch (t)
            {
                case "Точка":
                    isDot = true;
                    isSec = false;
                    isPol = false;
                    break;
                case "Отрезок":
                    isDot = false;
                    isSec = true;
                    isPol = false;
                    break;
                case "Полигон":
                    isDot = false;
                    isSec = false;
                    isPol = true;
                    break;
                default:
                    break;
            }


        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (isDrawing)
            {
                if (isDot)
                {
                    DrawDot(e);
                }
                if (isSec)
                {

                    DrawSec(e);

                }
                if (isPol)
                {
                    Drawpoints(e);

                }


            }

            if (isRot)
            {
                RotateAroundPoint(e.Location, double.Parse(degreeText.Text));
                RedrawPoly();
                isRot = false;
            }
            if (isScale)
            {
                float scaleX = float.Parse(ScaleX.Text);
                float scaleY = float.Parse(ScaleY.Text);
                ScaleAroundPoint(e.Location, scaleX, scaleY);
                RedrawPoly();
                isScale = false;
            }
            if (isCheckSec) {
                using (Graphics g = Graphics.FromImage(pictureBox1.Image))
                {
                    g.FillEllipse(redBrush, e.X, e.Y,3,3);
                }
                pictureBox1.Invalidate();
                CheckPointSec(e.Location);
                isCheckSec = false;
            }
            if (isPolyClass) {
                using (Graphics g = Graphics.FromImage(pictureBox1.Image))
                {
                    g.FillEllipse(redBrush, e.X, e.Y, 3, 3);
                }
                pictureBox1.Invalidate();
                CheckPolyAndPoint(e.Location);
                isPolyClass = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            isClear = true;
            points.Clear();
            button2.Enabled = true;
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            PointF center = new PointF((points[0].X + points[1].X) / 2, (points[0].Y + points[1].Y) / 2);
            RotateAroundPoint(center, 90);
            using (Graphics g = Graphics.FromImage(pictureBox1.Image))
            {
                g.Clear(Color.White);
                g.DrawLine(blackPen, points[0], points[1]);
            }
            pictureBox1.Invalidate();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            isSec = true;
            isDrawing = true;

        }



        private void button6_Click(object sender, EventArgs e)
        {
            int x = int.Parse(shiftDx.Text);
            int y = int.Parse(shiftDy.Text);
            Shift(x, y);
            RedrawPoly();

        }


        private void button8_Click(object sender, EventArgs e)
        {
            isRot = true;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            PointF polyCenter = GetCenter();
            if (isRot)
            {
                RotateAroundPoint(polyCenter, double.Parse(degreeText.Text));
                isRot = false;
            }
            if (isScale)
            {
                float scaleX = float.Parse(ScaleX.Text);
                float scaleY = float.Parse(ScaleY.Text);
                ScaleAroundPoint(polyCenter, scaleX, scaleY);
                isScale = false;

            }
            RedrawPoly();
        }



        private void button7_Click(object sender, EventArgs e)
        {
            isScale = true;

        }

        #region Line
        private (float, float, float) GetCoefs(PointF p1, PointF p2)
        {
            float a = p1.Y - p2.Y;
            float b = p2.X - p1.X;
            float c = p1.X * p2.Y - p2.X * p1.Y;
            return (a, b, c);

        }

        private void button10_Click(object sender, EventArgs e)
        {

            (float a1, float b1, float c1) = GetCoefs(points[0], points[1]);
            (float a2, float b2, float c2) = GetCoefs(points[2], points[3]);
            float div = a1 * b2 - a2 * b1;

            float x = (b1 * c2 - b2 * c1) / div;
            float y = (c1 * a2 - c2 * a1) / div;


            using (Graphics g = Graphics.FromImage(pictureBox1.Image))
            {
                g.FillEllipse(redBrush, x - 3, y - 3, 5, 5);
            }
            pictureBox1.Invalidate();
        }

        #endregion

        #region Drawing
        private void DrawDot(MouseEventArgs e)
        {
            using (Graphics g = Graphics.FromImage(pictureBox1.Image))
            {
                g.FillEllipse(blackBrush, e.X, e.Y, 5, 5);
                points.Add(new Point(e.X, e.Y));
                isDrawing = false;
            }
            pictureBox1.Invalidate();


        }

        private void Drawpoints(MouseEventArgs e)
        {
            points.Add(e.Location);
            if (points.Count > 1)
            {
                using (Graphics g = Graphics.FromImage(pictureBox1.Image))
                {
                    g.DrawLine(blackPen, points[points.Count - 2], points[points.Count - 1]);
                    if (Math.Abs(points[0].X - e.X) + Math.Abs(points[0].Y - e.Y) <= 25)
                    {
                        g.DrawLine(blackPen, points[0], e.Location);
                        points.Add(points[0]);
                        isDrawing = false;
                    }
                }
                pictureBox1.Invalidate();

            }


        }
        private void RedrawPoly()
        {

            using (Graphics g = Graphics.FromImage(pictureBox1.Image))
            {
                g.Clear(Color.White);
                g.DrawLines(blackPen, points.ToArray());

            }
            pictureBox1.Invalidate();
        }

        private void DrawSec(MouseEventArgs e)
        {
            if (secFirst)
            {
                points.Add(e.Location);
                secFirst = false;
            }
            else
            {
                secFirst = true;
                points.Add(e.Location);
                using (Graphics g = Graphics.FromImage(pictureBox1.Image))
                {
                    g.DrawLine(blackPen, points[points.Count - 2], points[points.Count - 1]);
                    isDrawing = false;
                }
                pictureBox1.Invalidate();
            }

        }
        #endregion

        #region Affine
        private PointF GetCenter()
        {
            PointF res = PointF.Empty;
            res.X = points.Select(p => p.X).Average();
            res.Y = points.Select(p => p.Y).Average();
            return res;

        }

        private double DegreesToRadians(double angle)
        {
            return Math.PI * angle / 180.0;
        }

        private void RotateAroundPoint(PointF p, double angle)
        {
            using (Graphics g = Graphics.FromImage(pictureBox1.Image))
            {
                g.FillEllipse(redBrush, p.X, p.Y, 5, 5);
            }
            pictureBox1.Invalidate();
            for (int i = 0; i < points.Count; i++)
            {
                var shift1 = new Matrix(3, 3).FillAffine(1, 0, 0, 1, -p.X, -p.Y);
                /*
                 cos sin 0
                -sin cos 0
                 0   0   1
                 */
                var rotation = new Matrix(3, 3).FillAffine((float)Math.Cos(DegreesToRadians(angle)), (float)-Math.Sin(DegreesToRadians(angle)), (float)Math.Sin(DegreesToRadians(angle)), (float)Math.Cos(DegreesToRadians(angle)), 0, 0);
                var shift2 = new Matrix(3, 3).FillAffine(1, 0, 0, 1, p.X, p.Y);
                var vals = new Matrix(1, 3).Fill(points[i].X, points[i].Y, 1);
                var prom = (shift1 * rotation * shift2);
                var res = vals * prom;
                points[i] = new PointF(res[0, 0], res[0, 1]);
            }

        }

        private void Shift(int dx, int dy)
        {
            for (int i = 0; i < points.Count; i++)
            {
                var shift = new Matrix(3, 3).FillAffine(1, 0, 0, 1, dx, dy);
                var vals = new Matrix(1, 3).Fill(points[i].X, points[i].Y, 1);
                var res = vals * shift;
                points[i] = new PointF(res[0, 0], res[0, 1]);
            }

        }
        private void ScaleAroundPoint(PointF point, float dx, float dy)
        {

            for (int i = 0; i < points.Count; i++)
            {
                var shift1 = new Matrix(3, 3).FillAffine(1, 0, 0, 1, -point.X, -point.Y);
                var scaling = new Matrix(3, 3).FillAffine(dx, 0, 0, dy, 0, 0);
                var shift2 = new Matrix(3, 3).FillAffine(1, 0, 0, 1, point.X, point.Y);
                var vals = new Matrix(1, 3).Fill(points[i].X, points[i].Y, 1);
                var t = (shift1 * scaling * shift2);
                var res = vals * t;
                points[i] = new PointF(res[0, 0], res[0, 1]);
            }


        }


        #endregion

        #region PointClass
        private void CheckPointSec(PointF p) {
            PointF first = points[0];
            PointF last = points[1];
            float res = (p.X-first.X) * (last.Y - first.Y) - (p.Y-first.Y) * (last.X - first.X);
            if (res > 0)
                label9.Text = "Точка находится слева";
            else if (res < 0)
                label9.Text = "Точка находится справа";
            else
                label9.Text = "Вы попали точно в отрезок";
           
        
        }

        private void CheckConvex(PointF p) {
            PointF ab = new PointF(points[1].X - points[0].X, points[1].Y - points[0].Y);
            PointF bc = new PointF(points[2].X - points[1].X, points[2].Y - points[1].Y);

            int sign =Math.Sign(ab.X * bc.Y - ab.Y * bc.X);
            bool isLeft=false;
            bool isRight=false;

            if (sign <0)
                isLeft = true;
            else
                isRight = true;
            for (int i = 3; i < points.Count-1; i++)
            {
                ab = new PointF(points[i].X - points[i-1].X, points[i].Y - points[i-1].Y);
                bc = new PointF(points[i+1].X - points[i].X, points[i+1].Y - points[i].Y);

                sign = Math.Sign(ab.X * bc.Y - ab.Y * bc.X);
                if (sign < 0 && isRight || sign > 0 && isLeft) {
                    label10.Text = "Полигон невыпуклый";
                    return;
                }
            }

            label10.Text = "Полигон выпуклый";

        }

        private void CheckInPoly(PointF p) {
            bool res = false;
            int j = points.Count - 1;
            for (int i = 0; i < points.Count; i++)
            {
                if (points[i].Y < p.Y && points[j].Y >= p.Y ||
                    points[j].Y < p.Y && points[i].Y >= p.Y)
                {
                    if (points[i].X + (p.Y - points[i].Y) /
                       (points[j].Y - points[i].Y) *
                       (points[j].X - points[i].X) < p.X)
                    {
                        res = !res;
                    }
                }
                j = i;
            }
            if (res == true)
                label10.Text += "\n"+"Точка внутри полигона";
            else
                label10.Text += "\n" + "Точка вне полигона";
        }

        private void CheckPolyAndPoint(PointF p) {
            CheckConvex(p);
            CheckInPoly(p);
            
        }

        #endregion

        private void button4_Click(object sender, EventArgs e)
        {
            isPolyClass = true;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            isCheckSec = true;
        }
    }


}
