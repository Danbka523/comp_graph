using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace laba7
{
    internal class Polyhedron
    {
        List<Polygon> polygons;

        public Polyhedron() { 
            polygons = new List<Polygon>();
        }

        public Polyhedron AddPolygon(Polygon poly) {
            polygons.Add(poly);
            return this;
        }

        public Polyhedron AddPolygons(List<Polygon> polys) { 
            polygons.AddRange(polys);
            return this;
        }

        public List<Polygon> Polygons { get { return polygons; } }

        public List<Point> GetVerts() { 
            List<Point> verts = new();
            foreach (Polygon poly in polygons)
            {
                foreach (Line lines in poly.Lines)
                {
                    if (verts.FindIndex(x=>x.X==lines.Start.X && x.Y==lines.Start.Y && x.Z==lines.Start.Z)==-1)
                        verts.Add(lines.Start);
                }

            }

            return verts;
        }

    
    }


}
