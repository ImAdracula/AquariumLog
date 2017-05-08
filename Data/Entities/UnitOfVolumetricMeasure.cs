using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JessicasAquariumMonitor.Data.Entities
{
    [Table("UnitsOfVolumetricMeasure", Schema = "Aquarium")]
    public class UnitOfVolumetricMeasure : IUnitOfMeasure
    {
        [Required, Column("VolumeInCubicCentimeters")]
        public decimal VolumeInCubicCentimeters { get; set; }

        [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("Id")]
        public int Id { get; set; }

        [Required, StringLength(50), Column("Name")]
        public string Name { get; set; }
    }
}