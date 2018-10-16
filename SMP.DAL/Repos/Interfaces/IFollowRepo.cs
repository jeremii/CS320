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
        IEnumerable<UserFollowViewModel> GetFollowing(string id, string myId);
        IEnumerable<UserFollowViewModel> GetFollowers(string id, string myId);
        Task<IEnumerable<UserFollowViewModel>> GetFollowersOfUser(string id, string myId);
        Task<IEnumerable<UserFollowViewModel>> GetWhoUserIsFollowing(string id, string myId);
        Task<UserFollowViewModel> GetUserFollow(bool followers, Follow follow, User user, string myId);
        Task<bool> IsFollowingAsync(string userId, string followerId);
        Follow GetOne(string userId, string followId);
    }
}
