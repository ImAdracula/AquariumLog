using System;
using System.Collections.Generic;
using System.Linq;
using JessicasAquariumMonitor.Data.Base;
using JessicasAquariumMonitor.Data.Entities;

namespace JessicasAquariumMonitor.Data.Repositories
{
    public interface ILogEntryRepository : IBulkGetRepository<LogEntry, int>, IBulkAddRepository<LogEntry, int>,
        IBulkDeleteRepository<LogEntry, int>
    {
        IEnumerable<LogEntry> GetEntriesByData(DateTime start, DateTime end);
    }

    internal sealed class LogEntryRepository : AbstractRepository<AquariumContext, int, LogEntry>, ILogEntryRepository
    {
        public LogEntryRepository(AquariumContext context) : base(context)
        {
        }

        public override LogEntry Get(int key)
        {
            var entry = DbSet.SingleOrDefault(logEntry => logEntry.Id == key);

            if (entry == null)
            {
                throw new KeyNotFoundException(@"Log entry not found with the given Id");
            }

            return entry;
        }

        public IEnumerable<LogEntry> GetEntriesByData(DateTime start, DateTime end)
            => DbSet.Where(entry => entry.TimeStamp >= start && entry.TimeStamp <= end);
    }
}