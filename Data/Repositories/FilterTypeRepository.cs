using System.Collections.Generic;
using System.Linq;
using Data.Base;
using Data.Entities;

namespace Data.Repositories
{
    public interface IFilterTypeRepository : IBulkGetRepository<FilterType, int>, IBulkAddRepository<FilterType, int>,
        IBulkDeleteRepository<FilterType, int>
    {
    }

    internal sealed class FilterTypeRepository : AbstractRepository<AquariumContext, int, FilterType>
    {
        public FilterTypeRepository(AquariumContext context) : base(context)
        {
        }

        public override FilterType Get(int key)
        {
            var entry = DbSet.SingleOrDefault(filterType => filterType.Id == key);

            if (entry == null)
            {
                throw new KeyNotFoundException(@"Filter type not found with the given Id");
            }

            return entry;
        }
    }
}