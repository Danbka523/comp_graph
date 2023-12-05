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

        public void Save(string filename, Polyhedron figure)
        {
            var docPath = Environment.CurrentDirectory;
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, filename)))
            {
                List<Vertex> verts = figure.GetVerts();
                verts.ForEach(v => { outputFile.WriteLine($"v {v.XF} {v.YF} {v.ZF}"); });

                foreach (var poly in figure.Polygons)
                {
                    var normal = poly.NormVector.Normalize();
                    outputFile.WriteLine($"vn {normal.XF} {normal.YF} {normal.ZF}");
                }
                int i = 1;

                foreach (var poly in figure.Polygons)
                {
                    var str = "f ";
                   
                    poly.Verts.ForEach(v =>
                    {
                        str += $"{verts.IndexOf(v) + 1}//{i} ";
      
                    });
                    outputFile.WriteLine(str);
                    i += 1;
                }
            }
        }


        public Polyhedron Load(string filePath) {

            Polyhedron res = new Polyhedron();
            List<Point> vertices = new List<Point>();
            List<Vector> normales = new List<Vector>();
            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                var data = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                if (data.Count() == 0)
                {
                    continue;
                }
                if (data[0] == "v")
                {
                    vertices.Add(new Point(float.Parse(data[1] ),
                        float.Parse(data[2]) , float.Parse(data[3])));
                }

                if (data[0] == "vn")
                {
                    normales.Add(new Vector(float.Parse(data[1] ), float.Parse(data[2] ), float.Parse(data[3]) ));
                }

                if (data[0] == "f")
                {
                    var face = new Polygon();
                    for (int i = 1; i < data.Length; i++)
                    {
                        var stringVertex = data[i].Split("/");
                        if (stringVertex.Count() < 3)
                        {
                            break;
                        }
                        face.Add(new Vertex(vertices[int.Parse(stringVertex[0]) - 1],
                            normales[int.Parse(stringVertex[2]) - 1]));
                    }


                    res.AddPolygon(face);

                }
            }

            return res;
        }

    }
}
