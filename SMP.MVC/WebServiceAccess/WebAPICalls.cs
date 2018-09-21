
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SMP.MVC.Configuration;
using SMP.Models.Entities;
using SMP.Models.Entities.Base;
using SMP.Models.ViewModels;
//using SMP.Models.ViewModels.Base;
using SMP.MVC.WebServiceAccess.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SMP.MVC.WebServiceAccess
{
    public class WebApiCalls : WebApiCallsBase, IWebApiCalls
    {
        private readonly string skipTake = "skip=0&take=10";

        public WebApiCalls(IWebServiceLocator settings) : base(settings)
        {
            
        }
        // ----------------------------------------------------------------------------------
        // GENERIC ASYNC METHODS
        // These generic async methods can be used for most CRUD operations of most return type.
        // When calling, pass the type argument as 'new Type()'
        // ----------------------------------------------------------------------------------
        public async Task<IList<T>> GetAllAsync<T>(T item) where T : class, new()
        {
            return await GetItemListAsync<T>(GetUri(item));
        }

        public async Task<IList<T>> GetSomeAsync<T>(T item, int id ) where T : class, new()
        {
            return await GetItemListAsync<T>(GetUri(item)+$"{id}?{skipTake}");
        }

        public async Task<T> GetOneAsync<T>(T item, int id) where T : class, new()
        {
            return await GetItemAsync<T>(GetUri(item)+$"{id}");
        }

        public async Task<string> CreateAsync<T>(T item)
        {
            string uri = GetUri(item) + "Create";

            var json = JsonConvert.SerializeObject(item);
            return await SubmitPostRequestAsync(uri, json);
        }

        public async Task<string> UpdateAsync<T>(int id, T item)
        {
            string uri = GetUri(item) + "Update/" + id ;

            var json = JsonConvert.SerializeObject(item);
            return await SubmitPutRequestAsync(uri, json);
        }

        public async Task DeleteAsync<T>(T item, int id )
        {
            string uri = GetUri(item) + "Delete/" + id ;
            await SubmitDeleteRequestAsync(uri);
        }

        // -----------------------------------------
        // USER ------------------------------------
        // -----------------------------------------

        public async Task<string> LoginAsync(LoginViewModel model)
        {
            var json = JsonConvert.SerializeObject(model);
            return await SubmitPostRequestAsync($"{LoginUri}", json);
        }

        public async Task<string> LogoutAsync()
        {
            return await SubmitPostRequestAsync($"{LogoutUri}", null);
        }

        // -----------------------------------------
        // POSTS -----------------------------------
        // -----------------------------------------

        // For now - unless anything arises - the generic methods handle these.

        // -----------------------------------------
        // FOLLOW ----------------------------------
        // -----------------------------------------

        public async Task<IList<UserPostViewModel>> GetFollowingPostsAsync(string userId)
        {
            return await GetItemListAsync<UserPostViewModel>($"{FollowingPostsUri}{userId}?{skipTake}");
        }
        public async Task<IList<UserOverviewViewModel>> GetFollowersAsync(string userId)
        {
            return await GetItemListAsync<UserOverviewViewModel>($"{FollowerUri}{userId}?{skipTake}");
        }
        public async Task<IList<UserOverviewViewModel>> GetFollowingAsync(string userId)
        {
            return await GetItemListAsync<UserOverviewViewModel>($"{FollowingUri}{userId}?{skipTake}");
        }


        //public async Task<int> GetLatestReqId()
        //{
        //    IList<Requisition> list = await GetAllAsync<Requisition>(new Requisition());
        //    return list[list.Count - 1].Id;
        //}
        //public async Task<int> GetLatestOrderItemId()
        //{
        //    IList<OrderItem> list = await GetAllAsync<OrderItem>(new OrderItem());
        //    return list[list.Count - 1].Id;
        //}
        //public async Task<int> GetLatestItemId()
        //{
        //    IList<Item> list = await GetAllAsync<Item>(new Item());
        //    return list[list.Count - 1].Id;
        //}
        //public async Task<int> GetLatestLoginId()
        //{
        //    IList<Login> list = await GetAllAsync<Login>(new Login());
        //    return list[list.Count - 1].EmployeeId;
        //}
        //public async Task<string> GetEmployeeName(int Id)
        //{
        //    Employee emp = await GetOneAsync(new Employee(), Id);
        //    return emp.FirstName + " " + emp.LastName;
        //}
        //public async Task<IList<Requisition>> GetAllReqsByStatusCode( int statCode )
        //{
        //    IList<Requisition> reqs = new List<Requisition>();
        //    IList<Requisition> allReqs = await GetAllAsync(new Requisition());
        //    foreach (Requisition item in allReqs)
        //    {
        //        if (item.StatusCodeId == statCode )
        //        {
        //            reqs.Add(item);
        //        }
        //    }
        //    return reqs;
        //}
        //public async Task<IList<Requisition>> GetAllReqsUnderDept()
        //{
        //    IList<Requisition> reqs = new List<Requisition>();
        //    int userId = await GetLatestLoginId();
        //    IList<Requisition> allReqs = await GetAllAsync(new Requisition());
        //    foreach( Requisition item in allReqs )
        //    {
        //        Employee requester = await GetOneAsync(new Employee(), item.RequesterId);
        //        EmployeeGroup requesterGroup = await GetOneAsync(new EmployeeGroup(), requester.EmployeeGroupId);
        //        Employee approver = await GetOneAsync(new Employee(), userId);
        //        if( approver.EmployeeGroupId == requesterGroup.ParentId )
        //        {
        //            reqs.Add(item);
        //        }
        //    }
        //    return reqs;

        //}
        //// ------------------------------------------------------------- //
        //// --------------------------- COUNT --------------------------- //
        //// ------------------------------------------------------------- //

        //public async Task<Dictionary<int, int>> GetCampusCount()
        //{
        //    var result = new Dictionary<int, int>();
        //    foreach (College one in await GetAllAsync(new College()))
        //    {
        //        result.Add(one.Id, 0);
        //    }
        //    foreach (Campus one in await GetAllAsync(new Campus()))
        //    {
        //        result[one.CollegeId]++;
        //    }
        //    return result;
        //}
        //public async Task<Dictionary<int, int>> GetEmployeeCount()
        //{
        //    var result = new Dictionary<int, int>();
        //    foreach (EmployeeGroup one in await GetAllAsync(new EmployeeGroup()))
        //    {
        //        result.Add(one.Id, 0);
        //    }
        //    foreach (Employee one in await GetAllAsync(new Employee()))
        //    {
        //        result[one.EmployeeGroupId]++;
        //    }
        //    return result;
        //}
        //public async Task<Dictionary<int, int>> GetBudgetCount()
        //{
        //    var result = new Dictionary<int, int>();
        //    foreach (EmployeeGroup one in await GetAllAsync(new EmployeeGroup()))
        //    {
        //        result.Add(one.Id, 0);
        //    }
        //    foreach (Budget one in await GetAllAsync(new Budget()))
        //    {
        //        result[one.EmployeeGroupId]++;
        //    }
        //    return result;
        //}
        //public async Task<Dictionary<int, int>> GetItemCount()
        //{
        //    var result = new Dictionary<int, int>();
        //    foreach (Vendor one in await GetAllAsync(new Vendor()))
        //    {
        //        result.Add(one.Id, 0);
        //    }
        //    foreach (Item one in await GetAllAsync(new Item()))
        //    {
        //        result[one.VendorId]++;
        //    }
        //    return result;
        //}
        //public async Task<List<EmployeeGroup>> GetAllDeptsOfCampus(int id)
        //{
        //    var result = new List<EmployeeGroup>();
        //    foreach( EmployeeGroup item in await GetAllAsync(new EmployeeGroup()))
        //    {
        //        if( item.CampusId == id )
        //        {
        //            result.Add(item);
        //        }
        //    }
        //    return result;
        //}
        //// --------------------------------------------------------------
        //// --------------------------------------------------------------
        //// --------------------------------------------------------------

        //public async Task<LoginViewModel> LoginEmployee(LoginViewModel item)
        //{
        //    return await GetItemAsync<LoginViewModel>(GetUri(item));
        //}

        //// ------------------------------------------------------------- //
        //// ------------------------- DROPDOWN -------------------------- //
        //// ------------------------------------------------------------- //
        //public async Task<List<SelectListItem>> GetDeptsForDropdown()
        //{
        //    var groups = await GetAllAsync(new EmployeeGroup());

        //    var ls = new List<SelectListItem>();

        //    foreach (EmployeeGroup one in groups)
        //    {
        //        ls.Add( new SelectListItem
        //        {
        //            Value = one.Id.ToString(),
        //            Text = one.Name
        //        });
        //    }
        //    return ls;
        //}
        //public async Task<List<SelectListItem>> GetCollegesForDropdown()
        //{
        //    var items = await GetAllAsync(new College());

        //    var ls = new List<SelectListItem>();

        //    foreach (College one in items)
        //    {
        //        ls.Add(new SelectListItem
        //        {
        //            Value = one.Id.ToString(),
        //            Text = one.Name
        //        });
        //    }
        //    return ls;
        //}
        //public async Task<List<SelectListItem>> GetEmployeesForDropdown()
        //{
        //    var groups = await GetAllAsync(new Employee());

        //    var ls = new List<SelectListItem>();

        //    foreach (Employee one in groups)
        //    {
        //        ls.Add(new SelectListItem
        //        {
        //            Value = one.Id.ToString(),
        //            Text = one.FirstName + " " + one.LastName
        //        });
        //    }
        //    return ls;
        //}
        //public async Task<List<SelectListItem>> GetCampusesForDropdown()
        //{
        //    var groups = await GetAllAsync(new Campus());

        //    var ls = new List<SelectListItem>();

        //    foreach (Campus one in groups)
        //    {
        //        ls.Add(new SelectListItem
        //        {
        //            Value = one.Id.ToString(),
        //            Text = one.Name
        //        });
        //    }
        //    return ls;
        //}
        //public async Task<List<SelectListItem>> GetCategoriesForDropdown()
        //{
        //    var items = await GetAllAsync(new Category());

        //    var ls = new List<SelectListItem>();

        //    foreach (Category one in items)
        //    {
        //        ls.Add(new SelectListItem
        //        {
        //            Value = one.Id.ToString(),
        //            Text = one.Name
        //        });
        //    }
        //    return ls;
        //}
        //// -------------------------------------------------------------- //
        //// ----------------------- ID -> NAMES -------------------------- //
        //// -------------------------------------------------------------- //
        //public async Task<Dictionary<int, string>> GetDeptNamesDictionary()
        //{
        //    Dictionary<int, string> DeptNames = new Dictionary<int, string>();
        //    foreach (EmployeeGroup e in GetAllAsync(new EmployeeGroup()).Result)
        //    {
        //        DeptNames.Add(e.Id, e.Name);
        //    }
        //    return DeptNames;
        //}
        ////public async Task<Dictionary<int, T>> GetDictionary<T>( T item) where T : EntityBase, new()
        ////{
        ////    var result = new Dictionary<int, item.GetType()>();
        ////    foreach( T e in GetAllAsync( new T()).Result )
        ////    {
        ////        result.Add(e.Id, e);
        ////    }
        ////    return result;
        ////}
        //public async Task<Dictionary<int, College>> GetCollegeDictionary()
        //{
        //    var result = new Dictionary<int, College>();
        //    foreach ( College e in GetAllAsync(new College()).Result)
        //    {
        //        result.Add(e.Id, e);
        //    }
        //    return result;
        //}
        //public async Task<Dictionary<int, Campus>> GetCampusDictionary()
        //{
        //    var result = new Dictionary<int, Campus>();
        //    foreach (Campus e in GetAllAsync(new Campus()).Result)
        //    {
        //        result.Add(e.Id, e);
        //    }
        //    return result;
        //}
        //public async Task<Dictionary<int, Address>> GetAddressDictionary()
        //{
        //    var result = new Dictionary<int, Address>();
        //    foreach (Address e in GetAllAsync(new Address()).Result)
        //    {
        //        result.Add(e.Id, e);
        //    }
        //    return result;
        //}
        //public async Task<Dictionary<int, Employee>> GetEmployeeDictionary()
        //{
        //    var result = new Dictionary<int, Employee>();
        //    result.Add(0, new Employee { FirstName = "" });
        //    foreach (Employee e in GetAllAsync(new Employee()).Result)
        //    {
        //        result.Add(e.Id, e);
        //    }
        //    return result;
        //}
        //public async Task<Dictionary<int, StatusCode>> GetStatusCodeDictionary()
        //{
        //    var result = new Dictionary<int, StatusCode>();
        //    foreach (StatusCode e in GetAllAsync(new StatusCode()).Result)
        //    {
        //        result.Add(e.Id, e);
        //    }
        //    return result;
        //}
        //public async Task<Dictionary<int, Category>> GetCategoryDictionary()
        //{
        //    var result = new Dictionary<int, Category>();
        //    foreach (Category e in GetAllAsync(new Category()).Result)
        //    {
        //        result.Add(e.Id, e);
        //    }
        //    return result;
        //}
        //// --------------------------------------------------------------
        //// --------------------------------------------------------------
        //// --------------------------------------------------------------

        //public async Task<College> GetCollegeAsync(int id)
        //{
        //    return await GetOneAsync(new College(), id);
        //}

        //public async Task<IList<College>> GetCollegesAsync()
        //{
        //    return await GetAllAsync(new College());
        //}

        //public async Task<Campus> GetCampusAsync(int id)
        //{
        //    return await GetOneAsync(new Campus(), id);
        //}

        //public async Task<IList<Campus>> GetCampusesAsync()
        //{
        //    return await GetAllAsync(new Campus());
        //}
        //public async Task<IList<OrderItem>> GetOrderItems ( int RequisitionId )
        //{
        //    IList<OrderItem> all = await GetAllAsync(new OrderItem());
        //    IList<OrderItem> filtered = new List<OrderItem>();

        //    IList<Item> allItems = await GetAllAsync(new Item());

        //    IList<Vendor> allVendors = await GetAllAsync(new Vendor());

        //    foreach ( OrderItem one in all )
        //    {
        //        if( one.RequisitionId == RequisitionId )
        //        {
        //            one.Item = await GetOneAsync(new Item(), one.ItemId);
        //            one.Item.Vendor = await GetOneAsync(new Vendor(), one.Item.VendorId);
        //            filtered.Add(one);
        //        }
        //    }
        //    return filtered;
        //}

    }
}
