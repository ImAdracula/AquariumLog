using JessicasAquariumMonitor.Helpers.General;
using JessicasAquariumMonitor.Types;
using ChemicalTypeEntity = JessicasAquariumMonitor.Data.Entities.ChemicalType;

namespace JessicasAquariumMonitor.Web.Converters
{
    internal sealed class ChemicalTypeConverter : IConverter<ChemicalTypeEntity, ChemicalType>,
        IConverter<ChemicalType, ChemicalTypeEntity>
    {
        public ChemicalType Convert(ChemicalTypeEntity from) => new ChemicalType
        {
            Id = from.Id,
            Name = from.Name.Trim()
        };

        public ChemicalTypeEntity Convert(ChemicalType from) => new ChemicalTypeEntity
        {
            Id = from.Id,
            Name = from.Name.Trim()
        };
    }
}