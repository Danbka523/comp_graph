using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba7
{
    internal class Ray
    {
        Point start;
        Vector direction;

        public Ray(Point start, Vector direction)
        {
            this.start = start;
            this.direction = direction;
        }

        public Point Start { get => start; set => start = value; }
        public Vector Direction { get => direction; set => direction = value; }

        public Ray(Ray r) {
            start = r.Start;
            direction = r.Direction;
        }
        public Ray() { }

        //https://edu.mmcs.sfedu.ru/pluginfile.php/17922/mod_resource/content/12/%D0%9B%D0%B5%D0%BA%D1%86%D0%B8%D1%8F%2013.%20%D0%A0%D0%B5%D0%B0%D0%BB%D0%B8%D1%81%D1%82%D0%B8%D1%87%D0%BD%D1%8B%D0%B9%20%D1%80%D0%B5%D0%BD%D0%B4%D0%B5%D1%80%D0%B8%D0%BD%D0%B3.pdf
        /*
         * R=I-2*N(N*I)
         * R-reflected ray
         * I-incident ray
         * N-normal
         */
        public Ray Reflect(Point reach, Vector normal) { 
            Vector refDir = direction - 2 * normal * direction.Scalar(normal);
            return new Ray(reach,refDir);
        }


        /* T = n1/n2*I = cos(teta)+n1/n2*(N*I))*N;
         * cos(teta) = sqrt(1-(n1/n2)^2*(1-(N*I)^2))
         * T-transparency ray
         * I-incident ray
         * N-normal
         * n1,n2 - refract coefs
         */
        public Ray Refract(Point reach,Vector normal, float r1, float r2 ) {
            float r = r1 / r2;
            float scalar = normal.Scalar(direction);
            float teta = 1 - r * r * (1 - scalar * scalar);

            if (teta <0) {
                return null;
            }

            float cos = (float)Math.Sqrt(teta);
            Vector newDirection = new Vector(direction * r - (cos + r * scalar) * normal).Normalize();
            return new Ray(reach,newDirection);
        }



        public bool Intersection(Polyhedron figure, ref float res, ref float t, ref Vector normal) {
            Polygon p=null;
            foreach (var poly in figure.Polygons)
            {
                for (int i = 0; i < poly.Verts.Count-3; i+=3)
                {
                    if (RayTriangleIntersection(new Vector(poly.Verts[i]), new Vector(poly.Verts[i+1]), new Vector(poly.Verts[i+2]),ref t) && (res == 0 || t < res))
                    {
                        res = t;
                        p = poly;
                    }
                }
            }

            if (res != 0)
            {
                normal = p.GetNorm();
                figure.material.Color = new Vector(p.pen.Color.R / 255f, p.pen.Color.G / 255f, p.pen.Color.B / 255f);
                return true;
            }

            return false;

        }

        //https://en.wikipedia.org/wiki/M%C3%B6ller%E2%80%93Trumbore_intersection_algorithm
        public bool RayTriangleIntersection(Vector p0, Vector p1, Vector p2, ref float intersect)
        {
            float eps = 0.0001f;
            
            intersect = -1;
            Vector edge1 = p1 - p0;
            Vector edge2 = p2 - p0;
            Vector pVec = direction * edge2;
            float det = edge1.Scalar(pVec);

 
            if (det > -eps && det < eps)
                return false;

            float invDet = 1.0f / det;
            Vector s = new Vector(r.start) - p0;
            float u = invDet * s.Scalar(pVec);
            if (u < 0 || u > 1)
                return false;

            Vector q = s * edge1;
            float v = invDet * r.direction.Scalar(q);

            if (v < 0 || u + v > 1)
                return false;

         
            float t = invDet * edge2.Scalar(q);
            if (t > eps)
            {
                intersect = t;
                return true;
            }

            else
                return false;
        }
    }
}
