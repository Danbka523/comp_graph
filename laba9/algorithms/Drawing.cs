using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba7
{
    public enum DRAWINGKIND { NONFACIAL, ZBUF, NORMAL, LIGHT}
    internal class Drawing
    {

        public List<Polyhedron> scene=new();
        Bitmap bmp;
        Graphics g;
        public Camera cam;
        public Pen figureDrawPen;
        public Pen highlightPen;
        public DRAWINGKIND kind = DRAWINGKIND.NORMAL;
        Transformations transformations;
        PictureBox pb;
        public LightSource lightSource;
        List<Color> colors;
        int iColor = 0; 
        public Drawing(PictureBox pb, Graphics g) { 
            this.pb = pb;
            bmp = new Bitmap(pb.Width, pb.Height);
            transformations= new Transformations(); 
            this.g = g;
            cam = new Camera();
            lightSource=new LightSource(new Point(100,100,100));
            colors = GenColors();
        }

        void DrawFigure(Polyhedron figure, Pen p, Bitmap bmp)
        {
            foreach (var poly in figure.Polygons)
            {
                if (poly.isFacial)
                    DrawPoly(poly,p, bmp);
            }
        }

        void DrawPoly(Polygon poly, Pen p, Bitmap bmp)
        {
            for (int i = 0; i < poly.Verts.Count; i++)
            {
                DrawLine(poly.Verts[i], poly.Verts[(i+1)%poly.Verts.Count],p,bmp);    
            }
        }

        void DrawLine(Point p1, Point p2, Pen p,Bitmap bmp) {
            var pp1 = p1.Projection(cam);
            var pp2 = p2.Projection(cam);
            if (pp1.Item1.HasValue && pp2.Item1.HasValue)
                DrawVuLine(new PointF(pp1.Item1.Value.X, pp1.Item1.Value.Y), new PointF(pp2.Item1.Value.X, pp2.Item1.Value.Y),p.Color, bmp);
            
        }

        #region VU
        void DrawVuLine(PointF? p1, PointF? p2, Color color, Bitmap bmp)
        {
            int x1 = (int)p1.Value.X;
            int y1 = (int)p1.Value.Y;
            int x2 = (int)p2.Value.X;
            int y2 = (int)p2.Value.Y;

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
                    DrawPixel((int)y, x, GetInterpolation((float)fpart(y)), color,bmp);
                    DrawPixel((int)y + 1, x, GetInterpolation((float)((1 - fpart(y)))), color,bmp);
                }
                else
                {
                    DrawPixel(x, (int)y, GetInterpolation((float)(fpart(y))), color,bmp);
                    DrawPixel(x, (int)y + 1, GetInterpolation((float)((1 - fpart(y)))), color,bmp);
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

        void DrawPixel(int x, int y, float intensity, Color color, Bitmap bmp)
        {
            if (CanDraw(x, y))
            {
                Color pixelColor = Color.FromArgb((int)(255 * (1 - intensity)), color);
                bmp.SetPixel(x, y, pixelColor);
            }
        }

        bool CanDraw(int x, int y) => x > 0 && x < pb.Width && y > 0 && y < pb.Height;
        #endregion


        void DrawAxis(Bitmap bmp)
        {
            Line axisX = new Line(new Point(0, 0, 0), new Point(300, 0, 0));
            Line axisY = new Line(new Point(0, 0, 0), new Point(0, 300, 0));
            Line axisZ = new Line(new Point(0, 0, 0), new Point(0, 0, 300));

            DrawLine(axisX.Start,axisX.End, new Pen(Color.Red, 2),bmp);
            DrawLine(axisY.Start, axisY.End, new Pen(Color.Green, 2), bmp);
            DrawLine(axisZ.Start, axisZ.End, new Pen(Color.Blue, 2), bmp);


        }

        void FindNonFacial(Polyhedron fig) {
            foreach (var poly in fig.Polygons)
            {
                var vectProec = new Vector(cam.cameraPosition, poly.GetCenter()).Normalize();
                var cos = vectProec.Cos(poly.GetNorm().Normalize());
                Debug.WriteLine(cos);
                Debug.WriteLine(poly);
                poly.isFacial = cos > 0;
                
            }
            Debug.WriteLine("");
        }

        Bitmap DrawZbuffer(bool isLight=false,LightSource light=null)
        {
            return zBuffer.z_buf(pb.Width, pb.Height, scene, cam,colors,isLight,light);

        }

        List<Color> GenColors() {
            List<Color> res=new();
            Random r = new Random();
            for (int i = 0; i < 50; ++i)
                res.Add(Color.FromArgb(r.Next(0, 255), r.Next(0, 100), r.Next(10, 255)));
            return res;
        }

        public void ReDraw(bool isShowAxis) {
            g.Clear(Color.White);

            Bitmap bmp = new Bitmap(pb.Width, pb.Height);

            if (isShowAxis)
                DrawAxis(bmp);
            foreach (var fig in scene)
            {
                switch (kind)
                {
                    case DRAWINGKIND.NONFACIAL:
                        FindNonFacial(fig);
                        if (fig.isHighLighthed)
                            DrawFigure(fig, highlightPen,bmp);
                        else
                            DrawFigure(fig, figureDrawPen, bmp);
                        break;
                    case DRAWINGKIND.ZBUF:
                        bmp=DrawZbuffer();
                        break;
                    case DRAWINGKIND.LIGHT:
                        bmp = DrawZbuffer(true,lightSource);
                        break;
                    case DRAWINGKIND.NORMAL:
                        fig.ResetFacial();
                        if (fig.isHighLighthed)
                            DrawFigure(fig, highlightPen, bmp);
                        else 
                            DrawFigure(fig, figureDrawPen,bmp);
                        break;
                    default:
                        break;
                }
            }

            pb.Image= bmp;
        }
        public void AddToScene(Polyhedron polyhedron) { scene.Add(polyhedron); polyhedron.SetColor(colors[iColor++]); }
        public void RemoveFromScene(Polyhedron polyhedron) { scene.Remove(polyhedron); iColor -= 1; }

        public void ClearScene()
        {
            scene.Clear();
        }
    }
}
