using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    [Table("ReplacedFilters", Schema = "Aquarium")]
    public class ReplacedFilter
    {
        [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.None), Column("Id")]
        public int Id { get; set; }

        [Required, Column("LogEntryId")]
        public int LogEntryId { get; set; }

        [Required, Column("FilterTypeId")]
        public int FilterTypeId { get; set; }

        [StringLength(50), Column("Note")]
        public string Note { get; set; }

    }
}