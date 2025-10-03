namespace DivisionEngine.Math
{
    /// <summary>
    /// Handles common matrix operations and provides predefined matrices.
    /// </summary>
    public static class Matrix
    {
        /// <summary>
        /// Represents a 4x4 matrix with all elements set to zero.
        /// </summary>
        public static float4x4 Zero4x4 => new float4x4(
            0, 0, 0, 0,
            0, 0, 0, 0,
            0, 0, 0, 0,
            0, 0, 0, 0
        );

        /// <summary>
        /// Represents a 4x4 identity matrix.
        /// </summary>
        public static float4x4 Identity4x4 => new float4x4(
            1, 0, 0, 0,
            0, 1, 0, 0,
            0, 0, 1, 0,
            0, 0, 0, 1
        );
    }
}
