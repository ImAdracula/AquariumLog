using System;
using System.Collections.Generic;
using System.Web.Http;
using JessicasAquariumMonitor.Types;

namespace JessicasAquariumMonitor.Web.Controllers
{
    [RoutePrefix(@"Chemical")]
    public sealed class ChemicalTypeController : ApiController
    {
        [HttpGet, Route(@"{id}")]
        public ChemicalType Get(int id)
        {
            throw new NotImplementedException();
        }

        [HttpGet, Route(@"All")]
        public IEnumerable<ChemicalType> Get()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public void Add(ChemicalType chemicalType)
        {
            throw new NotImplementedException();
        }

        [HttpPut, Route(@"{id}")]
        public void Update(int id, ChemicalType chemicalType)
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