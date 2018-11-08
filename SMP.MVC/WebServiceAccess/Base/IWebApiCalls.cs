using System.Collections.Generic;
using System.Threading.Tasks;
using SMP.Models.Entities;
using SMP.Models.ViewModels.HomeViewModels;
using SMP.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SMP.MVC.WebServiceAccess.Base
{
    public interface IWebApiCalls
    {

        Task<IList<T>> GetAllAsync<T>(T item) where T : class, new();
        Task<IList<T>> GetSomeAsync<T>(T item, int id) where T : class, new();
        Task<T> GetOneAsync<T>(T item, int id) where T : class, new();

        Task<string> CreateAsync<T>(T item);
        Task<string> UpdateAsync<T>(int id, T item);
        Task<string> UpdateAsync<T>(string id, T item);
        Task DeleteAsync<T>(T item, int id);
        Task Delete2StringIdsAsync<T>(T item, string id, string id2);

        Task<T> GetOneAsync<T>(T item, string id) where T : class, new();
        Task<IList<T>> GetSomeAsync<T>(T item, string id) where T : class, new();
        // -----------------------------------------
        // USER ------------------------------------
        // -----------------------------------------

        Task<string> LoginAsync(LoginViewModel model);
        Task<string> LogoutAsync();
        Task<IList<T>> GetSomeAsync<T>(T item, string id, bool descending) where T : class, new();
        Task<IList<UserFollowViewModel>> SearchAsync(string userId, string keyword);
        Task<IList<UserFollowViewModel>> GetAllUsers(string myId);
        // -----------------------------------------
        // POSTS -----------------------------------
        // -----------------------------------------


        // -----------------------------------------
        // FOLLOW ----------------------------------
        // -----------------------------------------

        Task<IList<UserPostViewModel>> GetFollowingPostsAsync(string userId);
        Task<IList<UserFollowViewModel>> GetFollowersAsync(string userId, string myId);
        Task<IList<UserFollowViewModel>> GetFollowingAsync(string userId, string myId);
        Task<bool> IsFollowingAsync(string id, string followerId);
        Task UnfollowUser(string userId, string followId);
        Task FollowUser(string userId, string followId);

        // -----------------------------------------
        // RSS -------------------------------------
        // -----------------------------------------

        Task<IList<Rss>> GetRss(string userId);
    }
}