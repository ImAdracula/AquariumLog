using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JessicasAquariumMonitor.Data.Entities
{
    [Table("ReplacedFilters", Schema = "Aquarium")]
    public class ReplacedFilter
    {
        [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.None), Column("Id")]
        public int Id { get; set; }

        [Required, Column("LogEntryId"), ForeignKey(nameof(LogEntry))]
        public int LogEntryId { get; set; }

        [Required, Column("FilterTypeId"), ForeignKey(nameof(FilterType))]
        public int FilterTypeId { get; set; }

        [StringLength(50), Column("Note")]
        public string Note { get; set; }

        public virtual LogEntry LogEntry { get; set; }
        public virtual FilterType FilterType { get; set; }
    }
}