using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba8
{
    internal class Vertex : Point
    {
        public Vector normVector;

        public Vertex(int x, int y, int z) : base(x, y, z) { }


        public Vertex(float x, float y, float z) : base(x, y, z)
        {

        }

        public Vertex(Point p) : base(p) { }

        public Vertex(Point p, Vector normVector) : base(p)
        {
            this.normVector = normVector;


        }
    }
}
