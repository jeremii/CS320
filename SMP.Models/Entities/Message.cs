using System;
using System.Collections.Generic;
using System.Text;
using SMP.Models.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMP.Models.Entities
{
    [Table("Messages", Schema = "SMP")]
    public class Message : EntityBase
    {
        [Required]
        public string SenderId { get; set; }
        [ForeignKey(nameof(SenderId))]
        public User Sender { get; set; }

        [Required]
        public string ReceiverId { get; set; }
        [ForeignKey(nameof(ReceiverId))]
        public User Receiver { get; set; }

        [Required]
        [DataType(DataType.MultilineText), MaxLength(5120)]
        public string Text { get; set; }

        public DateTime Time { get; set; } = DateTime.UtcNow;
    }
}
