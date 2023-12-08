using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba7
{

    internal class zBuffer
    {
        public static List<int> interpolate(int x1, int y1, int x2, int y2)
        {
            List<int> res = new List<int>();
            if (x1 == x2)
            {
                res.Add(y2);
            }

            float step = (y2 - y1) * 1.0f / (x2 - x1); 
            float y = y1;
            for (int i = x1; i <= x2; i++)
            {
                res.Add((int)y);
                y += step;
            }

            return res;
        }

        public static List<float> interpolate_intense(int x1, float i1, int x2, float i2)
        {
            List<float> res = new List<float>();
            if (x1 == x2)
            {
                res.Add(i1);
            }

            float step = (i2 - i1) / (x2 - x1); //с таким шагом будем получать новые значения
            float y = i1;
            for (int i = x1; i <= x2; i++)
            {
                res.Add(y);
                y += step;
            }

            return res;
        }


        //растеризация треугольника
        public static List<Point> Raster(List<Point> points, bool isLight = false)
        {
            List<Point> res = new List<Point>();
            points.Sort((p1, p2) => p1.Y.CompareTo(p2.Y));
            // "рабочие точки"
            // изначально они находятся в верхней точке
            var wpoints = points.Select((p) => (x: p.X, y: p.Y, z: p.Z, intense:p.Intense)).ToList();
            if (wpoints.Count == 0) {
                return null;
            }
            var xy01 = interpolate(wpoints[0].y, wpoints[0].x, wpoints[1].y, wpoints[1].x);
            var xy12 = interpolate(wpoints[1].y, wpoints[1].x, wpoints[2].y, wpoints[2].x);
            var xy02 = interpolate(wpoints[0].y, wpoints[0].x, wpoints[2].y, wpoints[2].x);
            var yz01 = interpolate(wpoints[0].y, wpoints[0].z, wpoints[1].y, wpoints[1].z);
            var yz12 = interpolate(wpoints[1].y, wpoints[1].z, wpoints[2].y, wpoints[2].z);
            var yz02 = interpolate(wpoints[0].y, wpoints[0].z, wpoints[2].y, wpoints[2].z);
            xy01.RemoveAt(xy01.Count() - 1); //убрать точку, чтобы не было повтора
            var xy = xy01.Concat(xy12).ToList();
            yz01.RemoveAt(yz01.Count() - 1);
            var yz = yz01.Concat(yz12).ToList();
            //когда растеризуем, треугольник делим надвое
            //ищем координаты, чтобы разделить треугольник на 2
            int center = xy.Count() / 2;
            List<int> lx, rx, lz, rz; //для приращений по координатам
            List<float> leftintense, rightintense;//для приращений по интенсивности цвета
            leftintense = new List<float>();
            rightintense = new List<float>();
            if (xy02[center] < xy[center])
            {
                lx = xy02;
                lz = yz02;
                rx = xy;
                rz = yz;
            }
            else
            {
                lx = xy;
                lz = yz;
                rx = xy02;
                rz = yz02;
            }
            if (isLight)
            {
                var lighting01 = interpolate_intense(wpoints[0].y, wpoints[0].intense, wpoints[1].y, wpoints[1].intense);
                var lighting12 = interpolate_intense(wpoints[1].y, wpoints[1].intense, wpoints[2].y, wpoints[2].intense);
                var lighting02 = interpolate_intense(wpoints[0].y, wpoints[0].intense, wpoints[2].y, wpoints[2].intense);
                lighting01.RemoveAt(lighting01.Count() - 1); //убрать точку, чтобы не было повтора
                var lighting = lighting01.Concat(lighting12).ToList();
                if (xy02[center] < xy[center])
                {
                    leftintense = lighting02;
                    rightintense = lighting;

                }
                else
                {
                    leftintense = lighting;
                    rightintense = lighting02;
                }

                //когда растеризуем, треугольник делим надвое
                //ищем координаты, чтобы разделить треугольник на 2

            }


            int y0 = wpoints[0].y;
            int y2 = wpoints[2].y;
            for (int i = 0; i <= y2 - y0; i++)
            {
                int leftx = lx[i];
                int rightx = rx[i];
                List<int> zcurr = interpolate(leftx, lz[i], rightx, rz[i]);
                if (isLight) {
                    List<float> intense_current = interpolate_intense(leftx, leftintense[i], rightx, rightintense[i]);
                    for (int j = leftx; j < rightx; j++)
                    {
                        res.Add(new Point(j, y0 + i, zcurr[j - leftx], intense_current[j - leftx]));
                    }
                }
                else
                {
                    for (int j = leftx; j < rightx; j++)
                    {
                        res.Add(new Point(j, y0 + i, zcurr[j - leftx]));
                    }
                }
            }

            return res;
        }


        public static List<List<Point>> Triangulate(List<Point> points)
        {
            List<List<Point>> res = new List<List<Point>>();
            if (points.Count == 3)
            {
                res = new List<List<Point>> { points };
            }

            for (int i = 2; i < points.Count(); i++)
            {
                res.Add(new List<Point> { points[0], points[i - 1], points[i] });
            }

            return res;
        }

        public static List<List<Point>> RasterFigure(Polyhedron figure, Camera camera,bool isLight)
        {
            List<List<Point>> res = new List<List<Point>>();
            foreach (var polygon in figure.Polygons)
            {

                List<Point> currentface = new List<Point>();
                List<Point> points = new List<Point>();
                points.AddRange(polygon.Verts);


                List<List<Point>> triangles = Triangulate(points);
                foreach (var triangle in triangles)
                {
                    currentface.AddRange(Raster(ProjectionToPlane(triangle, camera),isLight));
                }

                res.Add(currentface);
            }

            return res;
        }


        public static List<Point> ProjectionToPlane(List<Point> points, Camera camera)
        {
            List<Point> res = new List<Point>();

            foreach (var p in points)
            {
                var current = p.Projection(camera);
                if (current.Item1 != null)
                {
                    Point newpoint = new Point(current.Item1.Value.X, current.Item1.Value.Y, current.Item2,p.Intense);
                    res.Add(newpoint);
                }
            }

            return res;
        }



        public static Bitmap z_buf(int width, int height, List<Polyhedron> scene, Camera camera, List<Color> colors, bool isLight=false, LightSource light=null)
        {
            if (isLight)
            {
                foreach (var figure in scene)
                {
                    Lighting.CalculateLambert(figure, light);
                }

            }

            Bitmap bmp = new Bitmap(width, height);
            //z-буфер
            float[,] zbuffer = new float[width, height];
            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                    zbuffer[i, j] = float.MaxValue;
            List<List<List<Point>>> rasterscene = new List<List<List<Point>>>();
            for (int i = 0; i < scene.Count(); i++)
            {
                rasterscene.Add(RasterFigure(scene[i], camera,isLight)); //растеризовали все фигуры
            }


            int index = 0;
            for (int i = 0; i < rasterscene.Count(); i++)
            {
                var color = scene[i].GetColor();
                for (int j = 0; j < rasterscene[i].Count(); j++)
                {
                    List<Point> current = rasterscene[i][j];
                    Debug.WriteLine(current.Where(x => x.Intense > 0).Count());
                    foreach (Point p in current)
                    {
                        int x = (p.X);

                        int y = (p.Y);

                        if (x < width && y < height && y > 0 && x > 0)
                        {
                            if (p.ZF < zbuffer[x, y])
                            {
                                zbuffer[x, y] = p.ZF;
                                if (isLight)
                                {
                                    bmp.SetPixel(x, y, Color.FromArgb((int)(p.Intense * color.R), (int)(p.Intense * color.G), (int)(p.Intense * color.B)));
                           
                                }
                                else
                                    bmp.SetPixel(x, y, colors[index % colors.Count()]);
                            }
                        }
                    }
                    index++;
                }
            }

            return bmp;
        }


    }
}