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

namespace laba7
{
    internal class FigureCreator
    {
        const int LEN = 200;
        Transformations transformations = new Transformations();
        public Polyhedron GetTetrahedron()
        {
            Polyhedron res = new Polyhedron();
            Vertex a = new Vertex(0, 0, 0);
            Vertex b = new Vertex(LEN, 0, LEN);
            Vertex c = new Vertex(LEN, LEN, 0);
            Vertex d = new Vertex(0, LEN, LEN);
            res.AddPolygon(new Polygon().Add(a, b, c));
            res.AddPolygon(new Polygon().Add(c, b, d));
            res.AddPolygon(new Polygon().Add(a, d, b));
            res.AddPolygon(new Polygon().Add(c, d, a));
            return res;

        }
        public Polyhedron GetHexahedron()
        {
            Polyhedron res = new Polyhedron();
            //Vertex a = new Vertex(0, 0, 0);
            //Vertex b = new Vertex(LEN, 0, 0);
            //Vertex c = new Vertex(LEN, 0, LEN);
            //Vertex d = new Vertex(0, 0, LEN);
            //Vertex e = new Vertex(0, LEN, 0);
            //Vertex f = new Vertex(LEN, LEN, 0);
            //Vertex g = new Vertex(LEN, LEN, LEN);
            //Vertex h = new Vertex(0, LEN, LEN);
            //res.AddPolygon(new Polygon().Add(new Vertex(a, b)).Add(new Vertex(b, c)).Add(new Vertex(c, d)).Add(new Vertex(d, a))); 
            //res.AddPolygon(new Polygon().Add(new Vertex(b, c)).Add(new Vertex(c, g)).Add(new Vertex(g, f)).Add(new Vertex(f, b))); 
            //res.AddPolygon(new Polygon().Add(new Vertex(f, g)).Add(new Vertex(g, h)).Add(new Vertex(h, e)).Add(new Vertex(e, f)));
            //res.AddPolygon(new Polygon().Add(new Vertex(h, e)).Add(new Vertex(e, a)).Add(new Vertex(a, d)).Add(new Vertex(d, h)));
            //res.AddPolygon(new Polygon().Add(new Vertex(a, b)).Add(new Vertex(b, f)).Add(new Vertex(f, e)).Add(new Vertex(e, a)));
            //res.AddPolygon(new Polygon().Add(new Vertex(d, c)).Add(new Vertex(c, g)).Add(new Vertex(g, h)).Add(new Vertex(h, d)));
            return res;
        }

        public Polyhedron GetOctahedron()
        {
            Polyhedron res = new Polyhedron();
            //Polyhedron cube = GetHexahedron();

            //Vertex a = cube.Polygons[0].GetCenter();
            //Vertex b = cube.Polygons[1].GetCenter();
            //Vertex c = cube.Polygons[2].GetCenter();
            //Vertex d = cube.Polygons[3].GetCenter();
            //Vertex e = cube.Polygons[4].GetCenter();
            //Vertex f = cube.Polygons[5].GetCenter();

            //res.AddPolygon(new Polygon().Add(new Vertex(a, f)).Add(new Vertex(f,b)).Add(new Vertex(b,a)));
            //res.AddPolygon(new Polygon().Add(new Vertex(b,c)).Add(new Vertex(c,f)).Add(new Vertex(f,b)));
            //res.AddPolygon(new Polygon().Add(new Vertex(c, d)).Add(new Vertex(d,f)).Add(new Vertex(f,c)));
            //res.AddPolygon(new Polygon().Add(new Vertex(d,a)).Add(new Vertex(a,f)).Add(new Vertex(f,d)));
            //res.AddPolygon(new Polygon().Add(new Vertex(a, e)).Add(new Vertex(e,b)).Add(new Vertex(b,a)));
            //res.AddPolygon(new Polygon().Add(new Vertex(b, e)).Add(new Vertex(e,c)).Add(new Vertex(c,b)));
            //res.AddPolygon(new Polygon().Add(new Vertex(c, e)).Add(new Vertex(e,d)).Add(new Vertex(d,c)));
            //res.AddPolygon(new Polygon().Add(new Vertex(d, e)).Add(new Vertex(e,a)).Add(new Vertex(a,d)));


            return res;
        }



        public Polyhedron GetIcosahedron() {
            Polyhedron res = new Polyhedron();
            //Vertex circleCenter = new Vertex(100, 100, 100);
            //List<Vertex> circlePoints = new List<Vertex>();
            //for (int angle = 0; angle < 360; angle += 36)
            //{
            //    if (angle % 72 == 0)
            //        circlePoints.Add(new Vertex((float)(circleCenter.X + (100f * Math.Cos(transformations.DegreeToRadian(angle)))), circleCenter.Y + 100f, (float)(circleCenter.Z + (100f * Math.Sin(transformations.DegreeToRadian(angle))))));
            //    else
            //        circlePoints.Add(new Vertex((float)(circleCenter.X + (100f * Math.Cos(transformations.DegreeToRadian(angle)))), circleCenter.Y, (float)(circleCenter.Z + (100f * Math.Sin(transformations.DegreeToRadian(angle))))));
            //}
            //Vertex a = new Vertex(100, 50, 100);
            //Vertex b = new Vertex(100, 250, 100);
            //for (int i = 0; i < 10; i++)
            //{
            //    res.AddPolygon(new Polygon().Add(new Vertex(circlePoints[i], circlePoints[(i + 1) % 10]))
            //        .Add(new Vertex(circlePoints[(i + 1) % 10], circlePoints[(i + 2) % 10])).Add(new Vertex(circlePoints[(i + 2) % 10], circlePoints[i])));
            //}
            //res.AddPolygon(new Polygon().Add(new Vertex(circlePoints[1], a)).Add(new Vertex(a, circlePoints[3])).Add(new Vertex(circlePoints[3], circlePoints[1])));
            //res.AddPolygon(new Polygon().Add(new Vertex(circlePoints[3], a)).Add(new Vertex(a, circlePoints[5])).Add(new Vertex(circlePoints[5], circlePoints[3])));
            //res.AddPolygon(new Polygon().Add(new Vertex(circlePoints[5], a)).Add(new Vertex(a, circlePoints[7])).Add(new Vertex(circlePoints[7], circlePoints[5])));
            //res.AddPolygon(new Polygon().Add(new Vertex(circlePoints[7], a)).Add(new Vertex(a, circlePoints[9])).Add(new Vertex(circlePoints[9], circlePoints[7])));
            //res.AddPolygon(new Polygon().Add(new Vertex(circlePoints[9], a)).Add(new Vertex(a, circlePoints[1])).Add(new Vertex(circlePoints[1], circlePoints[9])));

            //res.AddPolygon(new Polygon().Add(new Vertex(circlePoints[0], b)).Add(new Vertex(b, circlePoints[2])).Add(new Vertex(circlePoints[2], circlePoints[0])));
            //res.AddPolygon(new Polygon().Add(new Vertex(circlePoints[2], b)).Add(new Vertex(b, circlePoints[4])).Add(new Vertex(circlePoints[4], circlePoints[2])));
            //res.AddPolygon(new Polygon().Add(new Vertex(circlePoints[4], b)).Add(new Vertex(b, circlePoints[6])).Add(new Vertex(circlePoints[6], circlePoints[4])));
            //res.AddPolygon(new Polygon().Add(new Vertex(circlePoints[6], b)).Add(new Vertex(b, circlePoints[8])).Add(new Vertex(circlePoints[8], circlePoints[6])));
            //res.AddPolygon(new Polygon().Add(new Vertex(circlePoints[8], b)).Add(new Vertex(b, circlePoints[0])).Add(new Vertex(circlePoints[0], circlePoints[8])));
            return res;
        }

        public Polyhedron GetDodecahedron() {
            Polyhedron res = new Polyhedron();
            //Polyhedron icos = GetIcosahedron();

            //List<Vertex> centers = new List<Vertex>();

            //foreach (Polygon poly in icos.Polygons) {
            //    centers.Add(poly.GetCenter());
            //}

            //for (int i = 0; i < centers.Count/2; i++)
            //{
            //    if (i % 2 == 0)
            //    {
            //        res.AddPolygon(new Polygon().Add(new Vertex(centers[i], centers[(i + 1) % 10]))
            //            .Add(new Vertex(centers[(i + 1) % 10], centers[(i + 2) % 10]))
            //            .Add(new Vertex(centers[(i + 2) % 10], centers[15 + (i / 2 + 1) % 5]))
            //            .Add(new Vertex(centers[15 + (i / 2 + 1) % 5], centers[15 + i / 2]))
            //            .Add(new Vertex(centers[15 + i / 2], centers[i])));

            //        continue;
            //    }
            //    res.AddPolygon(new Polygon().Add(new Vertex(centers[i], centers[(i + 1) % 10]))
            //        .Add(new Vertex(centers[(i + 1) % 10], centers[(i + 2) % 10]))
            //        .Add(new Vertex(centers[(i + 2) % 10], centers[10 + (i / 2 + 1) % 5]))
            //        .Add(new Vertex(centers[10 + (i / 2 + 1) % 5], centers[10 + i / 2]))
            //        .Add(new Vertex(centers[10 + i / 2], centers[i])));
            //}
            //res.AddPolygon(new Polygon().Add(new Vertex(centers[15], centers[16]))
            //    .Add(new Vertex(centers[16], centers[17]))
            //    .Add(new Vertex(centers[17], centers[18]))
            //    .Add(new Vertex(centers[18], centers[19]))
            //    .Add(new Vertex(centers[19], centers[15])));
            //res.AddPolygon(new Polygon().Add(new Vertex(centers[10], centers[11]))
            //    .Add(new Vertex(centers[11], centers[12]))
            //    .Add(new Vertex(centers[12], centers[13]))
            //    .Add(new Vertex(centers[13], centers[14]))
            //    .Add(new Vertex(centers[14], centers[10])));

            return res;
        }


        
        public Polyhedron CreateRotation(string fileName) {
            Polyhedron res = new();

            List<string> prms = File.ReadAllText(fileName).Split(';').ToList();

            string axis = prms[1];
            int count = int.Parse(prms[2]);
            int angle_inc = 360 / count;

            List<Vertex> points = new();
            List<string> p_str = prms[0].Split(' ',StringSplitOptions.RemoveEmptyEntries).ToList();
            for (int i = 0; i < p_str.Count-2; i+=3)
            {
                    points.Add(new Vertex(float.Parse(p_str[i]),
                                         float.Parse(p_str[i + 1]),
                                         float.Parse(p_str[i + 2])));
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

                    
                    for (int j = 0; j < allPoints[i].Count() - 1; j++)
                    {
                        List<Vertex> lines = new();
                        Vertex line1 = new Vertex(allPoints[i][j]);
                        Vertex line2 = new Vertex(allPoints[0][j + 1]);
                        Vertex line3 = new Vertex(allPoints[0][j]);
                        Vertex line4 = new Vertex(allPoints[i][j + 1]);

                        lines.Add(line1);
                        lines.Add(line2);
                        lines.Add(line3);
                        lines.Add(line4);

                        Polygon p = new Polygon().Add(line1,line3,line2,line4);
                        res.AddPolygon(p);
                    }
                   
                }
                else
                {
                  
                    for (int j = 0; j < allPoints[i].Count() - 1; j++)
                    {
                        List<Vertex> lines = new();
                        Vertex line1 = new Vertex(allPoints[i][j]);
                        Vertex line2 = new Vertex(allPoints[i + 1][j + 1]);
                        Vertex line3 = new Vertex(allPoints[i + 1][j]);
                        Vertex line4 = new Vertex(allPoints[i][j + 1]);

                        lines.Add(line1);
                        lines.Add(line2);
                        lines.Add(line3);
                        lines.Add(line4);

                        Polygon p = new Polygon(line1,line3,line2,line4);
                        res.AddPolygon(p);
                    }
                 
                }

                
            }
            
            return res;
        }

        public Polyhedron CreateFunction(string _x1, string _y1, string _x2, string _y2,string _hx,string _hy , string func) {
            Polyhedron res = new();
            
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
                        new Polygon().Add(topLeft, topRight, bottomRight, bottomRight));
                     
                }
            }



            return res;
        }

    }
}
