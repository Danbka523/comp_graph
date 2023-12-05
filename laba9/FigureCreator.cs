using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Layout;
using System.Windows.Forms.VisualStyles;
using AngouriMath;   //https://github.com/asc-community/AngouriMath?ysclid=lp2o5a92m5613424805
using static AngouriMath.MathS;

namespace laba9
{
    internal class FigureCreator
    {
        const int LEN = 200;
        Transformations transformations = new Transformations();
        public Polyhedron GetTetrahedron()
        {
            Polyhedron res = new Polyhedron(Color.Empty);
            Vertex a = new Vertex(0, 0, 0);
            Vertex b = new Vertex(LEN, 0, LEN);
            Vertex c = new Vertex(LEN, LEN, 0);
            Vertex d = new Vertex(0, LEN, LEN);
            res.AddPolygon(new Polygon().AddVerts(a,b,c));
            res.AddPolygon(new Polygon().AddVerts(c, b, d));
            res.AddPolygon(new Polygon().AddVerts(a, d, b));
            res.AddPolygon(new Polygon().AddVerts(c, d, a)); 
            return res;

        }
        public Polyhedron GetHexahedron()
        {
            Polyhedron res = new Polyhedron(Color.Empty);
            Vertex a = new Vertex(0, 0, 0);
            Vertex b = new Vertex(0, 200, 0);
            Vertex c = new Vertex(200, 200, 0);
            Vertex d = new Vertex(200, 0, 0);
            Vertex e = new Vertex(0, 0, 200);
            Vertex f = new Vertex(0, 200, 200);
            Vertex g = new Vertex(200, 200, 200);
            Vertex h = new Vertex(200, 0, 200);
            res.AddPolygon(new Polygon(a,b,c,d));
            res.AddPolygon(new Polygon(a,e,h,d));
            res.AddPolygon(new Polygon(a,e,f,b));
            res.AddPolygon(new Polygon(b,f,g,c));
            res.AddPolygon(new Polygon(e,f,g,h));
            res.AddPolygon(new Polygon(d,h,g,c));
           
            return res;
        }

        //public Polyhedron GetOctahedron()
        //{
        //    Polyhedron res = new Polyhedron();
        //    Polyhedron cube = GetHexahedron();

        //    Vertex a = cube.Polygons[0].GetCenter();
        //    Vertex b = cube.Polygons[1].GetCenter();
        //    Vertex c = cube.Polygons[2].GetCenter();
        //    Vertex d = cube.Polygons[3].GetCenter();
        //    Vertex e = cube.Polygons[4].GetCenter();
        //    Vertex f = cube.Polygons[5].GetCenter();

        //    res.AddPolygon(new Polygon().Add(new Line(a, f)).Add(new Line(f,b)).Add(new Line(b,a)));
        //    res.AddPolygon(new Polygon().Add(new Line(b,c)).Add(new Line(c,f)).Add(new Line(f,b)));
        //    res.AddPolygon(new Polygon().Add(new Line(c, d)).Add(new Line(d,f)).Add(new Line(f,c)));
        //    res.AddPolygon(new Polygon().Add(new Line(d,a)).Add(new Line(a,f)).Add(new Line(f,d)));
        //    res.AddPolygon(new Polygon().Add(new Line(a, e)).Add(new Line(e,b)).Add(new Line(b,a)));
        //    res.AddPolygon(new Polygon().Add(new Line(b, e)).Add(new Line(e,c)).Add(new Line(c,b)));
        //    res.AddPolygon(new Polygon().Add(new Line(c, e)).Add(new Line(e,d)).Add(new Line(d,c)));
        //    res.AddPolygon(new Polygon().Add(new Line(d, e)).Add(new Line(e,a)).Add(new Line(a,d)));


        //    return res;
        //}



        //public Polyhedron GetIcosahedron() {
        //    Polyhedron res = new Polyhedron();
        //    Vertex circleCenter = new Vertex(100, 100, 100);
        //    List<Vertex> circlePoints = new List<Vertex>();
        //    for (int angle = 0; angle < 360; angle += 36)
        //    {
        //        if (angle % 72 == 0)
        //            circlePoints.Add(new Vertex((float)(circleCenter.X + (100f * Math.Cos(transformations.DegreeToRadian(angle)))), circleCenter.Y + 100f, (float)(circleCenter.Z + (100f * Math.Sin(transformations.DegreeToRadian(angle))))));
        //        else
        //            circlePoints.Add(new Vertex((float)(circleCenter.X + (100f * Math.Cos(transformations.DegreeToRadian(angle)))), circleCenter.Y, (float)(circleCenter.Z + (100f * Math.Sin(transformations.DegreeToRadian(angle))))));
        //    }
        //    Vertex a = new Vertex(100, 50, 100);
        //    Vertex b = new Vertex(100, 250, 100);
        //    for (int i = 0; i < 10; i++)
        //    {
        //        res.AddPolygon(new Polygon().Add(new Line(circlePoints[i], circlePoints[(i + 1) % 10]))
        //            .Add(new Line(circlePoints[(i + 1) % 10], circlePoints[(i + 2) % 10])).Add(new Line(circlePoints[(i + 2) % 10], circlePoints[i])));
        //    }
        //    res.AddPolygon(new Polygon().Add(new Line(circlePoints[1], a)).Add(new Line(a, circlePoints[3])).Add(new Line(circlePoints[3], circlePoints[1])));
        //    res.AddPolygon(new Polygon().Add(new Line(circlePoints[3], a)).Add(new Line(a, circlePoints[5])).Add(new Line(circlePoints[5], circlePoints[3])));
        //    res.AddPolygon(new Polygon().Add(new Line(circlePoints[5], a)).Add(new Line(a, circlePoints[7])).Add(new Line(circlePoints[7], circlePoints[5])));
        //    res.AddPolygon(new Polygon().Add(new Line(circlePoints[7], a)).Add(new Line(a, circlePoints[9])).Add(new Line(circlePoints[9], circlePoints[7])));
        //    res.AddPolygon(new Polygon().Add(new Line(circlePoints[9], a)).Add(new Line(a, circlePoints[1])).Add(new Line(circlePoints[1], circlePoints[9])));

        //    res.AddPolygon(new Polygon().Add(new Line(circlePoints[0], b)).Add(new Line(b, circlePoints[2])).Add(new Line(circlePoints[2], circlePoints[0])));
        //    res.AddPolygon(new Polygon().Add(new Line(circlePoints[2], b)).Add(new Line(b, circlePoints[4])).Add(new Line(circlePoints[4], circlePoints[2])));
        //    res.AddPolygon(new Polygon().Add(new Line(circlePoints[4], b)).Add(new Line(b, circlePoints[6])).Add(new Line(circlePoints[6], circlePoints[4])));
        //    res.AddPolygon(new Polygon().Add(new Line(circlePoints[6], b)).Add(new Line(b, circlePoints[8])).Add(new Line(circlePoints[8], circlePoints[6])));
        //    res.AddPolygon(new Polygon().Add(new Line(circlePoints[8], b)).Add(new Line(b, circlePoints[0])).Add(new Line(circlePoints[0], circlePoints[8])));
        //    return res;
        //}

        //public Polyhedron GetDodecahedron() {
        //    Polyhedron res = new Polyhedron();
        //    Polyhedron icos = GetIcosahedron();

        //    List<Vertex> centers = new List<Vertex>();

        //    foreach (Polygon poly in icos.Polygons) {
        //        centers.Add(poly.GetCenter());
        //    }

        //    for (int i = 0; i < centers.Count/2; i++)
        //    {
        //        if (i % 2 == 0)
        //        {
        //            res.AddPolygon(new Polygon().Add(new Line(centers[i], centers[(i + 1) % 10]))
        //                .Add(new Line(centers[(i + 1) % 10], centers[(i + 2) % 10]))
        //                .Add(new Line(centers[(i + 2) % 10], centers[15 + (i / 2 + 1) % 5]))
        //                .Add(new Line(centers[15 + (i / 2 + 1) % 5], centers[15 + i / 2]))
        //                .Add(new Line(centers[15 + i / 2], centers[i])));

        //            continue;
        //        }
        //        res.AddPolygon(new Polygon().Add(new Line(centers[i], centers[(i + 1) % 10]))
        //            .Add(new Line(centers[(i + 1) % 10], centers[(i + 2) % 10]))
        //            .Add(new Line(centers[(i + 2) % 10], centers[10 + (i / 2 + 1) % 5]))
        //            .Add(new Line(centers[10 + (i / 2 + 1) % 5], centers[10 + i / 2]))
        //            .Add(new Line(centers[10 + i / 2], centers[i])));
        //    }
        //    res.AddPolygon(new Polygon().Add(new Line(centers[15], centers[16]))
        //        .Add(new Line(centers[16], centers[17]))
        //        .Add(new Line(centers[17], centers[18]))
        //        .Add(new Line(centers[18], centers[19]))
        //        .Add(new Line(centers[19], centers[15])));
        //    res.AddPolygon(new Polygon().Add(new Line(centers[10], centers[11]))
        //        .Add(new Line(centers[11], centers[12]))
        //        .Add(new Line(centers[12], centers[13]))
        //        .Add(new Line(centers[13], centers[14]))
        //        .Add(new Line(centers[14], centers[10])));

        //    return res;
        //}



        public Polyhedron CreateRotation(string fileName) {
            Polyhedron res = new(Color.Empty);

            List<string> prms = File.ReadAllText(fileName).Split(';').ToList();

            string axis = prms[1];
            int count = int.Parse(prms[2]);
            int angle_inc = 360 / count;

            List<Vertex> points = new();
            List<string> p_str = prms[0].Split(' ',StringSplitOptions.RemoveEmptyEntries).ToList();
            for (int i = 0; i < p_str.Count-2; i+=3)
            {
                    points.Add(new Vertex(float.Parse(p_str[i], CultureInfo.InvariantCulture),
                                         float.Parse(p_str[i + 1], CultureInfo.InvariantCulture),
                                         float.Parse(p_str[i + 2], CultureInfo.InvariantCulture)));
            }

            List<List<Vertex>> allPoints = new()
            {
                points
            };

            for (int i = 1; i < count; i++)
            {
                allPoints.Add(transformations.RotateFigPoints(axis,points,i*angle_inc));
            }

            //i - набор j - точка в наборе 
            for (int i = 0; i < allPoints.Count; i++)
            {
                if (i == allPoints.Count - 1)
                {

                    List<Vertex> verts = new();
                    for (int j = 0; j < allPoints[i].Count() - 1; j++)
                    {

                        verts.Add(allPoints[i][j]);
                        verts.Add(allPoints[i][j+1]);
                        verts.Add(allPoints[0][j+1]);
                        verts.Add(allPoints[0][j]);
                    }
                    Polygon p = new Polygon(verts);
                    res.AddPolygon(p);
                }
                else
                {
                    List<Vertex> verts = new();
                    for (int j = 0; j < allPoints[i].Count() - 1; j++)
                    {

                        Line line1 = new Line(allPoints[i][j], allPoints[i][j + 1]);
                        Line line2 = new Line(allPoints[i + 1][j + 1], allPoints[i + 1][j]);
                        Line line3 = new Line(allPoints[i + 1][j], allPoints[i][j]);
                        Line line4 = new Line(allPoints[i][j + 1], allPoints[i + 1][j + 1]);



                        verts.Add(allPoints[i][j+1]);
                        verts.Add(allPoints[i+1][j + 1]);
                        verts.Add(allPoints[i][j]);
                        verts.Add(allPoints[i][j+1]);
                    }
                    Polygon p = new Polygon(verts);
                    res.AddPolygon(p);
                }

                
            }
            
            return res;
        }

        public Polyhedron CreateFunction(string _x1, string _y1, string _x2, string _y2,string _hx,string _hy , string func) {
            Polyhedron res = new(Color.Empty);
            
            float x1 = float.Parse(_x1,CultureInfo.InvariantCulture);
            float y1 = float.Parse(_y1,CultureInfo.InvariantCulture);
            float x2 = float.Parse(_x2,CultureInfo.InvariantCulture);
            float y2 = float.Parse(_y2,CultureInfo.InvariantCulture);
            float hx = float.Parse(_hx,CultureInfo.InvariantCulture);
            float hy = float.Parse(_hy,CultureInfo.InvariantCulture);

            for (float i = x1; i < x2; i += hx)
            {
                for (float j = y1; j < y2; j += hy)
                {
                    // Генерация вершин квадрата
                    Vertex topLeft = new Vertex(i, j, transformations.EvalFunc(func, i, j));
                    Vertex topRight = new Vertex(i + hx, j, transformations.EvalFunc(func, i + hx, j));
                    Vertex bottomRight = new Vertex(i + hx, j + hy, transformations.EvalFunc(func, i + hx, j + hy));
                    Vertex bottomLeft = new Vertex(i, j + hy, transformations.EvalFunc(func, i, j + hy));

                    // Создание полигонов
                    res.AddPolygon(
                        new Polygon().AddVerts(topLeft,topRight,bottomRight,bottomLeft));
                     
                }
            }
            return res;
        }

    }
}
