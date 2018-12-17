using System;
using SMP.DAL.Repos.Base;
using SMP.Models.Entities;
using SMP.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SMP.DAL.Repos.Interfaces
{
    public interface IUserRepo
    {
        IEnumerable<UserFollowViewModel> GetAll( string myId);
        IEnumerable<User> GetAll();
        IEnumerable<UserOverviewViewModel> GetRange(int skip = 0, int take = 10);
        //UserOverviewViewModel GetOne(User user);
        UserOverviewViewModel GetOne(User user, IEnumerable<Post> posts, IEnumerable<Follow> follows, IEnumerable<Follow> followers);
        Task<User> GetUserModel(string id);
        UserOverviewViewModel GetUser(string id);
        IEnumerable<Post> GetUserPosts(string id);
        IEnumerable<UserFollowViewModel> FindUsers(string userId, string keyword);
        int Update(User user, bool persists = true);
        string FindIdByName(string first, string last);
    }
}
