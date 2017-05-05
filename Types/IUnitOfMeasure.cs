namespace JessicasAquariumMonitor.Types
{
    public interface IUnitOfMeasure
    {
        int Id { get; set; }
        string Name { get; set; }
        UnitOfMeasureType UnitOfMeasureType { get; }
    }
}