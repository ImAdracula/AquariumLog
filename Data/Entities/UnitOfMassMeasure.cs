using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JessicasAquariumMonitor.Data.Entities
{
    [Table("UnitsOfMassMeasure", Schema = "Aquarium")]
    public class UnitOfMassMeasure : IUnitOfMeasure
    {
        [Required, Column("VolumeInGrams")]
        public decimal VolumeInGrams { get; set; }

        [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("Id")]
        public int Id { get; set; }

        [Required, StringLength(50), Column("Name")]
        public string Name { get; set; }
    }
}