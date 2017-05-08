namespace JessicasAquariumMonitor.Types
{
    public sealed class UnitOfItemMeasure : IUnitOfMeasure
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public UnitOfMeasureType UnitOfMeasureType => UnitOfMeasureType.Item;
    }
}