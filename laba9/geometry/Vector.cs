using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba9
{
    internal class Vector
    {
        float x, y, z;
        public Vector(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        public Vector(Point point) : this(point.XF, point.YF, point.ZF) { }
        public Vector(Point start, Point end) : this(end.XF - start.XF, end.YF - start.YF, end.ZF - start.ZF) { }

        public Vector Normalize()
        {
            float normalization = (float)Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2) + Math.Pow(z, 2));
            x /= normalization;
            y /= normalization;
            z /= normalization;
            return this;
        }

        public int X { get => (int)x; set => x = value; }
        public int Y { get => (int)y; set => y = value; }
        public int Z { get => (int)z; set => z = value; }

        public float XF { get => x; set => x = value; }
        public float YF { get => y; set => y = value; }
        public float ZF { get => z; set => z = value; }


        public static Vector operator -(Vector v1, Vector v2)
        {
            return new Vector(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z);
        }

        public static Vector operator +(Vector v1, Vector v2)
        {
            return new Vector(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z);
        }

        public static Vector operator *(Vector a, Vector b)
        {
            return new Vector(a.y * b.z - a.z * b.y, a.z * b.x - a.x * b.z, a.x * b.y - a.y * b.x);
        }

        public static Vector operator *(float k, Vector b)
        {
            return new Vector(k * b.x, k * b.y, k * b.z);
        }
        public float Scalar(Vector other)
        {
            return x * other.XF + y * other.YF + z * other.ZF;
        }
        public float Abs() { 
            return (float)Math.Sqrt(x * x + y * y + z * z);
        }

        public float Cos(Vector other)
        {
            float scalar = Scalar(other);
            float l1 = Abs();
            float l2 = other.Abs();
            return scalar / (l1 *l2);
        }


        public override string ToString()
        {
            return $"{x} {y} {z}";
        }
    }
}
