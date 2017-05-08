using System;

namespace JessicasAquariumMonitor.Types
{
    public sealed class LogEntry
    {
        public int Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public byte? PotentialOfHydrogen { get; set; }
        public AdministeredChemical[] AdministeredChemicals { get; set; }
        public ReplacedFilter[] ReplacedFilters { get; set; }
    }
}