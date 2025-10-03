using static DivisionEngine.Debug;

namespace DivisionEngine
{
    /// <summary>
    /// Manages the timing and frame rate of the engine.
    /// </summary>
    public class Time
    {
        /// <summary>
        /// Instance of the Time class, used for singleton access.
        /// </summary>
        public static Time? Instance { get; private set; }

        public Time()
        {
            Log("Time system initialized.", LogLevel.Info);
        }
    }
}
