using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeScriptWrapper;

namespace RocketLeague
{
    public static class Utils
    {
        const int URotation180 = 32768;

        public static float URotationToRadians(float rotation)
        {
            return (float)(Math.PI / rotation /*URotation180*/);
        }

        public static Vector3 GetCarFacingVector(IntPtr carPointer)
        {
            Vector3 CarRotation = Memory.ReadVector3(Program.processHandle, carPointer + 0x009C);

            float pitch = URotationToRadians(CarRotation.X);
            float yaw = URotationToRadians(CarRotation.Y);

            float facing_x = (float)(Math.Cos(pitch) * Math.Cos(yaw));
            float facing_y = (float)(Math.Cos(pitch) * Math.Sin(yaw));

            return new Vector3(facing_x, facing_y, 0);
        }
        //this is supposed to move my car left or right towards the ball xD
        public static float SteerValueTo(Vector3 carDirection, Vector3 target)
        {
            float current_in_radians = (float)Math.Atan2(carDirection.Y, -carDirection.X);
            float target_in_radians = (float)Math.Atan2(target.Y, -target.X);

            float correction = target_in_radians - current_in_radians;

            //GameEngine->GetRadiansBetweenVectors(carDirection, playerCarToEnemyCar);

            if (Math.Abs(correction) > Math.PI)
            {

                if (correction < 0)
                    correction += (float)(2.0f * Math.PI);
                else
                    correction -= (float)(2.0f * Math.PI);
            }

            return correction;
            

        }
    }
}
