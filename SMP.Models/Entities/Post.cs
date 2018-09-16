using System;
using System.Collections.Generic;
using System.Text;
using SMP.Models.Entities.Base;
using SMP.Models.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMP.Models.Entities
{
    [Table("Posts", Schema = "SMP")]
    public class Post : EntityBase
    {
        [Required]
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; }

        [Required]
        [DataType(DataType.MultilineText), MaxLength(5120)]
        public string Text { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Time { get; set; }


    }
}