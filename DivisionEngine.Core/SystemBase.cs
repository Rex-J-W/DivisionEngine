namespace DivisionEngine
{
    /// <summary>
    /// Represents a system that processes entities with specific components in the ECS architecture.
    /// </summary>
    /// <param name="world">The world this system operates in</param>
    public abstract class SystemBase(World world)
    {
        protected World World { get; private set; } = world;

        /// <summary>
        /// Called every frame to update the system's logic.
        /// </summary>
        public abstract void Update();
    }
}
