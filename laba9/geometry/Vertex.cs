
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba9
{
    internal class Vertex : Point
    {
        public Vector normVector;
        public TexturePoint texturePoint;

        public Vertex(int x, int y, int z) : base(x, y, z) { }


        public Vertex(float x, float y, float z, float u=0, float v=0) : base(x, y, z)
        {
            texturePoint = new TexturePoint(u,v);
        }

        public Vertex(Point p) : base(p) { }

        public Vertex(Point p, Vector normVector, TexturePoint texturePoint) : base(p)
        {
            this.normVector = normVector;
            this.texturePoint = texturePoint;

        }
    }
}
