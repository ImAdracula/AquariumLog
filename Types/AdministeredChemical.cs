namespace JessicasAquariumMonitor.Types
{
    public sealed class AdministeredChemical
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public ChemicalType ChemicalType { get; set; }
        public UnitOfMeasure UnitOfMeasure { get; set; }
    }
}