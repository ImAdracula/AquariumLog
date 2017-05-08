using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JessicasAquariumMonitor.Data.Entities
{
    [Table("LogEntries", Schema = "Aquarium")]
    public class LogEntry
    {
        public LogEntry()
        {
            AdministeredChemicals = new HashSet<AdministeredChemical>();
            ReplacedFilters = new HashSet<ReplacedFilter>();
        }

        [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("Id")]
        public int Id { get; set; }

        [Required, Column("TimeStamp")]
        public DateTime TimeStamp { get; set; }

        [Column("PotentialOfHydrogen")]
        public byte? PotentialOfHydrogen { get; set; }

        public virtual ICollection<AdministeredChemical> AdministeredChemicals { get; }
        public virtual ICollection<ReplacedFilter> ReplacedFilters { get; }
    }
}