namespace DivisionEngine
{
    /// <summary>
    /// A query to retrieve entities that possess specific components within a given world.
    /// </summary>
    /// <param name="world">The world to query entities from</param>
    /// <param name="componentTypes">The component types to query</param>
    public class Query(World world, params Type[] componentTypes)
    {
        private readonly World world = world;
        private readonly Type[] componentTypes = componentTypes;

        // Currently unfinished
        // Must first expand HasComponent to accept typeof values

        /*public IEnumerable<int> Entities()
        {
            IEnumerable<int> entities = world.GetAliveEntities();
            foreach (var entityId in entities)
            {
                bool hasAllComponents = true;
                foreach (var type in componentTypes)
                {
                    if (!world.HasComponent(entityId, type))
                    {
                        hasAllComponents = false;
                        break;
                    }
                }
                if (hasAllComponents)
                {
                    yield return entityId;
                }
            }
        }*/
    }
}
