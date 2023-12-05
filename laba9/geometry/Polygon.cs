using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using static AngouriMath.MathS;

namespace laba9
{
    internal class Polygon
    {
        Vector normVector;
        List<Line> lines;
        List<Vertex> verts;
        public bool isFacial = true;
        public Polygon()
        {
            verts = new();
            normVector = new Vector(0, 0, 0);
        }

        public Polygon(IEnumerable<Vertex> verts) : this()
        {
            this.verts.AddRange(verts);
        }

        public Polygon(params Vertex[] verts) : this()
        {
            this.verts.AddRange(verts);
        }

        public Polygon Add(Vertex vert)
        {
            verts.Add(vert);
            return this;
        }

        public Polygon AddVerts(IEnumerable<Vertex> verts)
        {
            this.verts.AddRange(verts);
            return this;
        }

        public Polygon AddVerts(params Vertex[] verts)
        {
            this.verts.AddRange(verts);
            return this;
        }


        public Vector NormVector
        {
            get
            {
                Vector vect1 = new Vector(verts.First(), verts[1]);
                Vector vect2 = new Vector(verts.First(), verts.Last());
                normVector = vect2 * vect1;
                return normVector; //а вот мне не норм уже
            }
        }

        public List<Vertex> Verts
        {
            get => verts;
        }

        public Point GetCenter()
        {
            float x = 0, y = 0, z = 0;
            foreach (var vert in verts)
            {
                x += vert.XF;
                y += vert.YF;
                z += vert.ZF;
            }
            float centX = x / verts.Count;
            float centY = y / verts.Count;
            float centZ = z / verts.Count;

            return new Point(centX, centY, centZ);

        }

        public override string ToString()
        {
            string res="";
            foreach (var vert in verts)
            {
                res += vert.ToString()+" ";
            }
            return res;
        }


    }
}
