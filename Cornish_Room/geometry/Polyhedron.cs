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
        public static float eps = 0.0001f;
        List<Polygon> polygons;
        public bool isHighLighthed;
        public Material material;
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

        public void SetPen(Pen pen)
        {
            polygons.ForEach(polygon => { polygon.pen = pen; });
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
