using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    [Table("UnitsOfMassMeasure", Schema = "Aquarium")]
    public class UnitOfMassMeasure : IUnitOfMeasure
    {
        [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.None), Column("Id")]
        public int Id { get; set; }

        [Required, StringLength(50), Column("Name")]
        public string Name { get; set; }

        [Required, Column("VolumeInGrams")]
        public decimal VolumeInGrams { get; set; }
    }
}