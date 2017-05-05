using System.Data.Entity;
using Data.Entities;

namespace Data.Base
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