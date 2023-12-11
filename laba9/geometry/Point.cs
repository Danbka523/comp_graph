using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace laba7
{
    public enum ProjectionKind
    {
        PERSPECTIVE, CENTRAL, ISOMETRIC
    }

    internal class Point
    {
        float x, y, z;
        static float c = 1000f;
        public static PointF world;
        public static ProjectionKind kind = ProjectionKind.PERSPECTIVE;
        static Transformations transformations = new Transformations();
        static Matrix perspectiveProjectionMatrix;
        float intense;
        public static Size screenSize;

        //public Matrix isometric = new Matrix(4, 4).Fill(
        //    (float)Math.Cos(2), 0, (float)Math.Sin(2), 0,
        //    (float)Math.Sin(4) * (float)Math.Sin(2), (float)Math.Cos(4), -(float)Math.Sin(4) * (float)Math.Cos(2), 0,
        //    0, 0, 0, 0,
        //    0, 0, 0, 1);
        // static Matrix isometric = new Matrix(3, 3).Fill((float)Math.Sqrt(3), 0, -(float)Math.Sqrt(3), 1, 2, 1, (float)Math.Sqrt(2), -(float)Math.Sqrt(2), (float)Math.Sqrt(2)) * (1 / (float)Math.Sqrt(6));
        static Matrix isometric = new Matrix(3, 3).Fill(
            (float)(1/Math.Sqrt(2)), (float)(1 / Math.Sqrt(6)), (float)(1 / Math.Sqrt(3)),
            (float)(-1 / Math.Sqrt(2)), (float)(1 / Math.Sqrt(6)), (float)(1 / Math.Sqrt(3)),
            0,(float)(-2 / Math.Sqrt(6)), (float)(1 / Math.Sqrt(3))
            );


        public Point(float x, float y, float z, float intense=0 ) {
            this.x = x; this.y = y; this.z = z; this.intense = intense; 
  
        }

        public Point(Point p)
        {
            x = p.x; y = p.y; z = p.z; intense = p.intense;
            transformations = new Transformations();
        }

        public int X { get => (int)x; set => x = value; }
        public int Y { get => (int)y; set => y = value; }
        public int Z { get => (int)z; set => z = value; }

        public float XF { get => x; set => x = value; }
        public float YF { get => y; set => y = value; }
        public float ZF { get => z; set => z = value; }

        public float Intense { get=> intense; set => intense = value; }
        public static Point operator -(Point v)
        {
            return new Point(-1 * v.x, -1 * v.y, -1 * v.z);
        }
        public override string ToString()
        {
            return $"({XF} {YF} {ZF})";
        }
        public static Point operator *(Point b, Matrix m)
        {
            var res = new Point(0, 0, 0);
            for (int i = 0; i < 4; i++)
            {
                res.x += b.XF * m[0, i];
                res.y += b.XF * m[1, i];
                res.z += b.XF * m[2, i];
            }

            return res;
        }

        public static void SetProjection(Size screenSize, float zScreenNear, float zScreenFar, float fov)
        {
            perspectiveProjectionMatrix = new Matrix(4, 4).Fill(
                screenSize.Height / (float)(Math.Tan(transformations.DegreeToRadian(fov / 2)) * screenSize.Width), 0, 0, 0, 0,
                1.0f / (float)Math.Tan(transformations.DegreeToRadian(fov / 2)), 0, 0, 0, 0,
                -(zScreenFar + zScreenNear) / (zScreenFar - zScreenNear),
                -2 * (zScreenFar * zScreenNear) / (zScreenFar - zScreenNear), 0, 0, -1, 0);
        }
        public (PointF?,float) Projection(Camera cam) {
            var viewCoord = cam.ToCameraView(this);
            if (kind == ProjectionKind.PERSPECTIVE)
            {
                if (viewCoord.ZF < 0)
                {
                    return (null, (float)viewCoord.ZF);
                }

                Matrix res = new Matrix(1, 4).Fill(viewCoord.XF, viewCoord.YF, viewCoord.ZF, 1) *
                             perspectiveProjectionMatrix;
                if (res[0, 3] == 0)
                {
                    return (null, viewCoord.ZF);
                }

                res *= 1.0f / res[0, 3];
                res[0, 0] = Math.Clamp(res[0, 0], -1, 1);
                res[0, 1] = Math.Clamp(res[0, 1], -1, 1);
                if (res[0, 2] < 0)
                {
                    return (null, viewCoord.ZF);
                }

                return (
                    new PointF(world.X + res[0, 0] * world.X,
                        world.Y + (res[0, 1] * world.Y)), (float)viewCoord.ZF);
            }
            else
            {
                Matrix res = new Matrix(1, 3).Fill(XF, YF, ZF) * isometric;
                return (new PointF(world.X + res[0, 0], world.Y + res[0, 1]),res[0,2]);
            }
        }

        public PointF? Projection() {
            return new PointF(Math.Clamp(X, 0, screenSize.Width - 1), Math.Clamp((int)(world.Y - YF), 0, screenSize.Height - 1));
        }
    }
}
 