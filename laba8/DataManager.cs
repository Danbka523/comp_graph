using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.LinkLabel;

namespace laba8
{
    internal class DataManager
    {

        public DataManager() {
        
        }

        public void Save(string filename, Polyhedron figure) {
            var verts = figure.GetVerts();
            var docPath = Environment.CurrentDirectory;
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, filename)))
            {
                foreach (var v in verts) {
                    outputFile.WriteLine($"v {v.X} {v.Y} {v.Z}");
                }
                foreach (var p in figure.Polygons)
                {
                    outputFile.Write($"f ");
                    List<int> vertsToAdd = new();
                    foreach (var line in p.Lines)
                    {                       
                        if (!vertsToAdd.Contains(verts.FindIndex(x => x.XF == line.Start.XF && x.YF == line.Start.YF && x.ZF == line.Start.ZF) + 1))
                            vertsToAdd.Add(verts.FindIndex(x => x.XF == line.Start.XF && x.YF == line.Start.YF && x.ZF == line.Start.ZF) + 1);
                        if (!vertsToAdd.Contains(verts.FindIndex(x => x.XF == line.End.XF && x.YF == line.End.YF && x.ZF == line.End.ZF) + 1))
                            vertsToAdd.Add(verts.FindIndex(x => x.XF == line.End.XF && x.YF == line.End.YF && x.ZF == line.End.ZF) + 1);
                    }
                    vertsToAdd.ForEach(vert => { outputFile.Write($"{vert} "); });
                    outputFile.WriteLine();
                }
            }
        }


        public Polyhedron Load(string filePath) {
            var verts = new List<Point>();
            var fig_lines = new List<Line>();
            var lines = File.ReadAllLines(filePath);
            int skipCount = 0;
            foreach (var line in lines)
            {
                var t = line.Split(" ");
                if (t[0] == "v") {
                    Point p = new Point(float.Parse(t[1], CultureInfo.InvariantCulture),
                        float.Parse(t[2], CultureInfo.InvariantCulture),
                        float.Parse(t[3], CultureInfo.InvariantCulture));
                    verts.Add(p);
                      
                        
                }
                if (t[0] == "f") {
                    var f = line.Split(" ",StringSplitOptions.RemoveEmptyEntries);
                    skipCount = f.Length - 1;

                    for (int i = 1; i < f.Length - 1; i++)
                    {
                        fig_lines.Add(new Line(verts[int.Parse(f[i]) - 1], verts[int.Parse(f[i + 1]) - 1]));
                    }
                    fig_lines.Add(new Line(verts[int.Parse(f[1]) - 1], verts[int.Parse(f.Last()) - 1]));
                    } 
            }

            

            List<Polygon> polygons = new List<Polygon>();
            for (int i = 0; i < fig_lines.Count(); i+=skipCount) {
                polygons.Add(new Polygon(fig_lines.Skip(i).Take(skipCount).ToList()));
            }
            return new Polyhedron().AddPolygons(polygons);
        }

    }
}
