using System;
namespace SMP.Models.ViewModels
{
    public class UserOverviewViewModel
    {
        public string UserId { get; set; }

        public string FullName { get; set; }

        public int FollowerCount { get; set; }

        public int FollowingCount { get; set; }

        public int PostCount { get; set; }

        public string Bio { get; set; }
    }
}
