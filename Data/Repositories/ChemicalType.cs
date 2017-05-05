using System.Collections.Generic;
using System.Linq;
using JessicasAquariumMonitor.Data.Base;
using JessicasAquariumMonitor.Data.Entities;

namespace JessicasAquariumMonitor.Data.Repositories
{
    public interface IChemicalTypeRepository : IBulkGetRepository<ChemicalType, int>,
        IBulkAddRepository<ChemicalType, int>,
        IBulkDeleteRepository<ChemicalType, int>
    {
    }

    internal sealed class ChemicalTypeRepository : AbstractRepository<AquariumContext, int, ChemicalType>
    {
        public ChemicalTypeRepository(AquariumContext context) : base(context)
        {
        }

        public override ChemicalType Get(int key)
        {
            var entry = DbSet.SingleOrDefault(chemicalType => chemicalType.Id == key);

            if (entry == null)
            {
                throw new KeyNotFoundException(@"Chemical type not found with the given Id");
            }

            return entry;
        }
    }
}