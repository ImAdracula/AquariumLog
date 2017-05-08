namespace JessicasAquariumMonitor.Types
{
    public sealed class UnitOfMassMeasure : IUnitOfMeasure
    {
        public decimal VolumeInGrams { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public UnitOfMeasureType UnitOfMeasureType => UnitOfMeasureType.Mass;
    }
}