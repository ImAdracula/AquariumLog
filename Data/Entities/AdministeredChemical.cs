using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Data.Base;

namespace Data.Entities
{
    [Table("AdministeredChemicals", Schema = "Aquarium")]
    public class AdministeredChemical
    {
        private const string LogEntryAndChemicalTypeUniqueIndexName = "IX_AdministeredChemicals_LogEntryAndChemicalType";

        [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.None), Column("Id")]
        public int Id { get; set; }

        [Required, Column("LogEntryId"), ForeignKey(nameof(LogEntry)),
         Index(LogEntryAndChemicalTypeUniqueIndexName, 1, IsUnique = true)]
        public int LogEntryId { get; set; }

        [Required, Column("ChemicalTypeId"), ForeignKey(nameof(ChemicalType)),
         Index(LogEntryAndChemicalTypeUniqueIndexName, 2, IsUnique = true)]
        public int ChemicalTypeId { get; set; }

        [Column("UnitOfMassMeasureId"), ForeignKey(nameof(UnitOfMassMeasure))]
        public int? UnitOfMassMeasureId { get; set; }

        [Column("UnitOfVolumetricMeasureId"), ForeignKey(nameof(UnitOfVolumetricMeasure))]
        public int? UnitOfVolumetricMeasureId { get; set; }

        [Column("UnitOfItemMeasureId"), ForeignKey(nameof(UnitOfItemMeasure))]
        public int? UnitOfItemMeasureId { get; set; }

        [Required, Column("Amount"), Decimal(9, 3)]
        public decimal Amount { get; set; }

        public virtual LogEntry LogEntry { get; set; }
        public virtual ChemicalType ChemicalType { get; set; }
        public virtual UnitOfMassMeasure UnitOfMassMeasure { get; set; }
        public virtual UnitOfVolumetricMeasure UnitOfVolumetricMeasure { get; set; }
        public virtual UnitOfItemMeasure UnitOfItemMeasure { get; set; }
    }
}