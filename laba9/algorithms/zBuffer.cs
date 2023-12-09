using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace laba7
{

    internal class zBuffer
    {
        #region interpolation
        public static List<int> Interpolate(int x1, int y1, int x2, int y2)
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

        public static List<float> InterpolateIntense(int x1, float i1, int x2, float i2)
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

        public static List<TexturePoint> InterpolateTexture(int x1, TexturePoint t1, int x2, TexturePoint t2)
        {
            List<TexturePoint> res = new List<TexturePoint>();
            if (x1 == x2)
            {
                res.Add(t1);
            }

            TexturePoint step = (t2 - t1) / (x2 - x1); //с таким шагом будем получать новые значения
            TexturePoint y = t1;
            for (int i = x1; i <= x2; i++)
            {
                res.Add(y);
                y += step;
            }

            return res;
        }
        #endregion

        #region Rasterisation
        //растеризация треугольника
        public static List<Vertex> Raster(List<Vertex> points, bool isLight = false, bool isTexturing = false)
        {
            List<Vertex> res = new List<Vertex>();
            points.Sort((p1, p2) => p1.Y.CompareTo(p2.Y));
            // "рабочие точки"
            // изначально они находятся в верхней точке
            var wpoints = points.Select((p) => (x: p.X, y: p.Y, z: p.Z, intense:p.Intense, uv: p.texturePoint)).ToList();
            if (wpoints.Count == 0) {
                return null;
            }
            var xy01 = Interpolate(wpoints[0].y, wpoints[0].x, wpoints[1].y, wpoints[1].x);
            var xy12 = Interpolate(wpoints[1].y, wpoints[1].x, wpoints[2].y, wpoints[2].x);
            var xy02 = Interpolate(wpoints[0].y, wpoints[0].x, wpoints[2].y, wpoints[2].x);
            var yz01 = Interpolate(wpoints[0].y, wpoints[0].z, wpoints[1].y, wpoints[1].z);
            var yz12 = Interpolate(wpoints[1].y, wpoints[1].z, wpoints[2].y, wpoints[2].z);
            var yz02 = Interpolate(wpoints[0].y, wpoints[0].z, wpoints[2].y, wpoints[2].z);
            xy01.RemoveAt(xy01.Count() - 1); //убрать точку, чтобы не было повтора
            var xy = xy01.Concat(xy12).ToList();
            yz01.RemoveAt(yz01.Count() - 1);
            var yz = yz01.Concat(yz12).ToList();
            //когда растеризуем, треугольник делим надвое
            //ищем координаты, чтобы разделить треугольник на 2
            int center = xy.Count() / 2;
            List<int> lx, rx, lz, rz; //для приращений по координатам
            List<float> leftintense, rightintense;//для приращений по интенсивности цвета
            List<TexturePoint> lefttexture, righttexture; //для приращений по интенсивности цвета

            leftintense = new List<float>();
            rightintense = new List<float>();

            lefttexture = new List<TexturePoint>();
            righttexture = new List<TexturePoint>();

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
                var lighting01 = InterpolateIntense(wpoints[0].y, wpoints[0].intense, wpoints[1].y, wpoints[1].intense);
                var lighting12 = InterpolateIntense(wpoints[1].y, wpoints[1].intense, wpoints[2].y, wpoints[2].intense);
                var lighting02 = InterpolateIntense(wpoints[0].y, wpoints[0].intense, wpoints[2].y, wpoints[2].intense);
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

            if (isTexturing) {
                var texture01 = InterpolateTexture(wpoints[0].y, wpoints[0].uv, wpoints[1].y, wpoints[1].uv);
                var texture12 = InterpolateTexture(wpoints[1].y, wpoints[1].uv, wpoints[2].y, wpoints[2].uv);
                var texture02 = InterpolateTexture(wpoints[0].y, wpoints[0].uv, wpoints[2].y, wpoints[2].uv);
                texture01.RemoveAt(texture01.Count() - 1); //убрать точку, чтобы не было повтора
                var texture = texture01.Concat(texture12).ToList();
                if (xy02[center] < xy[center])
                {
                    lefttexture = texture02;
                    righttexture = texture;

                }
                else
                {
                    lefttexture = texture;
                    righttexture = texture02;
                }
            }

            int y0 = wpoints[0].y;
            int y2 = wpoints[2].y;
            for (int i = 0; i <= y2 - y0; i++)
            {
                int leftx = lx[i];
                int rightx = rx[i];
                List<int> zcurr = Interpolate(leftx, lz[i], rightx, rz[i]);
                if (isLight) {
                    List<float> intense_current = InterpolateIntense(leftx, leftintense[i], rightx, rightintense[i]);
                    for (int j = leftx; j < rightx; j++)
                    {
                        res.Add(new Vertex(j, y0 + i, zcurr[j - leftx], intense_current[j - leftx]));
                    }
                }
                else if (isTexturing)
                {
                    List<TexturePoint> textureCurrent = InterpolateTexture(leftx, lefttexture[i], rightx, righttexture[i]);
                    for (int j = leftx; j < rightx; j++)
                    {
                        res.Add(new Vertex(j, y0 + i, zcurr[j - leftx], tp: textureCurrent[j - leftx]));
                    }

                }
                else
                {
                    for (int j = leftx; j < rightx; j++)
                    {
                        res.Add(new Vertex(j, y0 + i, zcurr[j - leftx]));
                    }
                }
            }

            return res;
        }


        public static List<List<Vertex>> Triangulate(List<Vertex> points)
        {
            List<List<Vertex>> res = new List<List<Vertex>>();
            if (points.Count == 3)
            {
                res = new List<List<Vertex>> { points };
            }

            for (int i = 2; i < points.Count(); i++)
            {
                res.Add(new List<Vertex> { points[0], points[i - 1], points[i] });
            }

            return res;
        }

        public static List<List<Vertex>> RasterFigure(Polyhedron figure, Camera camera,bool isLight=false, bool isTexturing=false)
        {
            List<List<Vertex>> res = new List<List<Vertex>>();
            foreach (var polygon in figure.Polygons)
            {

                List<Vertex> currentface = new List<Vertex>();
                List<Vertex> points = new List<Vertex>();
                points.AddRange(polygon.Verts);


                List<List<Vertex>> triangles = Triangulate(points);
                foreach (var triangle in triangles)
                {
                    currentface.AddRange(Raster(ProjectionToPlane(triangle, camera),isLight, isTexturing));
                }

                res.Add(currentface);
            }

            return res;
        }

        #endregion
       
        
        public static List<Vertex> ProjectionToPlane(List<Vertex> points, Camera camera)
        {
            List<Vertex> res = new List<Vertex>();

            foreach (var p in points)
            {
                var current = p.Projection(camera);
                if (current.Item1 != null)
                {
                    Vertex newpoint = new Vertex(current.Item1.Value.X, current.Item1.Value.Y, current.Item2,p.Intense,p.normVector,p.texturePoint);
                    res.Add(newpoint);
                }
            }

            return res;
        }



        public static Bitmap z_buf(int width, int height, List<Polyhedron> scene, Camera camera, List<Color> colors, bool isLight=false, LightSource light=null, bool isTexturing = false, string filePath=null)
        {
            if (isLight)
            {
                foreach (var figure in scene)
                {
                    Lighting.CalculateLambert(figure, light);
                }

            }
            Bitmap texture=new(width,height);
            if (isTexturing)
                texture = new Bitmap(filePath);
            Bitmap bmp = new Bitmap(width, height);
            //z-буфер
            float[,] zbuffer = new float[width, height];
            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                    zbuffer[i, j] = float.MaxValue;
            List<List<List<Vertex>>> rasterscene = new List<List<List<Vertex>>>();
            for (int i = 0; i < scene.Count(); i++)
            {
                rasterscene.Add(RasterFigure(scene[i], camera,isLight,isTexturing)); //растеризовали все фигуры
            }


            int index = 0;
            for (int i = 0; i < rasterscene.Count(); i++)
            {
                var color = scene[i].GetColor();
                for (int j = 0; j < rasterscene[i].Count(); j++)
                {
                    List<Vertex> current = rasterscene[i][j];
                    
                    foreach (Vertex p in current)
                    {
                        int x = (p.X);
                        int y = (p.Y);

                        float u = p.texturePoint.U;
                        float v = p.texturePoint.V;
                        Debug.WriteLineIf(p.texturePoint.U > 0, "true");
                        if (x < width && y < height && y > 0 && x > 0)
                        {
                            if (p.ZF < zbuffer[x, y])
                            {
                                zbuffer[x, y] = p.ZF;
                                if (isLight)
                                {
                                    bmp.SetPixel(x, y, Color.FromArgb((int)(p.Intense * color.R), (int)(p.Intense * color.G), (int)(p.Intense * color.B)));

                                }
                                else if (isTexturing) {
                                    var text_color = texture.GetPixel( (int)(u * (texture.Width - 1)), (int)(v * (texture.Height - 1)));
                                    bmp.SetPixel(x, y, text_color);
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