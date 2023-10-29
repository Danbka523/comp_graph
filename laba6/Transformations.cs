using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace laba6
{
    internal class Transformations
    {
        double DegreeToRadian(float degree) => Math.PI * degree / 180;
        MyPoint CenterOfFigure(Polyhedron figure)
        {
            float x = figure.Polygons.Average(p=>p.GetCenter().XF);
            float y = figure.Polygons.Average(p=>p.GetCenter().YF);
            float z = figure.Polygons.Average(p=>p.GetCenter().ZF);

            return new MyPoint(x, y, z);

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

                    line.Start = new MyPoint(res_start[0, 0], res_start[1, 0], res_start[2, 0]);
                    line.End = new MyPoint(res_end[0, 0], res_end[1, 0], res_end[2, 0]);
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

                    line.Start = new MyPoint(res_start[0, 0], res_start[1, 0], res_start[2, 0]);
                    line.End = new MyPoint(res_end[0, 0], res_end[1, 0], res_end[2, 0]);
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

                    line.Start = new MyPoint(res_start[0, 0], res_start[1, 0], res_start[2, 0]);
                    line.End = new MyPoint(res_end[0, 0], res_end[1, 0], res_end[2, 0]);
                }
            }
        }

        public void RotateAroundAxis(Polyhedron figure, float degree, string axis) {
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

            foreach (var poly in figure.Polygons)
            {
                foreach (var line in poly.Lines)
                {
                    var res_start = rotate * new Matrix(4, 1).Fill(line.Start.XF, line.Start.YF, line.Start.ZF, 1);
                    var res_end = rotate * new Matrix(4, 1).Fill(line.End.XF, line.End.YF, line.End.ZF, 1);

                    line.Start = new MyPoint(res_start[0, 0], res_start[1, 0], res_start[2, 0]);
                    line.End = new MyPoint(res_end[0, 0], res_end[1, 0], res_end[2, 0]);
                }
            }

        }
        public void RotateAroundCustomAxis(Polyhedron figure, float degree, string axis) { }
        public void RotateAroundCenterAxis(Polyhedron figure, float degree, string axis) {

            MyPoint center = CenterOfFigure(figure);

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

                    line.Start = new MyPoint(res_start[0, 0], res_start[1, 0], res_start[2, 0]);
                    line.End = new MyPoint(res_end[0, 0], res_end[1, 0], res_end[2, 0]);
                }
            }

            RotateAroundAxis(figure, degree, axis);

            rotate = new Matrix(4, 4).Fill(
       1, 0, 0, center.X,
       0, 1, 0,center.Y,
       0, 0, 1, center.Z,
       0, 0, 0, 1
       );
            foreach (var poly in figure.Polygons)
            {
                foreach (var line in poly.Lines)
                {
                    var res_start = rotate * new Matrix(4, 1).Fill(line.Start.XF, line.Start.YF, line.Start.ZF, 1);
                    var res_end = rotate * new Matrix(4, 1).Fill(line.End.XF, line.End.YF, line.End.ZF, 1);

                    line.Start = new MyPoint(res_start[0, 0], res_start[1, 0], res_start[2, 0]);
                    line.End = new MyPoint(res_end[0, 0], res_end[1, 0], res_end[2, 0]);
                }
            }

        }
    }
}
