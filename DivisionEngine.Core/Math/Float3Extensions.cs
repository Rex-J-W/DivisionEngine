using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DivisionEngine.Math
{
    /// <summary>
    /// Extension methods for float3 vector operations.
    /// </summary>
    /// <remarks>This class is still untested, and therefore cannot be used in production yet</remarks>
    public static class Float3Extensions
    {
        public static float3 Normalize(this float3 v)
        {
            float length = Length(v);
            if (length == 0) return new float3(0, 0, 0);
            return new float3(v.X / length, v.Y / length, v.Z / length);
        }

        public static float3 Reflect(float3 I, float3 N)
        {
            return I - 2 * Dot(N, I) * N;
        }

        public static float3 Refract(float3 I, float3 N, float eta)
        {
            float cosi = Math.Clamp(Dot(I, N), -1, 1);
            float etai = 1, etat = eta;
            if (cosi < 0) cosi = -cosi;
            else
            {
                float temp = etai;
                etai = etat;
                etat = temp;
                N = -N;
            }
            float etaRatio = etai / etat;
            float k = 1 - etaRatio * etaRatio * (1 - cosi * cosi);
            if (k < 0) return new float3(0, 0, 0);
            else return etaRatio * I + (etaRatio * cosi - (float)Math.Sqrt(k)) * N;
        }

        public static float3 Lerp(float3 a, float3 b, float t)
        {
            return new float3(
                Math.Lerp(a.X, b.X, t),
                Math.Lerp(a.Y, b.Y, t),
                Math.Lerp(a.Z, b.Z, t)
            );
        }

        public static float3 Slerp(float3 a, float3 b, float t)
        {
            float dot = Dot(Normalize(a), Normalize(b));
            dot = Math.Clamp(dot, -1, 1);
            float theta = (float)Math.Acos(dot) * t;
            float3 relativeVec = Normalize(b - dot * a);
            return Normalize(a * (float)Math.Cos(theta) + relativeVec * (float)Math.Sin(theta));
        }

        public static float3 Cross(float3 a, float3 b)
        {
            return new float3(
                a.Y * b.Z - a.Z * b.Y,
                a.Z * b.X - a.X * b.Z,
                a.X * b.Y - a.Y * b.X
            );
        }

        public static float Dot(this float3 a, float3 b)
        {
            return a.X * b.X + a.Y * b.Y + a.Z * b.Z;
        }

        public static float Length(this float3 v)
        {
            return (float)Math.Sqrt(v.X * v.X + v.Y * v.Y + v.Z * v.Z);
        }
    }
}
