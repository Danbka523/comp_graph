using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace laba8
{
    using FastBitmap;
    internal class Drawing
    {
        Graphics g;
        Pen figureDrawPen;
        Pen highLightPen;
        FastBitmap fbmp;
        PictureBox pictureBox;
        Camera camera;
        public List<Polyhedron> sceneFigures= new();
        public Polyhedron figure;
        public int highLightedIdx;

        public Drawing(Graphics g, PictureBox pictureBox, Camera camera)
        {
            this.pictureBox = pictureBox;
            figureDrawPen = new Pen(Color.Black, 5);
            highLightPen = new Pen(Color.Red, 5);
            this.g = g;
            this.g.Clear(Color.White);
            this.camera = camera;
        }

        void DrawFigure(Polyhedron figure, Pen p)
        {
            foreach (Polygon poly in figure.Polygons)
            {
                DrawPoly(poly, p);
            }
        }

        void DrawPoly(Polygon p, Pen pen)
        {
            foreach (var line in p.Lines)
            {
                DrawLine(line, pen);
            }
        }

        void DrawLine(Line l, Pen p)
        {
            var p1 = l.Start.Projection(camera).Item1;
            var p2 = l.End.Projection(camera).Item1;
            if (p1.HasValue && p2.HasValue)
                DrawVuLine(p1,p2,p.Color);


        }
        #region VU
        void DrawVuLine(PointF? p1, PointF? p2, Color color) {
            int x1 = (int) p1.Value.X;
            int y1 = (int) p1.Value.Y;
            int x2 = (int) p2.Value.X;
            int y2 = (int) p2.Value.Y;

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
                    DrawPixel((int)y, x, GetInterpolation((float)fpart(y)),color);
                    DrawPixel((int)y + 1, x, GetInterpolation((float)((1 - fpart(y)))),color);
                }
                else
                {
                    DrawPixel(x, (int)y, GetInterpolation((float)(fpart(y))), color);
                    DrawPixel(x, (int)y + 1, GetInterpolation((float)((1 - fpart(y)))), color);
                }

                y += gradient;
            }

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

        float GetInterpolation(float value)
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

        void Swap(ref int x, ref int y)
        {
            (x, y) = (y, x);
        }
        #endregion
        void DrawPixel(int x, int y,float intensity, Color color) {
 
            Color pixelColor = Color.FromArgb((int)(255 * (1 - intensity)), color);
            fbmp.SetPixel(new System.Drawing.Point(x,y), pixelColor);
        }
        public void DrawAxis()
        {
            Line axisX = new Line(new Point(0, 0, 0), new Point(300, 0, 0));
            Line axisY = new Line(new Point(0, 0, 0), new Point(0, 300, 0));
            Line axisZ = new Line(new Point(0, 0, 0), new Point(0, 0, 300));

            DrawLine(axisX, new Pen(Color.Red, 5));
            DrawLine(axisY, new Pen(Color.Green, 5));
            DrawLine(axisZ, new Pen(Color.Blue, 5));


        }

        public void ReDraw(bool isShowAxis)
        {
            var bmp = new Bitmap(pictureBox.Width, pictureBox.Height);
            fbmp = new FastBitmap(bmp);
            g.Clear(Color.White);
            if (isShowAxis)
                DrawAxis();

            for (int i = 0; i < sceneFigures.Count; i++)
            {
                if (i == highLightedIdx)
                    DrawFigure(sceneFigures[i], highLightPen);
                else
                    DrawFigure(sceneFigures[i], figureDrawPen);

            }

            fbmp.Dispose();
            pictureBox.Image = bmp;              
            //pictureBox.Invalidate();
        }

        public void AddToScene(Polyhedron p) => sceneFigures.Add(p);
        public void RemoveInScene(Polyhedron p) => sceneFigures.Remove(p);
    
    }
}
