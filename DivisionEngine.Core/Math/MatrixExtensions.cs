using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DivisionEngine.Math
{
    /// <summary>
    /// Extension methods for matrix operations.
    /// </summary>
    public static class MatrixExtensions
    {
        /// <summary>
        /// Computes the inverse of the specified 4x4 matrix.
        /// </summary>
        /// <remarks>The method calculates the inverse of the given matrix using a mathematical approach.
        /// If the determinant of the matrix is zero, indicating that the matrix is singular, the method returns the
        /// identity matrix instead.</remarks>
        /// <param name="matrix">The <see cref="float4x4"/> matrix to invert.</param>
        /// <returns>A new <see cref="float4x4"/> representing the inverse of the input matrix. If the matrix is singular
        /// (non-invertible), the returned matrix will be the identity matrix.</returns>
        /// https://stackoverflow.com/questions/1148309/inverting-a-4x4-matrix
        public static float4x4 Invert(this float4x4 matrix)
        {
            float[] m = [
                matrix.M11, matrix.M12, matrix.M13, matrix.M14,
                matrix.M21, matrix.M22, matrix.M23, matrix.M24,
                matrix.M31, matrix.M32, matrix.M33, matrix.M34,
                matrix.M41, matrix.M42, matrix.M43, matrix.M44
            ];
            double[] inv = [16];
            double det;
            int i;

            inv[0] = m[5] * m[10] * m[15] -
                        m[5] * m[11] * m[14] -
                        m[9] * m[6] * m[15] +
                        m[9] * m[7] * m[14] +
                        m[13] * m[6] * m[11] -
                        m[13] * m[7] * m[10];

            inv[4] = -m[4] * m[10] * m[15] +
                        m[4] * m[11] * m[14] +
                        m[8] * m[6] * m[15] -
                        m[8] * m[7] * m[14] -
                        m[12] * m[6] * m[11] +
                        m[12] * m[7] * m[10];

            inv[8] = m[4] * m[9] * m[15] -
                        m[4] * m[11] * m[13] -
                        m[8] * m[5] * m[15] +
                        m[8] * m[7] * m[13] +
                        m[12] * m[5] * m[11] -
                        m[12] * m[7] * m[9];

            inv[12] = -m[4] * m[9] * m[14] +
                        m[4] * m[10] * m[13] +
                        m[8] * m[5] * m[14] -
                        m[8] * m[6] * m[13] -
                        m[12] * m[5] * m[10] +
                        m[12] * m[6] * m[9];

            inv[1] = -m[1] * m[10] * m[15] +
                        m[1] * m[11] * m[14] +
                        m[9] * m[2] * m[15] -
                        m[9] * m[3] * m[14] -
                        m[13] * m[2] * m[11] +
                        m[13] * m[3] * m[10];

            inv[5] = m[0] * m[10] * m[15] -
                        m[0] * m[11] * m[14] -
                        m[8] * m[2] * m[15] +
                        m[8] * m[3] * m[14] +
                        m[12] * m[2] * m[11] -
                        m[12] * m[3] * m[10];

            inv[9] = -m[0] * m[9] * m[15] +
                        m[0] * m[11] * m[13] +
                        m[8] * m[1] * m[15] -
                        m[8] * m[3] * m[13] -
                        m[12] * m[1] * m[11] +
                        m[12] * m[3] * m[9];

            inv[13] = m[0] * m[9] * m[14] -
                        m[0] * m[10] * m[13] -
                        m[8] * m[1] * m[14] +
                        m[8] * m[2] * m[13] +
                        m[12] * m[1] * m[10] -
                        m[12] * m[2] * m[9];

            inv[2] = m[1] * m[6] * m[15] -
                        m[1] * m[7] * m[14] -
                        m[5] * m[2] * m[15] +
                        m[5] * m[3] * m[14] +
                        m[13] * m[2] * m[7] -
                        m[13] * m[3] * m[6];

            inv[6] = -m[0] * m[6] * m[15] +
                        m[0] * m[7] * m[14] +
                        m[4] * m[2] * m[15] -
                        m[4] * m[3] * m[14] -
                        m[12] * m[2] * m[7] +
                        m[12] * m[3] * m[6];

            inv[10] = m[0] * m[5] * m[15] -
                        m[0] * m[7] * m[13] -
                        m[4] * m[1] * m[15] +
                        m[4] * m[3] * m[13] +
                        m[12] * m[1] * m[7] -
                        m[12] * m[3] * m[5];

            inv[14] = -m[0] * m[5] * m[14] +
                        m[0] * m[6] * m[13] +
                        m[4] * m[1] * m[14] -
                        m[4] * m[2] * m[13] -
                        m[12] * m[1] * m[6] +
                        m[12] * m[2] * m[5];

            inv[3] = -m[1] * m[6] * m[11] +
                        m[1] * m[7] * m[10] +
                        m[5] * m[2] * m[11] -
                        m[5] * m[3] * m[10] -
                        m[9] * m[2] * m[7] +
                        m[9] * m[3] * m[6];

            inv[7] = m[0] * m[6] * m[11] -
                        m[0] * m[7] * m[10] -
                        m[4] * m[2] * m[11] +
                        m[4] * m[3] * m[10] +
                        m[8] * m[2] * m[7] -
                        m[8] * m[3] * m[6];

            inv[11] = -m[0] * m[5] * m[11] +
                        m[0] * m[7] * m[9] +
                        m[4] * m[1] * m[11] -
                        m[4] * m[3] * m[9] -
                        m[8] * m[1] * m[7] +
                        m[8] * m[3] * m[5];

            inv[15] = m[0] * m[5] * m[10] -
                        m[0] * m[6] * m[9] -
                        m[4] * m[1] * m[10] +
                        m[4] * m[2] * m[9] +
                        m[8] * m[1] * m[6] -
                        m[8] * m[2] * m[5];

            det = m[0] * inv[0] + m[1] * inv[4] + m[2] * inv[8] + m[3] * inv[12];

            if (det == 0)
                return Matrix.Zero4x4;

            det = 1.0 / det;

            float[] invOut = [16];
            for (i = 0; i < 16; i++)
                invOut[i] = (float)(inv[i] * det);

            return new float4x4(invOut[0], invOut[1], invOut[2], invOut[3],
                                  invOut[4], invOut[5], invOut[6], invOut[7],
                                  invOut[8], invOut[9], invOut[10], invOut[11],
                                  invOut[12], invOut[13], invOut[14], invOut[15]);
        }
    }
}
