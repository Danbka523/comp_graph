using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;

namespace laba7
{
    internal class Transformations
    {
        public double DegreeToRadian(float degree) => Math.PI * degree / 180;
        Point CenterOfFigure(Polyhedron figure)
        {
            float x = figure.Polygons.Average(p=>p.GetCenter().XF);
            float y = figure.Polygons.Average(p=>p.GetCenter().YF);
            float z = figure.Polygons.Average(p=>p.GetCenter().ZF);

            return new Point(x, y, z);

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
                foreach (var line in poly.Lines)
                {
                    var res_start = mirror * new Matrix(4, 1).Fill(line.Start.XF, line.Start.YF, line.Start.ZF, 1);
                    var res_end = mirror * new Matrix(4, 1).Fill(line.End.XF, line.End.YF, line.End.ZF, 1);

                    line.Start = new Point(res_start[0, 0], res_start[1, 0], res_start[2, 0]);
                    line.End = new Point(res_end[0, 0], res_end[1, 0], res_end[2, 0]);
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
                foreach (var line in poly.Lines)
                {
                    var res_start = shift * new Matrix(4, 1).Fill(line.Start.XF, line.Start.YF, line.Start.ZF, 1);
                    var res_end = shift * new Matrix(4, 1).Fill(line.End.XF, line.End.YF, line.End.ZF, 1);

                    line.Start = new Point(res_start[0, 0], res_start[1, 0], res_start[2, 0]);
                    line.End = new Point(res_end[0, 0], res_end[1, 0], res_end[2, 0]);
                }
            }
        }

        public void Scale(Polyhedron figure, float sX, float sY, float sZ) { 
            Matrix scale = new Matrix(4, 4).Fill(
                sX,0,0,0,
                0,sY,0,0,
                0,0,sZ,0,
                0,0,0,1
                );
            foreach (var poly in figure.Polygons)
            {
                foreach (var line in poly.Lines)
                {
                    var res_start = scale * new Matrix(4, 1).Fill(line.Start.XF, line.Start.YF, line.Start.ZF, 1);
                    var res_end = scale * new Matrix(4, 1).Fill(line.End.XF, line.End.YF, line.End.ZF, 1);

                    line.Start = new Point(res_start[0, 0], res_start[1, 0], res_start[2, 0]);
                    line.End = new Point(res_end[0, 0], res_end[1, 0], res_end[2, 0]);
                }
            }
        }

        public void RotateAroundAxis(Polyhedron figure, float degree, string axis) {

            Matrix rotate=GetRotationAroundAxisMatrix(axis,degree);

            foreach (var poly in figure.Polygons)
            {
                foreach (var line in poly.Lines)
                {
                    var res_start = rotate * new Matrix(4, 1).Fill(line.Start.XF, line.Start.YF, line.Start.ZF, 1);
                    var res_end = rotate * new Matrix(4, 1).Fill(line.End.XF, line.End.YF, line.End.ZF, 1);

                    line.Start = new Point(res_start[0, 0], res_start[1, 0], res_start[2, 0]);
                    line.End = new Point(res_end[0, 0], res_end[1, 0], res_end[2, 0]);
                }
            }

        }
        public void RotateAroundCustomAxis(Polyhedron figure, float degree, Point p1, Point p2) {
            if (p2.Z < p1.Z || (p2.Z == p1.Z && p2.Y < p1.Y) ||
              (p2.Z == p1.Z && p2.Y == p1.Y) && p2.X < p1.X)
            {
                (p1, p2) = (p2, p1);
            }


            float angleSin = (float)Math.Sin(DegreeToRadian(degree));
            float angleCos  = (float)Math.Cos(DegreeToRadian(degree));

            Point vec = new Point(p2.XF - p1.XF, p2.YF - p1.YF, p2.ZF - p1.ZF);
            float len = (float)Math.Sqrt((double)(vec.XF*vec.XF + vec.YF*vec.YF + vec.ZF*vec.ZF));

            float l = vec.XF / len;
            float m = vec.YF / len;
            float n = vec.ZF / len;

            Matrix rotate = new Matrix(4, 4).Fill(
                l * l + angleCos * (1f - l * l), l*(1f-angleCos)*m-n*angleSin,l*(1f-angleCos)*n+m*angleSin,0,
                l*(1f-angleCos)*m + n*angleSin, m*m+angleCos*(1f-m*m),m*(1f-angleCos)*n-l*angleSin,0,
                l*(1f-angleCos)*n-m*angleSin,m*(1f-angleCos)*n+l*angleSin,n*n+angleCos*(1f-n*n),0,
                0,0,0,1f
                );

            foreach (var poly in figure.Polygons)
            {
                foreach (var line in poly.Lines)
                {
                    var res_start = rotate * new Matrix(4, 1).Fill(line.Start.XF, line.Start.YF, line.Start.ZF, 1f);
                    var res_end = rotate * new Matrix(4, 1).Fill(line.End.XF, line.End.YF, line.End.ZF, 1f);

                    line.Start = new Point(res_start[0, 0], res_start[1, 0], res_start[2, 0]);
                    line.End = new Point(res_end[0, 0], res_end[1, 0], res_end[2, 0]);
                }
            }
        }
        public void RotateAroundCenterAxis(Polyhedron figure, float degree, string axis) {

            Point center = CenterOfFigure(figure);

            Matrix rotate;
            rotate = new Matrix(4, 4).Fill(
                1, 0, 0, -center.X,
                0, 1, 0, -center.Y,
                0, 0, 1, -center.Z,
                0, 0, 0, 1
                );
            foreach (var poly in figure.Polygons)
            {
                foreach (var line in poly.Lines)
                {
                    var res_start = rotate * new Matrix(4, 1).Fill(line.Start.XF, line.Start.YF, line.Start.ZF, 1);
                    var res_end = rotate * new Matrix(4, 1).Fill(line.End.XF, line.End.YF, line.End.ZF, 1);

                    line.Start = new Point(res_start[0, 0], res_start[1, 0], res_start[2, 0]);
                    line.End = new Point(res_end[0, 0], res_end[1, 0], res_end[2, 0]);
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
                foreach (var line in poly.Lines)
                {
                    var res_start = rotate * new Matrix(4, 1).Fill(line.Start.XF, line.Start.YF, line.Start.ZF, 1);
                    var res_end = rotate * new Matrix(4, 1).Fill(line.End.XF, line.End.YF, line.End.ZF, 1);

                    line.Start = new Point(res_start[0, 0], res_start[1, 0], res_start[2, 0]);
                    line.End = new Point(res_end[0, 0], res_end[1, 0], res_end[2, 0]);
                }
            }

        }

        private Matrix GetRotationAroundAxisMatrix(string axis,float degree)
        {
            axis = axis.Trim();
            double rad = DegreeToRadian(degree);
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

        public List<Point> RotateFigPoints(string axis, List<Point> points, int degree)
        {
            Matrix rotate = GetRotationAroundAxisMatrix(axis, degree);
            List<Point> result = new List<Point>();
            points.ForEach(p=>{
                Matrix m = new Matrix(4, 1).Fill(p.XF, p.YF, p.ZF, 1);
                var t = rotate * m;
                result.Add(new Point(t[0, 0], t[1, 0], t[2, 0]));
            });


            return result;
        }

    }
}
