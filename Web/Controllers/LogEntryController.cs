using System;
using System.Collections.Generic;
using System.Web.Http;
using JessicasAquariumMonitor.Types;

namespace JessicasAquariumMonitor.Web.Controllers
{
    [RoutePrefix(@"Log")]
    public sealed class LogEntryController : ApiController
    {
        [HttpGet, Route(@"{id}")]
        public LogEntry Get(int id)
        {
            throw new NotImplementedException();
        }

        [HttpGet, Route(@"Query")]
        public IEnumerable<LogEntry> Get(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public void Add(LogEntry logEntry)
        {
            throw new NotImplementedException();
        }

        [HttpPut, Route(@"{id}")]
        public void Update(int id, LogEntry logEntry)
        {
            throw new NotImplementedException();
        }

        [HttpDelete, Route(@"{id}")]
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}