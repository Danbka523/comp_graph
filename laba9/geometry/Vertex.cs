using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace laba7
{
    internal class Vertex : Point
    {
        public Vector normVector;


        public Vertex(int x, int y, int z, Vector norm = null) : base(x, y, z) {
            normVector = norm;
        }


        public Vertex(float x, float y, float z, Vector norm=null) : base(x, y, z)
        {
            normVector = norm;
        }

        public Vertex(Point p, Vector norm=null) : base(p) {
            normVector = norm;
        }


    }
}
