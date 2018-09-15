using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SMP.Models.Entities;

namespace SMP.Models.Entities
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        [DataType(DataType.Text), MaxLength(255)]
        public string FirstName { get; set; }
        [DataType(DataType.Text), MaxLength(255)]
        public string LastName { get; set; }

        [InverseProperty(nameof(Post.User))]
        public List<Post> Posts = new List<Post>();
        [InverseProperty(nameof(Follow.User))]
        public List<Follow> Follows = new List<Follow>();
    }
}
