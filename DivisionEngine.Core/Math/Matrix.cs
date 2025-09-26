using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DivisionEngine.Math
{
    /// <summary>
    /// Handles common matrix operations and provides predefined matrices.
    /// </summary>
    public static class Matrix
    {
        public static float4x4 Zero4x4 => new float4x4(
            0, 0, 0, 0,
            0, 0, 0, 0,
            0, 0, 0, 0,
            0, 0, 0, 0
        );

        public static float4x4 Identity4x4 => new float4x4(
            1, 0, 0, 0,
            0, 1, 0, 0,
            0, 0, 1, 0,
            0, 0, 0, 1
        );
    }
}
