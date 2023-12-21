using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba7
{
    internal class TexturePoint
    {
        float u, v;

        public TexturePoint(float u, float v)
        {
            this.u = u;
            this.v = v;
        }

        public float U => u;

        public float V => v;

        public static TexturePoint operator +(TexturePoint tp1, TexturePoint tp2)
        {
            return new TexturePoint(tp1.u + tp2.u, tp1.v + tp2.v);
        }

        public static TexturePoint operator -(TexturePoint tp1, TexturePoint tp2)
        {
            return new TexturePoint(tp1.u - tp2.u, tp1.v - tp2.v);
        }

        public static TexturePoint operator /(TexturePoint tp1, float d)
        {
            return new TexturePoint(tp1.u / d, tp1.v / d);
        }
    }
}
