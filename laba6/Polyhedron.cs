using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace laba6
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



    }


}
