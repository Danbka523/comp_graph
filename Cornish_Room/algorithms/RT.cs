using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace laba7
{
    internal class RT
    {
        Scene scene;
        public RT(Scene scene) {
            this.scene = scene;
        
        }


        public Bitmap BackWardRT(int width, int height, Point[,] pixels) {
            Bitmap res = new Bitmap(width, height);
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Ray ray = new Ray(pixels[i, j], new Vector(scene.cameraPos, pixels[i, j]).Normalize());
                    Vector color = RayTracing(ray,10);
                    res.SetPixel(i, j, Color.FromArgb((int)(255 * color.XF), (int)(255 * color.YF), (int)(255 * color.ZF)));

                }
            }

            //List<List<Vector>> colors = Enumerable.Repeat(new List<Vector>(), width).ToList();

            //Bitmap res = new Bitmap(width, height);
            //Parallel.For(0, width, i => {
            //    Parallel.For(0, height, j => {
            //        Ray ray = new Ray(pixels[i, j], new Vector(scene.cameraPos, pixels[i, j]).Normalize());
            //        Vector color = RayTracing(ray, 5);
            //        colors[i].Add(color);

            //    });
            //});


            //for (int i = 0; i < width; i++)
            //{
            //    for (int j = 0; j < height; j++)
            //    {

            //        Vector color = colors[i][j];
            //        res.SetPixel(i, j,
            //          Color.FromArgb((int)(255 * color.XF), (int)(255 * color.YF), (int)(255 * color.ZF)));

            //    }
            //}

            return res;
        
        }
        
        public Vector RayTracing(Ray ray, int iter) {
            Vector color = new Vector(0, 0, 0);
            if (iter == 0)
                return color;


            bool refractFigure = false;


            float intersection = FindClosest(ray, out Vector normal, out Material material);


            if (intersection == 0)
                return color;


            if (ray.Direction.Scalar(normal) > 0)
            {
                normal *= -1;
                refractFigure = true;
            }

      
            Point reachPoint = new Vector(ray.Direction * intersection + new Vector(ray.Start));

            //из презентации 
            //В точке пересечения луча с объектом строится три вторичных 
            //луча – один в направлении отражения (1), второй – в направлении 
            //источника света (2), третий в направлении преломления 
            //прозрачной поверхностью (3)

            //ambient
            Vector ambientR = scene.lightSource.Color * material.Ambient;
            ambientR = new Vector(ambientR.XF * material.Color.XF, ambientR.YF * material.Color.YF,ambientR.ZF * material.Color.ZF);
            color += ambientR;
            

            //diffuse
            if (IsVisible(scene.lightSource, reachPoint, scene))
                color += scene.lightSource.Shade(normal, material.Color, material.Diffuse);


            if (material.Reflection > 0)
            {
                Ray reflectionRay = ray.Reflect(reachPoint, normal);
                color += material.Reflection * RayTracing(reflectionRay, iter - 1);
            }

            if (material.Refraction > 0)
            {
                //коэффициент преломления
                float refractRatio;
                //если угол острый получился, то
                if (refractFigure)
                    refractRatio = material.Environment;
                else
                    refractRatio = 1 / material.Environment;

                Ray transparencyRay = ray.Refract(reachPoint, normal, material.Refraction, refractRatio);
                if (transparencyRay != null)
                    color += material.Refraction * RayTracing(transparencyRay, iter - 1);
            }



            if (color.XF > 1.0f || color.YF > 1.0f || color.ZF > 1.0f)
                return color.Normalize();
            return color;
        
        }

        public float FindClosest(Ray ray, out Vector normal, out Material material) {
            float intersectionPoint = 0;;
            normal = null;
            material = new Material();
            foreach (var figure in scene.figures)
            {

                if (ray.Intersection(figure, out float intersect, out Vector norm)) {
                    if (intersect < intersectionPoint|| intersectionPoint == 0)
                    {
                        intersectionPoint = intersect;
                        normal = norm;
                        material = new Material(figure.material);
                    }

                }
            }
            return intersectionPoint;
            
        }

        public static bool IsVisible(LightSource light, Point reachPoint, Scene scene)
        {
            float length = new Vector(reachPoint, light.Position).Length();
            Ray ray = new Ray(reachPoint, new Vector(light.Position,reachPoint));
            Vector normal;
            float res;
            foreach (var fig in scene.figures)
            {
                if (ray.Intersection(fig,out res,out normal))
                    if (res < length && res > Polyhedron.eps)
                        return false;
            }

            return true;
        }

    }
}
