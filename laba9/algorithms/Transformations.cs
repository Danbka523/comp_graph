using AngouriMath;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;

namespace laba7
{
    internal class Transformations
    {
        public float DegreeToRadian(float degree) => (float)Math.PI * degree / 180;
        Vertex CenterOfFigure(Polyhedron figure)
        {
            float x = figure.Polygons.Average(p => p.GetCenter().XF);
            float y = figure.Polygons.Average(p => p.GetCenter().YF);
            float z = figure.Polygons.Average(p => p.GetCenter().ZF);

            return new Vertex(x, y, z);

        }


        public void MirrorAroundAxis(Polyhedron figure, string axis)
        {
            Matrix mirror;
            switch (axis)
            {
                case "XY":
                    mirror = new Matrix(4, 4).Fill(
                        1, 0, 0, 0,
                        0, 1, 0, 0,
                        0, 0, -1, 0,
                        0, 0, 0, 1);
                    break;
                case "XZ":
                    mirror = new Matrix(4, 4).Fill(
                        1, 0, 0, 0,
                        0, -1, 0, 0,
                        0, 0, 1, 0,
                        0, 0, 0, 1);
                    break;
                case "YZ":
                    mirror = new Matrix(4, 4).Fill(
                        -1, 0, 0, 0,
                        0, 1, 0, 0,
                        0, 0, 1, 0,
                        0, 0, 0, 1);
                    break;
                default:
                    throw new ArgumentException("invalid axis");
            }

            foreach (var poly in figure.Polygons)
            {
                for (int i = 0; i < poly.Verts.Count; i++)
                {
                    var res = mirror * new Matrix(4, 1).Fill(poly.Verts[i].XF, poly.Verts[i].YF, poly.Verts[i].ZF, 1);
                    poly.Verts[i] = new Vertex(res[0, 0], res[1, 0], res[2, 0], poly.Verts[i].Intense, poly.Verts[i].normVector, poly.Verts[i].texturePoint);

                }
            }


        }

        public void Shift(Polyhedron figure, float dX, float dY, float dZ) {
            Matrix shift = new Matrix(4, 4).Fill(
                1, 0, 0, dX,
                0, 1, 0, dY,
                0, 0, 1, dZ,
                0, 0, 0, 1);

            foreach (var poly in figure.Polygons)
            {
                for (int i = 0; i < poly.Verts.Count; i++)
                {
                    var res = shift * new Matrix(4, 1).Fill(poly.Verts[i].XF, poly.Verts[i].YF, poly.Verts[i].ZF, 1);
                    poly.Verts[i] = new Vertex(res[0, 0], res[1, 0], res[2, 0], poly.Verts[i].Intense, poly.Verts[i].normVector, poly.Verts[i].texturePoint);

                }
            }
        }

        public void Scale(Polyhedron figure, float sX, float sY, float sZ) {
            Matrix scale = new Matrix(4, 4).Fill(
                sX, 0, 0, 0,
                0, sY, 0, 0,
                0, 0, sZ, 0,
                0, 0, 0, 1
                );
            foreach (var poly in figure.Polygons)
            {
                for (int i = 0; i < poly.Verts.Count; i++)
                {
                    var res = scale * new Matrix(4, 1).Fill(poly.Verts[i].XF, poly.Verts[i].YF, poly.Verts[i].ZF, 1);
                    poly.Verts[i] = new Vertex(res[0, 0], res[1, 0], res[2, 0], poly.Verts[i].Intense, poly.Verts[i].normVector, poly.Verts[i].texturePoint);

                }
            }
        }

        public void RotateAroundAxis(Polyhedron figure, float degree, string axis) {

            Matrix rotate = GetRotationAroundAxisMatrix(axis, degree);

            foreach (var poly in figure.Polygons)
            {
                for (int i = 0; i < poly.Verts.Count; i++)
                {
                    var res = rotate * new Matrix(4, 1).Fill(poly.Verts[i].XF, poly.Verts[i].YF, poly.Verts[i].ZF, 1);
                    poly.Verts[i] = new Vertex(res[0, 0], res[1, 0], res[2, 0], poly.Verts[i].Intense, poly.Verts[i].normVector, poly.Verts[i].texturePoint);

                }
            }
        }
        public Point RotateAroundAxis(Point p, float degree, string axis)
        {

            Matrix rotate = GetRotationAroundAxisMatrix(axis, degree);
            var res = rotate * new Matrix(4, 1).Fill(p.XF, p.YF, p.ZF, 1);
            return new Point(res[0, 0], res[1, 0], res[2, 0]);
        }
        public Matrix RotateAroundAxis(float degree, string axis)
        {
            return  GetRotationAroundAxisMatrix(axis, degree);
        }

        public void RotateAroundCustomAxis(Polyhedron figure, float degree, Vertex p1, Vertex p2) {
            if (p2.Z < p1.Z || (p2.Z == p1.Z && p2.Y < p1.Y) ||
              (p2.Z == p1.Z && p2.Y == p1.Y) && p2.X < p1.X)
            {
                (p1, p2) = (p2, p1);
            }


            float angleSin = (float)Math.Sin(DegreeToRadian(degree));
            float angleCos = (float)Math.Cos(DegreeToRadian(degree));

            Vertex vec = new Vertex(p2.XF - p1.XF, p2.YF - p1.YF, p2.ZF - p1.ZF);
            float len = (float)Math.Sqrt((float)(vec.XF * vec.XF + vec.YF * vec.YF + vec.ZF * vec.ZF));

            float l = vec.XF / len;
            float m = vec.YF / len;
            float n = vec.ZF / len;

            Matrix rotate = new Matrix(4, 4).Fill(
                l * l + angleCos * (1f - l * l), l * (1f - angleCos) * m - n * angleSin, l * (1f - angleCos) * n + m * angleSin, 0,
                l * (1f - angleCos) * m + n * angleSin, m * m + angleCos * (1f - m * m), m * (1f - angleCos) * n - l * angleSin, 0,
                l * (1f - angleCos) * n - m * angleSin, m * (1f - angleCos) * n + l * angleSin, n * n + angleCos * (1f - n * n), 0,
                0, 0, 0, 1f
                );

            foreach (var poly in figure.Polygons)
            {
                for (int i = 0; i < poly.Verts.Count; i++)
                {
                    var res = rotate * new Matrix(4, 1).Fill(poly.Verts[i].XF, poly.Verts[i].YF, poly.Verts[i].ZF, 1);
                    poly.Verts[i] = new Vertex(res[0, 0], res[1, 0], res[2, 0], poly.Verts[i].Intense, poly.Verts[i].normVector, poly.Verts[i].texturePoint);

                }
            }
        }
        public void RotateAroundCenterAxis(Polyhedron figure, float degree, string axis)
        {

            Vertex center = CenterOfFigure(figure);

            Matrix rotate;
            rotate = new Matrix(4, 4).Fill(
                1, 0, 0, -center.X,
                0, 1, 0, -center.Y,
                0, 0, 1, -center.Z,
                0, 0, 0, 1
                );
            foreach (var poly in figure.Polygons)
            {
                for (int i = 0; i < poly.Verts.Count; i++)
                {
                    var res = rotate * new Matrix(4, 1).Fill(poly.Verts[i].XF, poly.Verts[i].YF, poly.Verts[i].ZF, 1);
                    poly.Verts[i] = new Vertex(res[0, 0], res[1, 0], res[2, 0], poly.Verts[i].Intense, poly.Verts[i].normVector, poly.Verts[i].texturePoint);

                }
            }

            RotateAroundAxis(figure, degree, axis);

            rotate = new Matrix(4, 4).Fill(
       1f, 0, 0, center.X,
       0, 1f, 0, center.Y,
       0, 0, 1f, center.Z,
       0, 0, 0, 1f
       );
            foreach (var poly in figure.Polygons)
            {
                for (int i = 0; i < poly.Verts.Count; i++)
                {
                    var res = rotate * new Matrix(4, 1).Fill(poly.Verts[i].XF, poly.Verts[i].YF, poly.Verts[i].ZF, 1);
                    poly.Verts[i] = new Vertex(res[0, 0], res[1, 0], res[2, 0], poly.Verts[i].Intense, poly.Verts[i].normVector, poly.Verts[i].texturePoint);

                }
            }

        }


        private Matrix GetRotationAroundAxisMatrix(string axis, float degree)
        {
            axis = axis.Trim();
            float rad = DegreeToRadian(degree);
            Matrix rotate;
            switch (axis)
            {
                case "X":
                    rotate = new Matrix(4, 4).Fill(
                        1, 0, 0, 0,
                        0, (float)Math.Cos(rad), (float)Math.Sin(rad), 0,
                        0, -(float)Math.Sin(rad), (float)Math.Cos(rad), 0,
                        0, 0, 0, 1);
                    break;
                case "Z":
                    rotate = new Matrix(4, 4).Fill(
                        (float)Math.Cos(rad), -(float)Math.Sin(rad), 0, 0,
                        (float)Math.Sin(rad), (float)Math.Cos(rad), 0, 0,
                        0, 0, 1, 0,
                        0, 0, 0, 1);
                    break;
                case "Y":
                    rotate = new Matrix(4, 4).Fill(
                        (float)Math.Cos(rad), 0, (float)Math.Sin(rad), 0,
                        0, 1, 0, 0,
                        -(float)Math.Sin(rad), 0, (float)Math.Cos(rad), 0,
                        0, 0, 0, 1);
                    break;
                default:
                    throw new ArgumentException("invalid axis");
            }
            return rotate;

        }

        public List<Vertex> RotateFigPoints(string axis, List<Vertex> points, int degree)
        {
            Matrix rotate = GetRotationAroundAxisMatrix(axis, degree);
            List<Vertex> result = new List<Vertex>();
            points.ForEach(p => {
                Matrix m = new Matrix(4, 1).Fill(p.XF, p.YF, p.ZF, 1);
                var t = rotate * m;
                result.Add(new Vertex(t[0, 0], t[1, 0], t[2, 0]));
            });


            return result;
        }


        public float EvalFunc(string func, float x, float y) {
            Entity f = func.Replace("x", x.ToString()).Replace("y", y.ToString()).Replace(",", ".");
            return (float)f.EvalNumerical();
        }

        public Matrix PerspectiveProjection(float left, float right, float bottom, float top, float near, float far) {

            var a = 2 * near / (right - left);
            var b = (right + left) / (right - left);
            var c = 2 * near / (top - bottom);
            var d = (top + bottom) / (top - bottom);
            var e = -(far + near) / (far - near);
            var f = -2 * far * near / (far - near);
            return new Matrix(4, 4).Fill(

                 a, 0, b, 0,
                 0, c, d, 0,
                 0, 0, e, f,
                 0, 0, -1, 0
                );
        }

        public void RotateVectors(ref Vector vector1, ref Vector vector2, float angle, Vector axis)
        {
            axis.Normalize();
            float l = axis.XF;
            float m = axis.YF;
            float n = axis.ZF;
            float anglesin = (float)Math.Sin(DegreeToRadian(angle));
            float anglecos = (float)Math.Cos(DegreeToRadian(angle));
            Matrix rotation = new Matrix(4, 4).Fill(l * l + anglecos * (1 - l * l),
                l * (1 - anglecos) * m - n * anglesin, l * (1 - anglecos) * n + m * anglesin, 0,
                l * (1 - anglecos) * m + n * anglesin, m * m + anglecos * (1 - m * m),
                m * (1 - anglecos) * n - l * anglesin, 0,
                l * (1 - anglecos) * n - m * anglesin, m * (1 - anglecos) * n + l * anglesin,
                n * n + anglecos * (1 - n * n), 0,
                0, 0, 0, 1);

            var res = rotation * new Matrix(4, 1).Fill(vector1.XF, vector1.YF, vector1.ZF, 1);
            vector1 = new Vector(res[0, 0], res[1, 0], res[2, 0]).Normalize();
            res = rotation * new Matrix(4, 1).Fill(vector2.XF, vector2.YF, vector2.ZF, 1);
            vector2 = new Vector(res[0, 0], res[1, 0], res[2, 0]).Normalize();
        }
    }
}
