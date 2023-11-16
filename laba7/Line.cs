using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba7
{
    internal class Line
    {
        Point start, end;

        public Line(Point start, Point end)
        {
            this.start = start;
            this.end = end;
        }

        public Point Start { get => start; set => start=value; }
        public Point End { get => end; set => end=value; }

    }
}
