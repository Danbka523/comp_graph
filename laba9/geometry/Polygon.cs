using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics;
using System.Text;
using System.Threading.Tasks;
using static AngouriMath.MathS;

namespace laba7
{
    internal class Polygon
    {
        List<Vertex> vertices;
        public Vector normVector; //мне уже конкретно не норм
        public bool isFacial {  get; set; }  
        public Polygon(Vector normVector=null) { 
            vertices = new List<Vertex>();
            isFacial = true;
            this.normVector= normVector;
        }

        public Polygon(List<Vertex> vertices)
        {
            this.vertices = new List<Vertex>(vertices);
            isFacial = true;
        }

        public Polygon(params Vertex[] vertices)
        {
            this.vertices = new();
            this.vertices.AddRange(vertices);
            isFacial = true;
        }

        public Polygon Add(Vertex vert)
        {
            vertices.Add(vert);
            return this;
        }

        public Polygon Add(List<Vertex> vertices)
        {
            this.vertices.AddRange(vertices);
            return this;
        }

        public Polygon Add(params Vertex[] vertices)
        {
            this.vertices.AddRange(vertices);
            return this;
        }

        public List<Vertex> Verts{ get => vertices; }

        public Point GetCenter() {
            float x=0, y=0, z = 0;
            foreach (var vert in vertices)
            {
                x += vert.XF;
                y += vert.YF;
                z += vert.ZF;
            }
            float centX = x/vertices.Count;
            float centY = y/vertices.Count;
            float centZ = z/vertices.Count;

            Point res =new Point(centX, centY, centZ);
            return res;
        }

        public Vector GetNorm() {
            if (normVector == null)
            {
                var v1 = new Vector(vertices[0], vertices[1]);
                var v2 = new Vector(vertices[0], vertices.Last());
                normVector= -(v1 * v2);
            }
            return normVector;
        }
        public override string ToString()
        {
            string res = "";
            foreach (var vert in Verts)
            {
                res += vert.ToString() + " ";
            }
            return res;
        }
    }
}
