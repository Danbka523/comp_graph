using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static Antlr4.Runtime.Atn.SemanticContext;

namespace laba9
{
    internal class Polyhedron
    {
        List<Polygon> polygons;
        private Color shapeColor;
        public Polyhedron(Color color)
        {
            polygons = new List<Polygon>();
            if (color.IsEmpty)
            {
                Random rnd = new();
                shapeColor = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
            }
            else
                shapeColor = color;
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
        public Color GetColor { get { return shapeColor; } }    
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
