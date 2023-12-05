using HonkSharp.Fluency;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba8
{
    internal class Camera
    {
        public Point position;
        public Vector direction;
        public Vector up;
        public Vector right;

        Transformations transformations;

        float rotationSpeed = 1f;
        float pitch = 0;
        float yaw = 0;
        public Camera() {
            position = new Point(-10, 0, 0);
            direction = new Vector(1, 0, 0);
            up = new Vector(0, 0, 1);
            right = (direction * up).Normalize();
            transformations=new Transformations();
        }

        public void Reset() {
            position = new Point(-10, 0, 0);
            direction = new Vector(1, 0, 0);
            up = new Vector(0, 0, 1);
            right = (direction * up).Normalize();
        }

        public void Move(float leftright = 0, float forwardbackward = 0, float updown = 0)
        {
            position.XF += leftright * right.XF + forwardbackward * direction.XF + updown * up.XF;
            position.YF += leftright * right.YF + forwardbackward * direction.YF + updown * up.YF;
            position.ZF += leftright * right.ZF + forwardbackward * direction.ZF + updown * up.ZF;

        }

        public Point ToCameraView(Point p)
        {
            return new Point(
             right.XF* (p.XF - position.XF) + right.YF * (p.YF - position.YF) +
             right.ZF * (p.ZF - position.ZF),
             up.XF * (p.XF - position.XF) + up.YF * (p.YF - position.YF) +
             up.ZF * (p.ZF - position.ZF),
             direction.XF * (p.XF - position.XF) + direction.YF * (p.YF - position.YF) +
             direction.ZF * (p.ZF - position.ZF));
        }

        public void ChangeView(float shiftX = 0, float shiftY = 0)
        {
            var newPitch = (float) Math.Clamp(pitch + shiftY * rotationSpeed, -89.0, 89.0);
            var newYaw = (yaw + shiftX) % 360;
            if (newPitch != pitch)
            {
 
                transformations.RotateVectors(ref direction, ref up, (newPitch - pitch), right);
                pitch = newPitch;

           
            }
            if (newYaw != yaw)
            {
                transformations.RotateVectors(ref direction, ref right, (newYaw - yaw), up);
                yaw = newYaw;
   
            }

        }


    }
}
