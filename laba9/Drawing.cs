using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace laba9
{
    using FastBitmap;

    using System.Diagnostics;

    public enum DRAWINGKIND {CASUAL,NORMAL,ZBUFFER };
    internal class Drawing
    {
        Graphics g;
        Pen figureDrawPen;
        Pen highLightPen;
        FastBitmap fbmp;
        zBuffer zBuffer;
        PictureBox pictureBox;
        Camera camera;
        public List<Polyhedron> sceneFigures= new();
        public Polyhedron figure;
        public int highLightedIdx;

        public DRAWINGKIND DRAWINGKIND = DRAWINGKIND.CASUAL;
       

        public Drawing(Graphics g, PictureBox pictureBox, Camera camera)
        {
            zBuffer = new zBuffer();
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
                if (poly.isFacial)
                    DrawPoly(poly, p);
            }
        }

        void DrawPoly(Polygon p, Pen pen)
        {
            //foreach (var line in p.Lines)
            //{
            //    DrawLine(line, pen);
            //}
            for (var i = 0; i < p.Verts.Count; i++)
            {
                DrawLine(p.Verts[i], p.Verts[(i+1) % p.Verts.Count], pen);
            }
        }

        void DrawLine(Point start, Point end, Pen p)
        {
            var p1 = start.Projection(camera).Item1;
            var p2 = end.Projection(camera).Item1;
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

            DrawLine(axisX.Start,axisX.End, new Pen(Color.Red, 5));
            DrawLine(axisY.Start,axisY.End, new Pen(Color.Green, 5));
            DrawLine(axisZ.Start,axisZ.End, new Pen(Color.Blue, 5));


        }

        public void ReDraw(bool isShowAxis)
        {
            var bmp = new Bitmap(pictureBox.Width, pictureBox.Height);
            fbmp = new FastBitmap(bmp);
            g.Clear(Color.White);
            if (isShowAxis)
                DrawAxis();



            switch (DRAWINGKIND)
            {
                case DRAWINGKIND.CASUAL:                   
                    DrawCasual();              
                    break;
                case DRAWINGKIND.NORMAL:
                    DrawNormal();
                    break;
                case DRAWINGKIND.ZBUFFER:
                    DrawZBuffer();
                    break;
                default:
                    break;
            }
            pictureBox.Image = bmp;
            fbmp.Dispose();
            
            //bmp.Dispose();
            //pictureBox.Invalidate();
        }

        void DrawNormal()
        {

                for (int i = 0; i < sceneFigures.Count; i++)
                {
                FindNonFacial(sceneFigures[i]);
                    if (i == highLightedIdx)
                        DrawFigure((sceneFigures[i]), highLightPen);
                    else
                        DrawFigure((sceneFigures[i]), figureDrawPen);

                }
            
        }

        void FindNonFacial(Polyhedron figure) {

            foreach (var poly in figure.Polygons)
            {
                Vector vectProec = new Vector(camera.ToCameraView(poly.GetCenter()));
                //Debug.WriteLine("center " + camera.ToCameraView(poly.GetCenter()).ToString());
                //Debug.WriteLine("proec " +vectProec.ToString());
                //Debug.WriteLine(camera.position.ToString());
                Vector vectNormal = poly.NormVector;

              
                
                //Debug.WriteLine("norm? " + vectNormal.ToString());
                vectNormal = new Vector(camera.ToCameraView(new Point(vectNormal.XF, vectNormal.YF, vectNormal.ZF))).Normalize();
                float cos = vectProec.Cos(vectNormal);
                //Debug.WriteLine("norm? " + vectNormal.ToString());
                float vectScalar = vectNormal.Scalar(vectProec);
                ////float vectScalar = vectNormal.Scalar(camera.right.Normalize());
                //Debug.WriteLine("vectProec =" + vectProec.ToString());
                ////Debug.WriteLine("vectNormal =" + vectNormal.ToString());
                //Debug.WriteLine(vectScalar);
                //Debug.WriteLine(poly.ToString());


                //float cos = vectScalar / (vectProec.Abs()*vectNormal.Abs()); ;
                Debug.WriteLine(cos);

                poly.isFacial = cos > 0;
                if (!poly.isFacial)
                    Debug.WriteLine(poly.ToString());   
                //Debug.WriteLine("");
            }
            Debug.WriteLine("");

        }

        void DrawCasual() {
            for (int i = 0; i < sceneFigures.Count; i++)
            {
                sceneFigures[i].ResetFacial();
                if (i == highLightedIdx)
                    DrawFigure(sceneFigures[i], highLightPen);
                else
                    DrawFigure(sceneFigures[i], figureDrawPen);

            }
        }

        Bitmap DrawZBuffer() {
 
            return zBuffer.z_buf(pictureBox.Width, pictureBox.Height, sceneFigures, camera, GenerateColors(),fbmp);  
        }

        public void AddToScene(Polyhedron p) => sceneFigures.Add(p);
        public void RemoveInScene(Polyhedron p) => sceneFigures.Remove(p);

        List<Color> GenerateColors()
        {
            List<Color> res = new List<Color>();
            Random r;
            r = new Random();
            for (int i = 0; i < 50; ++i)
                res.Add(Color.FromArgb(r.Next(0, 255), r.Next(0, 100), r.Next(10, 255)));
            return res;
        }

    }
}
