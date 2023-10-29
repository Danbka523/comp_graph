using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba6
{
    internal class Polygon
    {
        List<Line> lines;

        public Polygon() { 
            lines = new List<Line>();
        }

        public Polygon(List<Line> lines)
        {
            this.lines.AddRange(lines);
        }

        public Polygon Add(Line line)
        {
            lines.Add(line);
            return this;
        }

        public Polygon Add(List<Line> lines)
        {
            this.lines.AddRange(lines);
            return this;
        }

        public List<Line> Lines{ get => lines; }

        public MyPoint GetCenter() {
            float x=0, y=0, z = 0;
            foreach (var line in lines)
            {
                x += line.Start.XF;
                y += line.Start.YF;
                z += line.Start.ZF;
            }
            float centX = x/lines.Count;
            float centY = y/lines.Count;
            float centZ = z/lines.Count;

            MyPoint res =new MyPoint(centX, centY, centZ);
            return res;
        }


    }
}
