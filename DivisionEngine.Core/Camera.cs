using System;

namespace DivisionEngine
{
    /// <summary>
    /// Represents a camera in a 3D space, including its position, rotation, and field of view.
    /// </summary>
    public class Camera
    {
        public float fov;
        public float3 position;
        public float4 rotation; // quaternion for rotation

        public Camera(float x, float y, float z)
        {
            position = new float3(x, y, z);
            rotation = new float4(0, 0, 0, 1); // Default rotation (no rotation)
        }

        public float FovToScreenDistance(float fov, float height)
        {
            return (height / 2) / MathF.Tan(fov * MathF.PI / 360);
        }

    }
}
