using System;
namespace SMP.Models.ViewModels
{
    public class UserOverviewViewModel
    {
        public string UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int FollowerCount { get; set; }

        public int FollowingCount { get; set; }
    }
}
