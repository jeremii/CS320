using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SMP.DAL.EF;

namespace SMP.DAL.Initializers
{
    public static class DataInitializer
    {
        public static void InitializeData(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<Context>();
            InitializeData(context);
        }
        public static void InitializeData(Context context)
        {
            context.Database.Migrate();
            setTableToNull(context);
            ClearData(context);
            SeedData(context);
        }
        public static void setTableToNull(Context context)
        {
            var sql = $"Update SMP.Follow set UserId = NULL";
            context.Database.ExecuteSqlCommand(sql);
        }
        public static void ClearData(Context context)
        {
            ExecuteDeleteSQL(context, "Follow");
            ExecuteDeleteSQL(context, "Post");
            ExecuteDeleteSQL(context, "User");
            ////ExecuteDeleteSQL(context, "FileAttachment");
            //ExecuteDeleteSQL(context, "Employee");


            //ExecuteDeleteSQL(context, "EmployeeGroup");
            //ExecuteDeleteSQL(context, "Campus");
            //ExecuteDeleteSQL(context, "College");
            //ExecuteDeleteSQL(context, "Address");

            //ExecuteDeleteSQL(context, "Budget");

            //ExecuteDeleteSQL(context, "OrderItem");

            //ExecuteDeleteSQL(context, "Category");
            //ExecuteDeleteSQL(context, "Vendor");
            //ExecuteDeleteSQL(context, "Item");
            //ExecuteDeleteSQL(context, "StatusCode");
            //ExecuteDeleteSQL(context, "Approver");

            ResetIdentity(context);
        }
        public static void ExecuteDeleteSQL(Context context, string tableName)
        {
            var sql = $"Delete from SMP.{tableName}";
            context.Database.ExecuteSqlCommand(sql);
        }
        public static void ResetIdentity(Context context)
        {
            var tables = new[] {"Follow", "Post", "User"};
            foreach (var itm in tables)
            {
                var sql = $"DBCC CHECKIDENT (\"SMP.{itm}\", RESEED, 0);";
                context.Database.ExecuteSqlCommand(sql);
            }
        }

        public static void SeedData(Context context)
        {
            try
            {
                //if (!context.Address.Any())
                //{
                //    context.Address.AddRange(StoreSampleData.GetAddresses());
                //    context.SaveChanges();
                //}
                //if (!context.College.Any())
                //{
                //    context.College.AddRange(StoreSampleData.GetColleges());
                //    context.SaveChanges();
                //}
                //if (!context.Campus.Any())
                //{
                //    context.Campus.AddRange(StoreSampleData.GetCampuses());
                //    context.SaveChanges();
                //}
                //if (!context.EmployeeGroup.Any())
                //{
                //    context.EmployeeGroup.AddRange(StoreSampleData.GetEmployeeGroups());
                //    context.SaveChanges();
                //}
                //if (!context.Employee.Any())
                //{
                //    context.Employee.AddRange(StoreSampleData.GetEmployees());
                //    context.SaveChanges();
                //    context.EmployeeGroup.Single(x => x.Id == 1).HeadId = 8;
                //    context.EmployeeGroup.Single(x => x.Id == 2).HeadId = 3;
                //    context.EmployeeGroup.Single(x => x.Id == 3).HeadId = 4;
                //    context.EmployeeGroup.Single(x => x.Id == 4).HeadId = 5;
                //    //context.EmployeeGroup.Single(x => x.Id == 5).HeadId = 6;
                //    context.EmployeeGroup.Single(x => x.Id == 6).HeadId = 6;
                //    //context.EmployeeGroup.Single(x => x.Id == 7).HeadId = null;
                //    //context.EmployeeGroup.Single(x => x.Id == 8).HeadId = null;
                //    //context.EmployeeGroup.Single(x => x.Id == 9).HeadId = null;
                //    context.EmployeeGroup.Single(x => x.Id == 10).HeadId = 1;
                //    context.EmployeeGroup.Single(x => x.Id == 11).HeadId = 7;
                //    //context.EmployeeGroup.Single(x => x.Id == 12).HeadId = null;
                //    context.SaveChanges();
                //}
                //if (!context.Budget.Any())
                //{
                //    context.Budget.AddRange(StoreSampleData.GetBudgets());
                //    context.SaveChanges();
                //}
                //if (!context.Category.Any())
                //{
                //    context.Category.AddRange(StoreSampleData.GetCategories());
                //    context.SaveChanges();
                //}
                //if (!context.Vendor.Any())
                //{
                //    context.Vendor.AddRange(StoreSampleData.GetVendors());
                //    context.SaveChanges();
                //}
                //if (!context.Item.Any())
                //{
                //    context.Item.AddRange(StoreSampleData.GetItems());
                //    context.SaveChanges();
                //}
                //if (!context.StatusCode.Any())
                //{
                //    context.StatusCode.AddRange(StoreSampleData.GetStatusCodes());
                //    context.SaveChanges();
                //}
                //if (!context.Requisition.Any())
                //{
                //    context.Requisition.AddRange(StoreSampleData.GetRequisitions());
                //    context.SaveChanges();
                //}
                //if (!context.OrderItem.Any())
                //{
                //    context.OrderItem.AddRange(StoreSampleData.GetOrderItems());
                //    context.SaveChanges();
                //}
                //if (!context.Approver.Any())
                //{
                //    context.Approver.AddRange(StoreSampleData.GetApprovers());
                //    context.SaveChanges();
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}