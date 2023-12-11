using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba7
{
    public enum Visibilty
    {
        INVISIBLE,
        VISIBLE_UP,
        VISIBLE_DOWN
    };
    internal class FloatingPoint : Point 
    {
        public Visibilty Visibility { get; }

        public FloatingPoint(float x, float y, float z, Visibilty visibility) : base(x, y, z)
        {
            Visibility = visibility;
        }

        public FloatingPoint(float x, float y, float z, ref float[] upHorizon, ref float[] downHorizon) :
            base(x, y, z)
        {
            if (y >= upHorizon[(int)x])
            {
                Visibility = Visibilty.VISIBLE_UP;
                return;
            }

            if (y <= downHorizon[(int)x])
            {
                Visibility = Visibilty.VISIBLE_DOWN;
                return;
            }

            Visibility = Visibilty.INVISIBLE;
        }
    }
}
