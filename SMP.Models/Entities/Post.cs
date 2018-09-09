using System;
using System.Collections.Generic;
using System.Text;
using SMP.Models.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMP.Models.Entities
{
    [Table("Posts", Schema = "SMP")]
    public class Post : EntityBase
    {
        [ForeignKey(nameof(UserId))]
        public int UserId { get; set; }

        [DataType(DataType.MultilineText), MaxLength(128)]
        public string Text { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Time { get; set; }
    }
}