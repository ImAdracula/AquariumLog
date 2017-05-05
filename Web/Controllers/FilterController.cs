using System;
using System.Collections.Generic;
using System.Web.Http;

namespace JessicasAquariumMonitor.Web.Controllers
{
    [RoutePrefix(@"Filter")]
    public sealed class FilterController : ApiController
    {
        [HttpGet, Route(@"{id}")]
        public Filter Get(int id)
        {
            throw new NotImplementedException();
        }

        [HttpGet, Route(@"All")]
        public IEnumerable<Filter> Get()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public void Add(Filter filter)
        {
            throw new NotImplementedException();
        }

        [HttpPut, Route(@"{id}")]
        public void Update(int id, Filter filter)
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