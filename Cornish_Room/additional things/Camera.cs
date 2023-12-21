using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba7
{
    internal class Camera
    {
        public Point cameraPosition;
        public Vector cameraDirection;
        public Vector cameraUp;
        public Vector cameraRight;
        Transformations transformations;
        const float cameraRotationSpeed = 1;
        float yaw = 0.0f, pitch = 0.0f;

        public Camera()
        {
            cameraPosition = new Point(-10, 0, 0);
            cameraDirection = new Vector(1, 0, 0);
            cameraUp = new Vector(0, 0, 1);
            cameraRight = (cameraDirection * cameraUp).Normalize();
            transformations = new Transformations();
        }

        public void Move(float leftright = 0, float forwardbackward = 0, float updown = 0)
        {
            cameraPosition.XF += leftright * cameraRight.XF + forwardbackward * cameraDirection.XF + updown * cameraUp.XF;
            cameraPosition.YF += leftright * cameraRight.YF + forwardbackward * cameraDirection.YF + updown * cameraUp.YF;
            cameraPosition.ZF += leftright * cameraRight.ZF + forwardbackward * cameraDirection.ZF + updown * cameraUp.ZF;
        }

        public Point ToCameraView(Point p)
        {
            return new Point(
                cameraRight.XF * (p.XF - cameraPosition.XF) + cameraRight.YF * (p.YF - cameraPosition.YF) +
                cameraRight.ZF * (p.ZF - cameraPosition.ZF),
                cameraUp.XF * (p.XF - cameraPosition.XF) + cameraUp.YF * (p.YF - cameraPosition.YF) +
                cameraUp.ZF * (p.ZF - cameraPosition.ZF),
                cameraDirection.XF * (p.XF - cameraPosition.XF) + cameraDirection.YF * (p.YF - cameraPosition.YF) +
                cameraDirection.ZF * (p.ZF - cameraPosition.ZF));
        }
        public void ChangeView(float shiftX = 0, float shiftY = 0)
        {
            var newPitch = (float)Math.Clamp(pitch + shiftY * cameraRotationSpeed, -89.0, 89.0);
            var newYaw = (yaw + shiftX) % 360;
            if (newPitch != pitch)
            {
                transformations.RotateVectors(ref cameraDirection, ref cameraUp, (newPitch - pitch), cameraRight);
                pitch = newPitch;
            }

            if (newYaw != yaw)
            {
                transformations.RotateVectors(ref cameraDirection, ref cameraRight, (newYaw - yaw), cameraUp);
                yaw = newYaw;
            }
        }

    }
}
