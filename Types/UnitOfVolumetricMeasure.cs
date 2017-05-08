namespace JessicasAquariumMonitor.Types
{
    public sealed class UnitOfVolumetricMeasure : IUnitOfMeasure
    {
        public decimal VolumeInCubicCentimeters { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public UnitOfMeasureType UnitOfMeasureType => UnitOfMeasureType.Volume;
    }
}