namespace DivisionEngine
{
    public readonly struct Entity
    {
        public int ID { get; }
        internal Entity(int id) => ID = id;
    }
}
