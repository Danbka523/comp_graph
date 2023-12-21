using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba7
{
    internal class Material
    {
        float ambient;
        float diffuse;
        float reflection;
        float refraction;
        float environment;
        Vector color;

        public float Ambient { get => ambient; set => ambient = value; }
        public float Diffuse { get => diffuse; set => diffuse = value; }
        public float Reflection { get => reflection; set => reflection = value; }
        public float Refraction { get => refraction; set => refraction = value; }
        public float Environment { get => environment; set => environment = value; }
        public Vector Color { get => color; set => color = value; }

        public Material(float ambient, float diffuse, float reflection, float refraction, float environment)
        {
            this.ambient = ambient;
            this.diffuse = diffuse;
            this.reflection = reflection;
            this.refraction = refraction;
            this.environment = environment;

        }

        public Material(Material m)
        {
            ambient = m.ambient;
            diffuse = m.diffuse;
            reflection = m.reflection;
            refraction = m.refraction;
            environment = m.environment;
            color = m.color;
        }

        public Material() { }


    }
}
