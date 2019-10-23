using UnityEngine;

namespace KanetoTools
{
    public static class Arithmetic {
        public static float RadianToAngle(float radian) {
            return 180 * radian / Mathf.PI;
        }
        public static float AngleToRadian(float Angle) {
            return Mathf.PI * Angle / 180;
        }
    }
}