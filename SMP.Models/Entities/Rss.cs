using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using SMP.Models.Entities.Base;

namespace SMP.Models.Entities
{
    [Table("Rss", Schema = "SMP")]
    public class Rss : EntityBase
    {
        [Required]
        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        [Required]
        public string Url { get; set; }
    }
}
