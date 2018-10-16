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

        //Task<IList<OrderItem>> GetOrderItems(int RequisitionId);
        //Task<IList<Requisition>> GetAllReqsUnderDept();
        //Task<IList<Requisition>> GetAllReqsByStatusCode(int statCode);

        //Task<List<SelectListItem>> GetDeptsForDropdown();
        //Task<List<SelectListItem>> GetCollegesForDropdown();
        //Task<List<SelectListItem>> GetEmployeesForDropdown();
        //Task<List<SelectListItem>> GetCampusesForDropdown();
        //Task<List<SelectListItem>> GetCategoriesForDropdown();

        //Task<string> GetEmployeeName(int Id);


        //Task<Dictionary<int, College>> GetCollegeDictionary();
        //Task<Dictionary<int, Campus>> GetCampusDictionary();
        //Task<Dictionary<int, string>> GetDeptNamesDictionary();
        //Task<Dictionary<int, Address>> GetAddressDictionary();
        //Task<Dictionary<int, Employee>> GetEmployeeDictionary();
        //Task<Dictionary<int, StatusCode>> GetStatusCodeDictionary();
        //Task<Dictionary<int, Category>> GetCategoryDictionary();
        //Task<List<EmployeeGroup>> GetAllDeptsOfCampus(int id);
        //Task<int> GetLatestReqId();
        //Task<int> GetLatestOrderItemId();
        //Task<int> GetLatestItemId();
        //Task<int> GetLatestLoginId();

        //Task<Dictionary<int, int>> GetCampusCount();
        //Task<Dictionary<int, int>> GetEmployeeCount();
        //Task<Dictionary<int, int>> GetBudgetCount();
        //Task<Dictionary<int, int>> GetItemCount();

        //Task<LoginViewModel> LoginEmployee(LoginViewModel item);

        ////Task<IList<Employee>> GetEmployeesAsync();
        //Task<College> GetCollegeAsync(int id);
        //Task<IList<College>> GetCollegesAsync();
        //Task<Campus> GetCampusAsync(int id);
        //Task<IList<Campus>> GetCampusesAsync();

    }
}