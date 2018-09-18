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
            ClearData(context);
            ResetAllIdentities(context);
            SeedData(context);
        }
        //public static void setTableToNull(Context context)
        //{
        //    var sql = $"Update SMP.Follow set UserId = NULL";
        //    context.Database.ExecuteSqlCommand(sql);
        //}
        public static void ClearData(Context context)
        {
            DeleteRowsFromTable(context, "dbo", "AspNetUsers");
            DeleteRowsFromTable(context, "SMP", "Follows");
            DeleteRowsFromTable(context, "SMP", "Posts");
        }
        public static void ResetAllIdentities (Context context)
        {
            ResetIdentity(context, "SMP", "Follows");
            ResetIdentity(context, "SMP", "Posts");
        }
        public static void DeleteRowsFromTable(Context context, string schemaName, string tableName)
        {
            var sql = $"Delete from [{schemaName}].[{tableName}]";
            context.Database.ExecuteSqlCommand(sql);
        }
        public static void ResetIdentity(Context context, string schemaName, string tableName )
        {
            var sql = $"DBCC CHECKIDENT (\"{schemaName}.{tableName}\", RESEED, 0);";
            context.Database.ExecuteSqlCommand(sql);
        }

        public static void SeedData(Context context)
        {
            try
            {
                if (!context.User.Any())
                {
                    context.User.AddRange(SampleData.GetUsers());
                    context.SaveChanges();
                }
                //if (!context.Follow.Any())
                //{
                //    context.Follow.AddRange(SampleData.GetFollows());
                //    context.SaveChanges();
                //}
                //if (!context.Post.Any())
                //{
                //    context.Post.AddRange(SampleData.GetPosts());
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

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}