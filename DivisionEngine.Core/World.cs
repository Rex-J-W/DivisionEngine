using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DivisionEngine
{
    /// <summary>
    /// Represents a simulation world that manages entities and their associated components.
    /// </summary>
    /// <remarks>The <see cref="World"/> class provides functionality to create and destroy entities, as well
    /// as manage components associated with those entities. It maintains a collection of active entities and component
    /// pools for efficient management of entity-component relationships.  Entities are uniquely identified by an
    /// integer ID, and components are stored in type-specific pools. This design allows for dynamic addition and
    /// removal of components for entities.</remarks>
    public class World
    {
        private int nextEntityId = 0;
        private readonly Dictionary<Type, object> componentPools = [];
        private readonly HashSet<int> aliveEntities = [];

        /// <summary>
        /// Creates a new instance of the <see cref="Entity"/> class with a unique identifier.
        /// </summary>
        /// <remarks>Each call to this method generates a new <see cref="Entity"/> with a unique
        /// identifier that is incremented automatically. The identifier is guaranteed to be unique within the scope of
        /// this instance.</remarks>
        /// <returns>A new <see cref="Entity"/> instance initialized with a unique identifier.</returns>
        public Entity CreateEntity()
        {
            return new Entity(nextEntityId++);
        }

        /// <summary>
        /// Removes the specified entity and its associated components from the system.
        /// </summary>
        /// <remarks>This method removes the entity from the system's internal collection of active
        /// entities.  If the entity is successfully removed, all components associated with the entity are also 
        /// removed from their respective component pools.</remarks>
        /// <param name="entity">The entity to be removed. Must not be null.</param>
        /// <returns><see langword="true"/> if the entity was successfully removed; otherwise, <see langword="false"/>.</returns>
        public bool DestroyEntity(Entity entity)
        {
            bool removed = aliveEntities.Remove(entity.ID);
            if (removed)
            {
                foreach (var pool in componentPools.Values)
                {
                    var method = pool.GetType().GetMethod("Remove");
                    method?.Invoke(pool, [entity.ID]);
                }
            }
            return removed;
        }

        /// <summary>
        /// Adds a component of the specified type to the given entity.
        /// </summary>
        /// <remarks>This method associates the specified component with the given entity in the
        /// entity-component system. The entity must exist in the world before calling this method.</remarks>
        /// <typeparam name="T">The type of the component to add. Must implement <see cref="IComponent"/>.</typeparam>
        /// <param name="entity">The entity to which the component will be added.</param>
        /// <param name="component">The component instance to add to the entity.</param>
        public void AddComponent<T>(Entity entity, T component) where T : IComponent
        {
            if (!aliveEntities.Contains(entity.ID))
                Debug.Error("Attempted to add a component to an entity that does not exist in the world.");
            var pool = GetPool<T>();
            pool.Add(entity.ID, component);
        }

        /// <summary>
        /// Attempts to retrieve a component of the specified type associated with the given entity.
        /// </summary>
        /// <remarks>This method does not throw an exception if the component is not found. Instead, it
        /// returns <see langword="false"/> and sets the <paramref name="component"/> parameter to its default
        /// value.</remarks>
        /// <typeparam name="T">The type of the component to retrieve. Must implement <see cref="IComponent"/>.</typeparam>
        /// <param name="entity">The entity whose component is to be retrieved.</param>
        /// <param name="component">When this method returns, contains the component of type <typeparamref name="T"/> if found; otherwise, the
        /// default value for the type of the component.</param>
        /// <returns><see langword="true"/> if the component of type <typeparamref name="T"/> was successfully retrieved;
        /// otherwise, <see langword="false"/>.</returns>
        public bool TryGetComponent<T>(Entity entity, out T component) where T : IComponent
        {
            return GetPool<T>().TryGet(entity.ID, out component);
        }

        private ComponentPool<T> GetPool<T>() where T : IComponent
        {
            if (!componentPools.TryGetValue(typeof(T), out var pool))
            {
                pool = new ComponentPool<T>();
                componentPools[typeof(T)] = pool;

                // Temporary info log for pool creation
                Debug.Info($"Created new component pool for type {typeof(T).Name}.");
            }
            return (ComponentPool<T>)pool;
        }

    }
}
