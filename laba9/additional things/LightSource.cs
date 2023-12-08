using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba7
{
    internal class LightSource
    {
        Point position= new Point(0,0,0);
        public LightSource(Point position) {
            this.position = position;
        }

        public Point Position { get=>position; set=>position=value; }

        public void Move(float x, float y, float z) {
            position.XF += x;
            position.YF += y;
            position.ZF += z;      
        }
    }
}
