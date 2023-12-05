using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba9
{
    internal class LightingSource
    {
        Point position;

        public LightingSource(Point position)
        {
            this.position = position;
        }

        public Point Position => position;

        public void Move(float shiftX = 0, float shiftY = 0, float shiftZ = 0)
        {
            position.XF += shiftX;
            position.YF += shiftY;
            position.ZF += shiftZ;
        }
    }
}
