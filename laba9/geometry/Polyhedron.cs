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
        public bool isHighLighthed;
        Color color;
  
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

        public Color GetColor() => color;
        public void SetColor(Color c) => color = c;


        public List<Polygon> Polygons { get { return polygons; } }

        public List<Vertex> GetVerts() {
            List<Vertex> verts=new();

            foreach (var poly in polygons)
            {
                foreach (var vert in poly.Verts)
                {
                    if (!verts.Contains(vert))
                         verts.Add(vert);
                }
            }

            return verts;
        }

        public void ResetFacial() {
            foreach (var poly in polygons)
                poly.isFacial = true;
        }
    
        
    
    }


}
