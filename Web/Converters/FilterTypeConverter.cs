using JessicasAquariumMonitor.Helpers.General;
using JessicasAquariumMonitor.Types;
using FilterTypeEntity = JessicasAquariumMonitor.Data.Entities.FilterType;

namespace JessicasAquariumMonitor.Web.Converters
{
    internal sealed class FilterTypeConverter : IConverter<FilterTypeEntity, FilterType>,
        IConverter<FilterType, FilterTypeEntity>
    {
        public FilterType Convert(FilterTypeEntity from) => new FilterType
        {
            Id = from.Id,
            Name = from.Name
        };

        public FilterTypeEntity Convert(FilterType from) => new FilterTypeEntity
        {
            Id = from.Id,
            Name = from.Name
        };
    }
}