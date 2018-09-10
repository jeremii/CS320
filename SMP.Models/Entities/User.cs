using System;
using System.Collections.Generic;
using System.Text;
using SMP.Models.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMP.Models.Entities
{
    [Table("Users", Schema = "SMP")]
    public class User : EntityBase
    {
        [DataType(DataType.Text), MaxLength(255)]
        public string FirstName { get; set; }
        [DataType(DataType.Text), MaxLength(255)]
        public string LastName { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [InverseProperty(nameof(Follow.User))]
        public List<Follow> Follows { get; set; }
    }
}