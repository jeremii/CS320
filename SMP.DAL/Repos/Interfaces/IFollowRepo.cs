using System;
using SMP.DAL.Repos.Base;
using SMP.Models.Entities;
using SMP.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SMP.DAL.Repos.Interfaces
{
    public interface IFollowRepo : IRepo<Follow>
    {
        IEnumerable<UserFollowViewModel> GetFollowing(string id);
        IEnumerable<UserFollowViewModel> GetFollowers(string id);
        Task<IEnumerable<UserFollowViewModel>> GetFollowersOfUser(string id);
        Task<IEnumerable<UserFollowViewModel>> GetWhoUserIsFollowing(string id);
        Task<UserFollowViewModel> GetUserFollow(bool followers, Follow follow, User user);
        Task<bool> IsFollowingAsync(string userId, string followerId);
        Follow GetOne(string userId, string followId);
    }
}
