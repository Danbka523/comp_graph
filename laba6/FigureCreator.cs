using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Layout;
using System.Windows.Forms.VisualStyles;

namespace laba6
{
    internal class FigureCreator
    {
        const int LEN = 150;
        Transformations transformations = new Transformations();
        public Polyhedron GetTetrahedron()
        {
            Polyhedron res = new Polyhedron();
            Point a = new Point(0, 0, 0);
            Point b = new Point(LEN, 0, LEN);
            Point c = new Point(LEN, LEN, 0);
            Point d = new Point(0, LEN, LEN);
            res.AddPolygon(new Polygon().Add(new Line(a, c)).Add(new Line(c, b)).Add(new Line(b, a)));
            res.AddPolygon(new Polygon().Add(new Line(c, b)).Add(new Line(b, d)).Add(new Line(d, c)));
            res.AddPolygon(new Polygon().Add(new Line(b, d)).Add(new Line(d, a)).Add(new Line(a, b)));
            res.AddPolygon(new Polygon().Add(new Line(c, d)).Add(new Line(d, a)).Add(new Line(a, c)));
            return res;

        }
        public Polyhedron GetHexahedron()
        {
            Polyhedron res = new Polyhedron();
            Point a = new Point(0, 0, 0);
            Point b = new Point(LEN, 0, 0);
            Point c = new Point(LEN, 0, LEN);
            Point d = new Point(0, 0, LEN);
            Point e = new Point(0, LEN, 0);
            Point f = new Point(LEN, LEN, 0);
            Point g = new Point(LEN, LEN, LEN);
            Point h = new Point(0, LEN, LEN);
            res.AddPolygon(new Polygon().Add(new Line(a, b)).Add(new Line(b, c)).Add(new Line(c, d)).Add(new Line(d, a))); 
            res.AddPolygon(new Polygon().Add(new Line(b, c)).Add(new Line(c, g)).Add(new Line(g, f)).Add(new Line(f, b))); 
            res.AddPolygon(new Polygon().Add(new Line(f, g)).Add(new Line(g, h)).Add(new Line(h, e)).Add(new Line(e, f)));
            res.AddPolygon(new Polygon().Add(new Line(h, e)).Add(new Line(e, a)).Add(new Line(a, d)).Add(new Line(d, h)));
            res.AddPolygon(new Polygon().Add(new Line(a, b)).Add(new Line(b, f)).Add(new Line(f, e)).Add(new Line(e, a)));
            res.AddPolygon(new Polygon().Add(new Line(d, c)).Add(new Line(c, g)).Add(new Line(g, h)).Add(new Line(h, d)));
            return res;
        }

        public Polyhedron GetOctahedron()
        {
            Polyhedron res = new Polyhedron();
            Polyhedron cube = GetHexahedron();

            Point a = cube.Polygons[0].GetCenter();
            Point b = cube.Polygons[1].GetCenter();
            Point c = cube.Polygons[2].GetCenter();
            Point d = cube.Polygons[3].GetCenter();
            Point e = cube.Polygons[4].GetCenter();
            Point f = cube.Polygons[5].GetCenter();

            res.AddPolygon(new Polygon().Add(new Line(a, f)).Add(new Line(f,b)).Add(new Line(b,a)));
            res.AddPolygon(new Polygon().Add(new Line(b,c)).Add(new Line(c,f)).Add(new Line(f,b)));
            res.AddPolygon(new Polygon().Add(new Line(c, d)).Add(new Line(d,f)).Add(new Line(f,c)));
            res.AddPolygon(new Polygon().Add(new Line(d,a)).Add(new Line(a,f)).Add(new Line(f,d)));
            res.AddPolygon(new Polygon().Add(new Line(a, e)).Add(new Line(e,b)).Add(new Line(b,a)));
            res.AddPolygon(new Polygon().Add(new Line(b, e)).Add(new Line(e,c)).Add(new Line(c,b)));
            res.AddPolygon(new Polygon().Add(new Line(c, e)).Add(new Line(e,d)).Add(new Line(d,c)));
            res.AddPolygon(new Polygon().Add(new Line(d, e)).Add(new Line(e,a)).Add(new Line(a,d)));


            return res;
        }



        public Polyhedron GetIcosahedron() {
            Polyhedron res = new Polyhedron();
            Point circleCenter = new Point(100, 100, 100);
            List<Point> circlePoints = new List<Point>();
            for (int angle = 0; angle < 360; angle += 36)
            {
                if (angle % 72 == 0)
                    circlePoints.Add(new Point((float)(circleCenter.X + (100 * Math.Cos(transformations.DegreeToRadian(angle)))), circleCenter.Y + 100, (float)(circleCenter.Z + (100 * Math.Sin(transformations.DegreeToRadian(angle))))));
                else
                    circlePoints.Add(new Point((float)(circleCenter.X + (100 * Math.Cos(transformations.DegreeToRadian(angle)))), circleCenter.Y, (float)(circleCenter.Z + (100 * Math.Sin(transformations.DegreeToRadian(angle))))));
            }
            Point a = new Point(100, 50, 100);
            Point b = new Point(100, 250, 100);
            for (int i = 0; i < 10; i++)
            {
                res.AddPolygon(new Polygon().Add(new Line(circlePoints[i], circlePoints[(i + 1) % 10])).Add(new Line(circlePoints[(i + 1) % 10], circlePoints[(i + 2) % 10])).Add(new Line(circlePoints[(i + 2) % 10], circlePoints[i])));
            }
            res.AddPolygon(new Polygon().Add(new Line(circlePoints[1], a)).Add(new Line(a, circlePoints[3])).Add(new Line(circlePoints[3], circlePoints[1])));
            res.AddPolygon(new Polygon().Add(new Line(circlePoints[3], a)).Add(new Line(a, circlePoints[5])).Add(new Line(circlePoints[5], circlePoints[3])));
            res.AddPolygon(new Polygon().Add(new Line(circlePoints[5], a)).Add(new Line(a, circlePoints[7])).Add(new Line(circlePoints[7], circlePoints[5])));
            res.AddPolygon(new Polygon().Add(new Line(circlePoints[7], a)).Add(new Line(a, circlePoints[9])).Add(new Line(circlePoints[9], circlePoints[7])));
            res.AddPolygon(new Polygon().Add(new Line(circlePoints[9], a)).Add(new Line(a, circlePoints[1])).Add(new Line(circlePoints[1], circlePoints[9])));

            res.AddPolygon(new Polygon().Add(new Line(circlePoints[0], b)).Add(new Line(b, circlePoints[2])).Add(new Line(circlePoints[2], circlePoints[0])));
            res.AddPolygon(new Polygon().Add(new Line(circlePoints[2], b)).Add(new Line(b, circlePoints[4])).Add(new Line(circlePoints[4], circlePoints[2])));
            res.AddPolygon(new Polygon().Add(new Line(circlePoints[4], b)).Add(new Line(b, circlePoints[6])).Add(new Line(circlePoints[6], circlePoints[4])));
            res.AddPolygon(new Polygon().Add(new Line(circlePoints[6], b)).Add(new Line(b, circlePoints[8])).Add(new Line(circlePoints[8], circlePoints[6])));
            res.AddPolygon(new Polygon().Add(new Line(circlePoints[8], b)).Add(new Line(b, circlePoints[0])).Add(new Line(circlePoints[0], circlePoints[8])));
            return res;
        }

        public Polyhedron GetDodecahedron() {
            Polyhedron res = new Polyhedron();
            Polyhedron icos = GetIcosahedron();

            List<Point> centers = new List<Point>();

            foreach (Polygon poly in icos.Polygons) {
                centers.Add(poly.GetCenter());
            }

            for (int i = 0; i < centers.Count/2; i++)
            {
                if (i % 2 == 0)
                {
                    res.AddPolygon(new Polygon().Add(new Line(centers[i], centers[(i + 1) % 10]))
                        .Add(new Line(centers[(i + 1) % 10], centers[(i + 2) % 10]))
                        .Add(new Line(centers[(i + 2) % 10], centers[15 + (i / 2 + 1) % 5]))
                        .Add(new Line(centers[15 + (i / 2 + 1) % 5], centers[15 + i / 2]))
                        .Add(new Line(centers[15 + i / 2], centers[i])));

                    continue;
                }
                res.AddPolygon(new Polygon().Add(new Line(centers[i], centers[(i + 1) % 10]))
                    .Add(new Line(centers[(i + 1) % 10], centers[(i + 2) % 10]))
                    .Add(new Line(centers[(i + 2) % 10], centers[10 + (i / 2 + 1) % 5]))
                    .Add(new Line(centers[10 + (i / 2 + 1) % 5], centers[10 + i / 2]))
                    .Add(new Line(centers[10 + i / 2], centers[i])));
            }
            res.AddPolygon(new Polygon().Add(new Line(centers[15], centers[16]))
                .Add(new Line(centers[16], centers[17]))
                .Add(new Line(centers[17], centers[18]))
                .Add(new Line(centers[18], centers[19]))
                .Add(new Line(centers[19], centers[15])));
            res.AddPolygon(new Polygon().Add(new Line(centers[10], centers[11]))
                .Add(new Line(centers[11], centers[12]))
                .Add(new Line(centers[12], centers[13]))
                .Add(new Line(centers[13], centers[14]))
                .Add(new Line(centers[14], centers[10])));

            return res;
        }


    }
}
