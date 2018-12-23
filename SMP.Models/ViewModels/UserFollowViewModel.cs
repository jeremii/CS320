using System;
namespace SMP.Models.ViewModels
{
    public class UserFollowViewModel
    {
        public string UserId { get; set; }

        public string FullName { get; set; }

        public bool isFollowing { get; set; }

        public bool isFollowingBack { get; set; }
    }
}