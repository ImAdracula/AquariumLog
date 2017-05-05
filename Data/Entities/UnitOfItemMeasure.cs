using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    [Table("UnitsOfItemMeasure", Schema = "Aquarium")]
    public class UnitOfItemMeasure : IUnitOfMeasure
    {
        [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.None), Column("Id")]
        public int Id { get; set; }

        [Required, StringLength(50), Column("Name")]
        public string Name { get; set; }
    }
}