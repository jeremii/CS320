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
        IEnumerable<UserOverviewViewModel> GetAll();
        IEnumerable<UserOverviewViewModel> GetRange(int skip = 0, int take = 10);
        UserOverviewViewModel GetOne(User user, IEnumerable<Post> posts, IEnumerable<Follow> following, IEnumerable<Follow> followers);
        Task<User> GetUserModel(string id);
        UserOverviewViewModel GetUser(string id);
        IEnumerable<Post> GetUserPosts(string id);
        IEnumerable<UserOverviewViewModel> FindUsers(string keyword);
        int Update(User user, bool persists = true);
    }
}
