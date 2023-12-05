using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace laba8
{
    internal class Polyhedron
    {
        List<Polygon> polygons;

        public Polyhedron()
        {
            polygons = new List<Polygon>();
        }

        public Polyhedron AddPolygon(Polygon poly)
        {
            polygons.Add(poly);
            return this;
        }

        public Polyhedron AddPolygons(List<Polygon> polys)
        {
            polygons.AddRange(polys);
            return this;
        }

        public List<Polygon> Polygons { get { return polygons; } }
        public List<Vertex> GetVerts() { 
            List<Vertex> verts = new List<Vertex>();
            foreach (Polygon poly in polygons)
            {
                foreach (var item in poly.Verts)
                {
                    if (!verts.Contains(item))
                        verts.Add(item);
                }
            }
            return verts;
        } 
        public void ResetFacial()
        {
            foreach (Polygon poly in polygons)
                poly.isFacial = true;
        }

    }


}
