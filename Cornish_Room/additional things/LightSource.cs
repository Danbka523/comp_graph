using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba7
{
    internal class LightSource
    {
        Point position;
        Vector color; //[0.0,1.0]
        public LightSource(Point position, Vector color=null) {
            this.position = position;
            this.color = color;
        }

        public Point Position { get=>position; set=>position=value; }
        public Vector Color { get=>color; set=>color=value; }    

        public void Move(float x, float y, float z) {
            position.XF += x;
            position.YF += y;
            position.ZF += z;      
        }

        public Vector Shade( Vector normal, Vector matColor, float diffCoef)
        {
            Vector dir = new Vector(Position, new Point(normal.XF, normal.YF, normal.ZF)).Normalize();
            float coefCos = Math.Max(normal.Cos(dir), 0);

            Vector diff = diffCoef * coefCos * Color;
            return new Vector(diff.XF * matColor.XF, diff.YF * matColor.YF, diff.ZF * matColor.ZF);
        }


    }
}
