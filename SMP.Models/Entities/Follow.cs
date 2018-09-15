using System;
using System.Collections.Generic;
using System.Text;
using SMP.Models.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMP.Models.Entities
{
    //[Table("Follows", Schema = "SMP")]
    public class Follow : EntityBase
    {
        [Required]
        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; }

        [ForeignKey(nameof(FollowerId))]
        public int FollowerId { get; set; }
    }
}