using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace laba8
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
        public Point(float x, float y, float z) {
            this.x = x; this.y = y; this.z = z;
        }

        public int X { get => (int)x; set => x = value; }
        public int Y { get => (int)y; set => x = value; }
        public int Z { get => (int)z; set => x = value; }

        public float XF { get => x; set => x = value; }
        public float YF { get => y; set => x = value; }
        public float ZF { get => z; set => x = value; }

        public PointF Projection() {
            if (kind == ProjectionKind.PERSPECTIVE)
            {
               // Matrix res = new Matrix(1, 4).Fill(XF, YF, ZF, 1) * perspective * (float)(1 / (-c * ZF + 1));
                Matrix res = new Matrix(1, 4).Fill(XF, YF, ZF, 1) * new Matrix(4,4).Fill(
                    1,0,0,0,
                    0,1,0,0,
                    0,0,0,0,
                    0,0,-1f/c,1) * (1-z/c);
                return new PointF(world.X + res[0, 0], world.Y + res[0, 1]);
            }
            else
            {
               // Matrix res = new Matrix(3, 3).Fill(1, 0, 0, 0, 1, 0, 0, 0, 0) * isometric * new Matrix(3, 1).Fill(XF, YF, ZF);
                Matrix res = new Matrix(1, 3).Fill(XF, YF, ZF) * isometric;
               // return new PointF(world.X + res[0, 0], world.Y + res[1, 0]);
                return new PointF(world.X + res[0, 0], world.Y + res[0, 1]);
            }
        }
    }
}
 