using System;
using System.Collections.Generic;
using System.Web.Http;
using JessicasAquariumMonitor.Types;

namespace JessicasAquariumMonitor.Web.Controllers
{
    [RoutePrefix(@"FilterType")]
    public sealed class FilterTypeController : ApiController
    {
        [HttpGet, Route(@"{id}")]
        public FilterType Get(int id)
        {
            throw new NotImplementedException();
        }

        [HttpGet, Route(@"All")]
        public IEnumerable<FilterType> Get()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public void Add(FilterType FilterType)
        {
            throw new NotImplementedException();
        }

        [HttpPut, Route(@"{id}")]
        public void Update(int id, FilterType FilterType)
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