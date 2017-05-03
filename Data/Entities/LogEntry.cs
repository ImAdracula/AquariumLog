using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    [Table("LogEntries", Schema = "Aquarium")]
    public class LogEntry
    {
        [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.None), Column("Id")]
        public int Id { get; set; }

        [Required, Column("TimeStamp")]
        public System.DateTime TimeStamp { get; set; }

        [Column("PotentialOfHydrogen")]
        public System.Nullable<byte> PotentialOfHydrogen { get; set; }

    }
}