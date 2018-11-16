using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace SMP.Models.Entities
{
    [Table("Users", Schema = "SMP")]
    public class User : IdentityUser
    {
        [DataType(DataType.Text), MaxLength(255)]
        public string FirstName { get; set; }
        [DataType(DataType.Text), MaxLength(255)]
        public string LastName { get; set; }

        [InverseProperty(nameof(Post.User))]
        public List<Post> Posts { get; set; } = new List<Post>();

        [InverseProperty(nameof(Follow.User))]
        public List<Follow> Follows { get; set; } = new List<Follow>();
        [InverseProperty(nameof(Follow.Follower))]
        public List<Follow> Followers { get; set; } = new List<Follow>();

        [InverseProperty(nameof(Rss.User))]
        public List<Rss> RSSFeeds { get; set; } = new List<Rss>();

        [InverseProperty(nameof(Message.Receiver))]
        public List<Message> ReceivedMessages { get; set; }

        [InverseProperty(nameof(Message.Sender))]
        public List<Message> SentMessages { get; set; }
    }
}
