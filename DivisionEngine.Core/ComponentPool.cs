namespace DivisionEngine
{
    /// <summary>
    /// Represents a pool of components that can be associated with entities, identified by unique entity IDs.
    /// </summary>
    /// <remarks>This class provides functionality to add, remove, and retrieve components associated with
    /// specific entity IDs. It is designed to manage components efficiently in scenarios where entities are dynamically
    /// created and updated.</remarks>
    /// <typeparam name="T">The type of components managed by the pool. Must implement the <see cref="IComponent"/> interface.</typeparam>
    internal class ComponentPool<T> where T : IComponent
    {
        private readonly Dictionary<int, T> components = [];

        public void Add(int entityId, T component) => components[entityId] = component;
        public bool Remove(int entityId) => components.Remove(entityId);
        public bool TryGet(int entityId, out T component) => components.TryGetValue(entityId, out component!);
        public IEnumerable<KeyValuePair<int, T>> All => components;
    }
}
