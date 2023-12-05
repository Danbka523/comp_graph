using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba8
{
    internal class Line
    {
        Point start, end;

        public Line(Point start, Point end)
        {
            this.start = start;
            this.end = end;
        }

        public Point Start { get => start; set => start = value; }
        public Point End { get => end; set => end = value; }
        public Point GetVectorCoordinates()
        {
            return new Point(end.XF - start.XF, end.YF - start.YF, end.ZF - start.ZF);
        }

        public Point GetReverseVectorCoordinates()
        {
            return new Point(start.XF - end.XF, start.YF - end.YF, start.ZF - end.ZF);
        }
    }
}
