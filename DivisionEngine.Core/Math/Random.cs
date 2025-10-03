namespace DivisionEngine.Math
{
    /// <summary>
    /// Represents a pseduo random number generator that holds state.
    /// </summary>
    /// <remarks>This class is a placeholder and has not been tested yet</remarks>
    public static class Random
    {
        private static System.Random _rng = new System.Random();

        /// <summary>
        /// Updates the seed of the random number generator.
        /// </summary>
        /// <param name="seed">New seed to use</param>
        public static void Seed(int seed)
        {
            _rng = new System.Random(seed);
        }

        /// <summary>
        /// Gets a random float between 0.0 and 1.0
        /// </summary>
        /// <returns>Next random float for current rng instance</returns>
        public static float NextFloat() => (float)_rng.NextDouble();

        /// <summary>
        /// Gets a random non-negative float from min to max.
        /// </summary>
        /// <param name="min">Minimum value</param>
        /// <param name="max">Maximum value</param>
        /// <returns>Value between min to max</returns>
        public static float NextFloat(float min, float max) => (float)(_rng.NextDouble() * (max - min) + min);

        /// <summary>
        /// Gets a random non-negative integer from min to max-1.
        /// </summary>
        /// <param name="min">Minimum value</param>
        /// <param name="max">Maximum value</param>
        /// <returns>Value between min to max-1</returns>
        public static int NextInt(int min, int max) => _rng.Next(min, max);

        /// <summary>
        /// Gets a random non-negative integer from 0 to max-1.
        /// </summary>
        /// <param name="max">Maximum value</param>
        /// <returns>Value between 0 to max-1</returns>
        public static int NextInt(int max) => _rng.Next(max);

        /// <summary>
        /// Gets a random non-negative integer
        /// </summary>
        /// <returns>Next random integer for current rng instance</returns>
        public static int NextInt() => _rng.Next();

        /// <summary>
        /// Gets a random boolean value
        /// </summary>
        /// <returns>Next random boolean for current rng instance</returns>
        /// <remarks>50% chance of true or false, uses "NextInt(0, 2) == 0" internally</remarks>
        public static bool NextBool() => _rng.Next(0, 2) == 0;
    }
}
