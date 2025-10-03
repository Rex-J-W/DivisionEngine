using DivisionEngine.Math;

namespace DivisionEngine.Components
{
    /// <summary>
    /// Represents a camera in a 3D space, including its position, rotation, and field of view.
    /// </summary>
    /// <remarks>Currently untested, needs to be integrated with rendering system.</remarks>
    public class Camera(float fov) : IComponent
    {
        public float fov = fov;

        public static float FovToScreenDistance(float fov, float height)
        {
            return height / 2 / MathF.Tan(fov * MathF.PI / 360);
        }

        // Complete setting these methods up later

        /*public float4x4 GetCameraToWorldMatrix()
        {
            float4x4 translationMatrix = float4x4.CreateTranslation(position);
            float4x4 rotationMatrix = float4x4.CreateFromQuaternion(rotation);
            return rotationMatrix * translationMatrix;
        }*/

        public float4x4 GetCameraInverseProjectionMatrix(float aspectRatio)
        {
            float fovRad = fov * (MathF.PI / 180f);
            float f = 1.0f / MathF.Tan(fovRad / 2.0f);
            float near = 0.1f;
            float far = 1000f;
            float4x4 projectionMatrix = new float4x4
            {
                M11 = f / aspectRatio,
                M22 = f,
                M33 = (far + near) / (near - far),
                M34 = (2 * far * near) / (near - far),
                M43 = -1,
                M44 = 0
            };
            return projectionMatrix.Inverse();
        }
    }
}
