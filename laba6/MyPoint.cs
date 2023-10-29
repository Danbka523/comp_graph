using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace laba6
{
    internal class MyPoint
    {
        float x, y, z;
        public static PointF world;

        public MyPoint(float x, float y, float z) {
            this.x = x; this.y = y; this.z = z;
        }

        public int X { get => (int)x; set => x = value; }
        public int Y { get => (int)y; set => x = value; }
        public int Z { get => (int)z; set => x = value; }

        public float XF { get => x; set => x = value; }
        public float YF { get => y; set => x = value; }
        public float ZF { get => z; set => x = value; }

        public PointF Project() {
            return new PointF(world.X + x, world.Y + y);
        
        }


    }
}
