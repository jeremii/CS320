using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SMP.Models.Entities;
using SMP.DAL.EF;

namespace SMP.DAL.Initializers
{
    public static class SampleData
    {

        public static IEnumerable<User> GetUsers() => new List<User>
        {
            new User {Email = "erhodes8@wvup.edu", NormalizedEmail = "ERHODES8@WVUP.EDU", SecurityStamp = Guid.NewGuid().ToString()},
        };

        public static IEnumerable<Post> GetPosts(List<User> Users) => new List<Post>
        {
            new Post { UserId = Users.Where( x => x.Email == "erhodes8@wvup.edu").FirstOrDefault().Id, Text = "Test"}
        };

        public static IList<Follow> GetFollows()
        {
            var follows = new List<Follow>();

            follows.AddRange(new List<Follow>
            {
                new Follow
                {
                    //UserId = 1,
                    FollowerId = 2
                },
                new Follow
                {
                    //UserId = 1,
                    FollowerId = 3
                },
                new Follow
                {
                    //UserId = 2,
                    FollowerId = 1
                },
                new Follow
                {
                    //UserId = 2,
                    FollowerId = 3
                }
            });
            return follows;
        }
        //public static IEnumerable<User> GetUsers() => new List<User>
        //{
        //    new User {
        //        Email = "erhodes8@wvup.edu", 
        //        NormalizedEmail = "ERHODES8@WVUP.EDU", 
        //        SecurityStamp = Guid.NewGuid().ToString()
        //    },
        //};

        //public static IList<Post> GetPosts()
        //{
        //    var obj = new List<Post>();

        //    obj.AddRange(new List<Post>
        //    {
        //        new Post
        //        {
        //            Text = "Test",
        //            Time = DateTime.Now,
        //        },
        //        new Post
        //        {
        //            Text = "Test 2",
        //            Time = DateTime.Now,
        //            //UserId = 2
        //        },
        //    });
        //    return obj;
        //}
        //public static IList<College> GetColleges() => new List<College>
        //{
        //    new College()
        //    {
        //        //Id = 1,
        //        Name = "WVU @ Parkersburg",
        //    }
        //};
        //public static IList<Campus> GetCampuses() => new List<Campus>
        //{
        //    new Campus()
        //    {
        //        //Id = 1, 
        //        CollegeId = 1,
        //        Name = "Main Campus",
        //        Phone = "304-424-8000",
        //        AddressId = 3
        //    },
        //    new Campus()
        //    {
        //        //Id = 2, 
        //        CollegeId = 1,
        //        Name = "Jackson County Center",
        //        Phone = "304-372-6992",
        //        AddressId = 4
        //    }
        //};
        //public static IList<Approver> GetApprovers()
        //{
        //    var approvers = new List<Approver>();

        //    approvers.AddRange(new List<Approver>
        //    {
        //        new Approver
        //        {
        //            Reasoning = "No reason.",
        //            EmployeeId = 1
        //        }
        //        ,
        //        new Approver
        //        {
        //            Reasoning = "For some reason.",
        //            EmployeeId = 2
        //        }
        //    });
        //    return approvers;
        //}
        //public static IEnumerable<Budget> GetBudgets() => new List<Budget>
        //{
        //    new Budget()
        //    {
        //        Name = "CS",
        //        DaCode = "731180007",
        //        Amount = 31100988.00,
        //        Active = true,
        //        StartDate = DateTime.Today,
        //        EndDate = null,
        //        EmployeeGroupId = 5

        //    },
        //    new Budget()
        //    {
        //        Name = "Nurse",
        //        DaCode = "731180008",
        //        Amount = 31624189.00,
        //        Active = true,
        //        StartDate = DateTime.Today,
        //        EndDate = null,
        //        EmployeeGroupId = 4
        //    },
        //    new Budget()
        //    {
        //        Name = "Backup",
        //        DaCode = "734060001",
        //        Amount = 31100988.00,
        //        Active = true,
        //        StartDate = DateTime.Today,
        //        EndDate = null,
        //        EmployeeGroupId = 1

        //    }
        //};
        //public static IList<EmployeeGroup> GetEmployeeGroups() => new List<EmployeeGroup>
        //{
        //    new EmployeeGroup()
        //    {
        //        Name = "Finance & Administration",
        //        //Id = 13,
        //        ParentId = null,
        //        CampusId = 1
        //    },
        //    new EmployeeGroup()
        //    {
        //        Name = "STEM Division",
        //        //Id = 9,
        //        ParentId = 1,
        //        CampusId = 1,
        //        //HeadId = 3
        //    },
        //    new EmployeeGroup()
        //    {
        //        Name = "Nursing & Health Sciences Division",
        //        //Id = 10,
        //        ParentId = 1,
        //        CampusId = 1
        //    },
        //    new EmployeeGroup()
        //    {
        //        Name = "Education Division",
        //        //Id = 11,
        //        ParentId = 1,
        //        CampusId = 1
        //    },
        //    new EmployeeGroup()
        //    {
        //        Name = "Business & Economics Division",
        //        //Id = 12, 
        //        ParentId = 1,
        //        CampusId = 1
        //    },
        //    new EmployeeGroup()
        //    {
        //        Name = "Purchasing Department",
        //        //Id = 1,
        //        ParentId = 1,
        //        CampusId = 1
        //    },
        //    new EmployeeGroup()
        //    {
        //        Name = "IT Department",
        //        //Id = 2,
        //        ParentId = 2,
        //        CampusId = 1
        //    },
        //    new EmployeeGroup()
        //    {
        //        Name = "Business Office",
        //        //Id = 3,
        //        ParentId = 5,
        //        CampusId = 1
        //    },
        //    new EmployeeGroup()
        //    {
        //        Name = "Nursing Department",
        //        //Id = 4,
        //        ParentId = 3,
        //        CampusId = 1
        //    },
        //    new EmployeeGroup()
        //    {
        //        Name = "Computer Science",
        //        //Id = 5,
        //        ParentId = 2,
        //        CampusId = 1
        //    },
        //    new EmployeeGroup()
        //    {
        //        Name = "Math Department",
        //        //Id = 6,
        //        ParentId = 2,
        //        CampusId = 1
        //    },
        //    new EmployeeGroup()
        //    {
        //        Name = "Teaching Department",
        //        //Id = 7,
        //        ParentId = 4,
        //        CampusId = 1
        //    }
        //};
        //public static IList<Employee> GetEmployees() => new List<Employee>
        //{
        //    new Employee()
        //    {
        //        FirstName = "Charles", LastName = "Almond",
        //        //Id = 1,
        //        EmployeeGroupId = 10, Active = true,
        //        StartDate = DateTime.Today,
        //        EndDate = null,
        //        Email = "calmond@wvup.edu", Password = "123"
        //    },
        //    new Employee()
        //    {
        //        FirstName = "Gary", LastName = "Thompson",
        //        //Id = 2, 
        //        EmployeeGroupId = 10, Active = true,
        //        StartDate = DateTime.Today,
        //        EndDate = null,
        //        Email = "gthompson@wvup.edu", Password = "123"
        //    },
        //    new Employee()
        //    {
        //        FirstName = "Jared", LastName = "Gump",
        //        //Id = 3, 
        //        EmployeeGroupId = 2, Active = true,
        //        StartDate = DateTime.Today,
        //        EndDate = null,
        //        Email = "jgump@wvup.edu", Password = "123",
        //    },
        //    new Employee()
        //    {
        //        FirstName = "Kathy", LastName = "Frum",
        //        //Id = 4, 
        //        EmployeeGroupId = 3, Active = true,
        //        StartDate = DateTime.Today,
        //        EndDate = null,
        //        Email = "kfrum@wvup.edu", Password = "123"
        //    },
        //    new Employee()
        //    {
        //        FirstName = "Julie", LastName = "Heller",
        //        //Id = 5, 
        //        EmployeeGroupId = 4, Active = true,
        //        StartDate = DateTime.Today,
        //        EndDate = null,
        //        Email = "jheller@wvup.edu", Password = "123",
        //    },
        //    new Employee()
        //    {
        //        FirstName = "David", LastName = "Lancaster",
        //        //Id = 6, 
        //        EmployeeGroupId = 6, Active = true,
        //        StartDate = DateTime.Today,
        //        EndDate = null,
        //        Email = "dlancaster@wvup.edu", Password = "123"
        //    },
        //    new Employee()
        //    {
        //        FirstName = "Jeffrey", LastName = "Holland",
        //        //Id = 7, 
        //        EmployeeGroupId = 6, Active = true,
        //        StartDate = DateTime.Today,
        //        EndDate = null,
        //        Email = "jholland@wvup.edu", Password = "123"
        //    },
        //    new Employee()
        //    {
        //        FirstName = "Alice", LastName = "Harris",
        //        //Id = 8, 
        //        EmployeeGroupId = 1, Active = true,
        //        StartDate = DateTime.Today,
        //        EndDate = null,
        //        Email = "aharris@wvup.edu", Password = "123"
        //    },
        //};
        //public static IList<StatusCode> GetStatusCodes() => new List<StatusCode>
        //{
        //    new StatusCode()
        //    {
        //        //Id = 1,
        //        Description = "Waiting for Supervisor Approval"
        //    },
        //    new StatusCode()
        //    {
        //        //Id = 2,
        //        Description = "Waiting for CFO Approval"
        //    },
        //    new StatusCode()
        //    {
        //        //Id = 3,
        //        Description = "Waiting to be Ordered"
        //    },
        //    new StatusCode()
        //    {
        //        //Id = 4,
        //        Description = "Ordered, waiting on Delivery"
        //    },
        //    new StatusCode()
        //    {
        //        //Id = 5,
        //        Description = "Completed"
        //    },
        //    new StatusCode()
        //    {
        //        //Id = 6,
        //        Description = "Denied"
        //    },
        //    new StatusCode()
        //    {
        //        //Id = 7,
        //        Description = "CFO Denied"
        //    },
        //};
        //public static IList<Category> GetCategories() => new List<Category>
        //{
        //    new Category()
        //    {
        //        //Id = 1,
        //        Name = "Books and Periodicals",
        //        Code = "5010902"
        //    },
        //    new Category()
        //    {
        //        //Id = 2,
        //        Name = "Computer Supplies",
        //        Code = "5010903"
        //    },
        //    new Category()
        //    {
        //        //Id = 3,
        //        Name = "Computer Software - Less than $5,000",
        //        Code = "5011105"
        //    },
        //    new Category()
        //    {
        //        //Id = 4,
        //        Name = "Student Activities",
        //        Code = "5011502"
        //    },
        //    new Category()
        //    {
        //        //Id = 5,
        //        Name = "Office Expenses",
        //        Code = "5013001"
        //    },
        //    new Category()
        //    {
        //        //Id = 6,
        //        Name = "Other General Expenses",
        //        Code = "5013002"
        //    },
        //    new Category()
        //    {
        //        //Id = 7,
        //        Name = "Postage and Freight",
        //        Code = "5014301"
        //    },
        //};
        //public static IList<Requisition> GetRequisitions() => new List<Requisition>
        //{
        //    new Requisition()
        //    {
        //        //Id = 1, 
        //        RequesterId = 1,
        //        Date = DateTime.Today,
        //        StatusCodeId = 1,
        //        JustificationForRequest = "Needed for academic operations.",
        //        TotalPrice = 30.0, ActualPrice = 0,
        //        CategoryId = 1,

        //    },
        //    new Requisition()
        //    {
        //        //Id = 2, 
        //        RequesterId = 2,
        //        Date = DateTime.Today,
        //        StatusCodeId = 1,
        //        JustificationForRequest = "Needed for academic operations.",
        //        TotalPrice = 20.0, ActualPrice = 0,
        //        CategoryId = 2,

        //    },
        //    new Requisition()
        //    {
        //        //Id = 3, 
        //        RequesterId = 2,
        //        Date = DateTime.Today,
        //        StatusCodeId = 1,
        //        JustificationForRequest = "Needed for academic operations.",
        //        TotalPrice = 4000.0, ActualPrice = 0,
        //        CategoryId = 2,

        //    }
        //};
        //public static IList<OrderItem> GetOrderItems() => new List<OrderItem>
        //{
        //    new OrderItem()
        //    {
        //        Quantity = 1, TotalPrice = 15.0,
        //        JustificationForItem = "Best Beret",
        //        ItemId = 1, RequisitionId = 1
        //    },
        //    new OrderItem()
        //    {
        //        Quantity = 1, TotalPrice = 15.0,
        //        JustificationForItem = "Beret2",
        //        ItemId = 2, RequisitionId = 1
        //    },
        //    new OrderItem()
        //    {
        //        Quantity = 2, TotalPrice = 20.0,
        //        JustificationForItem = "It's good.",
        //        ItemId = 3, RequisitionId = 2
        //    },
        //    new OrderItem()
        //    {
        //        Quantity = 1, TotalPrice = 4000.0,
        //        JustificationForItem = "I can't work without this",
        //        ItemId = 4, RequisitionId = 3
        //    },
        //};
        //public static IList<Vendor> GetVendors() => new List<Vendor>
        //{
        //    new Vendor()
        //    {
        //        //Id = 1,
        //        Name = "Gary's Side Hustle",
        //        Phone = "304-424-8000",
        //        Url = "www.beretshop.com",
        //        AddressId = 1
        //    },
        //    new Vendor()
        //    {
        //        //Id = 1,
        //        Name = "Choco shop",
        //        Phone = "304-424-8000",
        //        Url = "www.chocoshop.com",
        //        AddressId = 2
        //    },
        //};
        //public static IList<Item> GetItems() => new List<Item>
        //{
        //    new Item()
        //    {
        //        //Id = 1,
        //        Name = "Beret", Description = "Best Beret",
        //        Price = 15.0,
        //        VendorId = 1
        //    },
        //    new Item()
        //    {
        //        //Id = 2,
        //        Name = "Beret2", Description = "Good Beret",
        //        Price = 15.0,
        //        VendorId = 1
        //    },
        //    new Item()
        //    {
        //        Name = "Chocolate", Description = "Best Chocolate",
        //        Price = 20.0,
        //        VendorId = 2
        //    },
        //    new Item()
        //    {
        //        Name = "Premium Beret", Description = "Premium Beret",
        //        Price = 4000.0,
        //        VendorId = 1
        //    },
        //};
    }
}