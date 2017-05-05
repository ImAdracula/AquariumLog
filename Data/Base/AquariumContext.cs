using System.Data.Entity;
using JessicasAquariumMonitor.Data.Entities;

namespace JessicasAquariumMonitor.Data.Base
{
    internal sealed class AquariumContext : DbContext
    {
        public AquariumContext(string connectionString) : base(connectionString)
        {
        }

        public DbSet<LogEntry> LogEntries { get; set; }
        public DbSet<FilterType> FilterTypes { get; set; }
        public DbSet<ChemicalType> ChemicalTypes { get; set; }
    }
}