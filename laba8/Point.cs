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
        PERSPECTIVE, CENTRAL, ISOMETRIC, PARALLEL
    }

    internal class Point
    {
        float x, y, z;
        static float c = 1000f;
        public static PointF world;
        public static ProjectionKind kind = ProjectionKind.PERSPECTIVE;
        public static Size screenSize;
        public static float zScreenFar;
        public static float zScreenNear;
        public static float fov;
        public static Transformations transformations;

        // static Matrix isometric = new Matrix(3, 3).Fill((float)Math.Sqrt(3), 0, -(float)Math.Sqrt(3), 1, 2, 1, (float)Math.Sqrt(2), -(float)Math.Sqrt(2), (float)Math.Sqrt(2)) * (1 / (float)Math.Sqrt(6));
        static Matrix isometric = new Matrix(3, 3).Fill(
            (float)(1/Math.Sqrt(2)), (float)(1 / Math.Sqrt(6)), (float)(1 / Math.Sqrt(3)),
            (float)(-1 / Math.Sqrt(2)), (float)(1 / Math.Sqrt(6)), (float)(1 / Math.Sqrt(3)),
            0,(float)(-2 / Math.Sqrt(6)), (float)(1 / Math.Sqrt(3))
            );

        static Matrix perspective;

        public Point(float x, float y, float z) {
            this.x = x; this.y = y; this.z = z;
            
        }

        public int X { get => (int)x; set => x = value; }
        public int Y { get => (int)y; set => y = value; }
        public int Z { get => (int)z; set => z = value; }

        public float XF { get => x; set => x = value; }
        public float YF { get => y; set => y = value; }
        public float ZF { get => z; set => z = value; }

        public static void SetProjection(Size screenSize, float zScreenNear, float zScreenFar, float fov)
        {
            Point.screenSize = screenSize;
            Point.zScreenNear = zScreenNear;
            Point.zScreenFar = zScreenFar;
            Point.fov = fov;
            
            perspective = new Matrix(4, 4).Fill(screenSize.Height / ((float)Math.Tan(transformations.DegreeToRadian(fov / 2f)) * screenSize.Width), 0, 0, 0,
                                                0, 1.0f /(float) Math.Tan(transformations.DegreeToRadian(fov / 2f)), 0, 0,
                                                0, 0, -(zScreenFar + zScreenNear) / (zScreenFar - zScreenNear), -2f * (zScreenFar * zScreenNear) / (zScreenFar - zScreenNear),
                                                0, 0, -1, 0);

        }
        public (PointF?, float) Projection(Camera cam) {

            var viewCoord = cam.ToCameraView(this);

            switch (kind)
            {
                case ProjectionKind.PARALLEL:
                    if (viewCoord.ZF > 0)
                    {
                        return (new PointF(world.X + viewCoord.XF, world.Y + viewCoord.YF), ZF);
                    }
                    return (null, -1);
                case ProjectionKind.PERSPECTIVE:
                    if (viewCoord.ZF < 0)
                    {
                        return (null,ZF);
                    }
                 
                    Matrix res = new Matrix(1, 4).Fill(viewCoord.XF, viewCoord.YF, viewCoord.ZF, 1) * perspective;
                    if (res[0, 3] == 0)
                    {
                        return (null, ZF);
                    }
                    res *= 1.0f / res[0, 3];
                    res[0, 0] = Math.Clamp(res[0, 0], -1, 1);
                    res[0, 1] = Math.Clamp(res[0, 1], -1, 1);
                    if (res[0, 2] < 0)
                    {
                        return (null, ZF);
                    }
                    return (new PointF(world.X + res[0, 0] * world.X, world.Y + res[0, 1] * world.Y),ZF);

                default:
                    throw new ArgumentException("invalid perspective");
            }      
        }

        public override string ToString()
        {
            return $"{XF} {YF} {ZF}";
        }
    }
}
 