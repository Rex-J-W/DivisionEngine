namespace DivisionEngine
{
    /// <summary>
    /// Represents an immutable entity with a unique identifier.
    /// </summary>
    /// <remarks>The <see cref="Entity"/> struct is used to encapsulate an entity's unique identifier.
    /// Instances of this struct are immutable and can be used to represent entities in various contexts.</remarks>
    public readonly struct Entity
    {
        /// <summary>
        /// The unique ID of the entity.
        /// </summary>
        public int ID { get; }
        internal Entity(int id) => ID = id;
    }
}
