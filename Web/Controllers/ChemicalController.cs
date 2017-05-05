using System;
using System.Collections.Generic;
using System.Web.Http;

namespace JessicasAquariumMonitor.Web.Controllers
{
    [RoutePrefix(@"Chemical")]
    public sealed class ChemicalController : ApiController
    {
        [HttpGet, Route(@"{id}")]
        public Chemical Get(int id)
        {
            throw new NotImplementedException();
        }

        [HttpGet, Route(@"All")]
        public IEnumerable<Chemical> Get()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public void Add(Chemical chemical)
        {
            throw new NotImplementedException();
        }

        [HttpPut, Route(@"{id}")]
        public void Update(int id, Chemical chemical)
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