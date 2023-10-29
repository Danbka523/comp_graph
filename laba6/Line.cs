using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba6
{
    internal class Line
    {
        MyPoint start, end;

        public Line(MyPoint start, MyPoint end)
        {
            this.start = start;
            this.end = end;
        }

        public MyPoint Start { get => start; set => start=value; }
        public MyPoint End { get => end; set => end=value; }

    }
}
