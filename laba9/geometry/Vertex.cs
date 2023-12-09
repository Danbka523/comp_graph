using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static AngouriMath.Entity.Number;

namespace laba7
{
    internal class Vertex : Point
    {
        public Vector normVector;
        public TexturePoint texturePoint;

        public Vertex(int x, int y, int z, float intese=0f, Vector norm = null,TexturePoint tp=null) : base(x,y,z,intese) {
            if (tp == null)
                texturePoint = new TexturePoint(0, 0);
            else 
                texturePoint = tp;
            normVector = norm;
        }


        public Vertex(float x, float y, float z, float intense=0f, Vector norm=null, TexturePoint tp = null) : base(x, y, z,intense)
        {
            if (tp == null)
                texturePoint = new TexturePoint(0, 0);
            else
                texturePoint = tp;
            normVector = norm;
        }

        public Vertex(Point p, Vector norm=null, TexturePoint tp = null) : base(p) {
            if (tp == null)
                texturePoint = new TexturePoint(0, 0);
            else
                texturePoint = tp;
            normVector = norm;
        }


    }
}
