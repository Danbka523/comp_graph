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
                    if (verts.FindIndex(x=>x.XF==lines.Start.XF && x.YF==lines.Start.YF && x.ZF==lines.Start.ZF)==-1)
                        verts.Add(lines.Start);
                    if (verts.FindIndex(x=>x.XF==lines.End.XF && x.YF==lines.End.YF && x.ZF==lines.End.ZF)==-1)
                        verts.Add(lines.End);
                }

            }

            return verts;
        }

    
    }


}
