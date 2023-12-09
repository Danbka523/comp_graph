using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace laba7
{

    class Lighting
    {

        public static float GetLightness(Vertex v, LightSource light)
        {
            var normv = v.normVector.Normalize();
            var raytovertex = new Vector(light.Position,v).Normalize();
            float cos = Math.Max(normv.Cos(raytovertex), 0.0f);
            return cos;
        }


        public static float GetIntense(float lightness)
        {
            return (lightness + 1) / 2;//у Яны в презентации (1+cos)/2
            //return lightness*0.3*0.7;//что-то интуитивные коэффициенты не помогают

        }


        public static Vector NormalVertex(List<Polygon> polys)
        {
            Vector res = new Vector(0, 0, 0);
            foreach (var poly in polys)
            {
                var norm = poly.GetNorm();
                res.XF += norm.XF;
                res.YF += norm.YF;
                res.ZF += norm.ZF;
            }
            res.XF /= polys.Count();
            res.YF /= polys.Count();
            res.ZF /= polys.Count();
            return res;
        }


        public static void CalculateLambert(Polyhedron s, LightSource light)
        {

            Parallel.For(0, s.Polygons.Count, i =>
            {
                Polygon f = s.Polygons[i];
                foreach (var vert in f.Verts)
                {
                    List<Polygon> polys = s.Polygons.Where(x => x.Verts.Contains(vert)).ToList();//все грани, содержащие данную вершину
                    vert.normVector = NormalVertex(polys);
                }

            });
            
         

            //посчитать вектор нормали для каждой вершины
            foreach (var f in s.Polygons)
            {
                //посчитать яркость в каждой вершине многоугольника
                foreach (var vert in f.Verts)
                {
                    float lamb = GetLightness(vert, light);
                    float intense = GetIntense(lamb);
                    vert.Intense = intense;
                }
            }
        }
    }

}