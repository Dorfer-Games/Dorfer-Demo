using System;
using UnityEngine;

namespace UnityTools.Extentions
{
    public static class MathExtensions
    {
        public static void DrawBounds(Bounds b, Color color, float delay)
        {
            var p1 = new Vector3(b.min.x, b.min.y, b.min.z);
            var p2 = new Vector3(b.max.x, b.min.y, b.min.z);
            var p3 = new Vector3(b.max.x, b.min.y, b.max.z);
            var p4 = new Vector3(b.min.x, b.min.y, b.max.z);

            Debug.DrawLine(p1, p2, color, delay);
            Debug.DrawLine(p2, p3, color, delay);
            Debug.DrawLine(p3, p4, color, delay);
            Debug.DrawLine(p4, p1, color, delay);

            var p5 = new Vector3(b.min.x, b.max.y, b.min.z);
            var p6 = new Vector3(b.max.x, b.max.y, b.min.z);
            var p7 = new Vector3(b.max.x, b.max.y, b.max.z);
            var p8 = new Vector3(b.min.x, b.max.y, b.max.z);

            Debug.DrawLine(p5, p6, color, delay);
            Debug.DrawLine(p6, p7, color, delay);
            Debug.DrawLine(p7, p8, color, delay);
            Debug.DrawLine(p8, p5, color, delay);

            Debug.DrawLine(p1, p5, color, delay);
            Debug.DrawLine(p2, p6, color, delay);
            Debug.DrawLine(p3, p7, color, delay);
            Debug.DrawLine(p4, p8, color, delay);
        }

        public static decimal ClampDecimal(decimal value, decimal min, decimal max)
        {
            var result = value;
            result = Math.Max(result, min);
            result = Math.Min(result, max);
            return result;
        }

        public static float PingPong(this float value, float length)
        {
            return Mathf.PingPong(value, length);
        }

        public static float Repeat(this float value, float length)
        {
            return Mathf.Repeat(value, length);
        }

        public static float Remap(this float value, float from1, float to1, float from2, float to2)
        {
            return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
        }

        public static float Remap(this int value, float from1, float to1, float from2, float to2)
        {
            return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
        }

        public static float Clamp(this float value, float min, float max)
        {
            return Mathf.Clamp(value, min, max);
        }

        public static float InverseLerp(Vector3 a, Vector3 b, Vector3 value)
        {
            var AB = b - a;
            var AV = value - a;
            return Vector3.Dot(AV, AB) / Vector3.Dot(AB, AB);
        }
    }
}