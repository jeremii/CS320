using System;
using System.Collections.Generic;
using System.Text;
using SMP.Models.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMP.Models.Entities
{
    [Table("Follows", Schema = "SMP")]
    public class Follow : EntityBase
    {
        [ForeignKey(nameof(UserId))]
        public int UserId { get; set; }
        public User User { get; set; }

        [ForeignKey(nameof(FollowerId))]
        public int FollowerId { get; set; }
    }
}