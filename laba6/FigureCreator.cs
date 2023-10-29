using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba6
{
    internal class FigureCreator
    {
       public Polyhedron getTr() {
            Polyhedron res = new Polyhedron();
            MyPoint a = new MyPoint(0, 0, 0);
            MyPoint b = new MyPoint(200, 0, 200);
            MyPoint c = new MyPoint(200, 200, 0);
            MyPoint d = new MyPoint(0, 200, 200);
            res.AddPolygon(new Polygon().Add(new Line(a, c)).Add(new Line(c, b)).Add(new Line(b, a)));
            res.AddPolygon(new Polygon().Add(new Line(c, b)).Add(new Line(b, d)).Add(new Line(d, c)));
            res.AddPolygon(new Polygon().Add(new Line(b, d)).Add(new Line(d, a)).Add(new Line(a, b)));
            res.AddPolygon(new Polygon().Add(new Line(c, d)).Add(new Line(d, a)).Add(new Line(a, c)));
            return res;

        }
        

    }
}
