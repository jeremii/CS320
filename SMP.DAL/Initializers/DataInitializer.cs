using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SMP.DAL.EF;
using SMP.Models.Entities;
using System.Collections.Generic;

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
            DeleteRowsFromTable(context, "SMP", "Follows");
            DeleteRowsFromTable(context, "SMP", "Posts");
            DeleteRowsFromTable(context, "dbo", "AspNetUsers");
            
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

        public static void ResetIdentity(Context context)
        {

        }

        public static void ResetIdentity(Context context, string schemaName, string tableName )
        {
            var sql = $"DBCC CHECKIDENT (\"{schemaName}.{tableName}\", RESEED, 0);";
            context.Database.ExecuteSqlCommand(sql);
        }

        public static void SeedData(Context context)
        {
            context.Database.EnsureCreated();

            try
            {
                List<User> users = (List<User>)SampleData.GetUsers();
                if (!context.User.Any())
                {
                    context.User.AddRange(users);
                    context.SaveChanges();
                }
                if (!context.Follow.Any())
                {
                    context.Follow.AddRange(SampleData.GetFollows(users));
                    context.SaveChanges();
                }
                if (!context.Post.Any())
                {
                    context.Post.AddRange(SampleData.GetPosts(users[0]));
                    context.SaveChanges();
                    context.Post.AddRange(SampleData.GetPosts(users[1]));
                    context.SaveChanges();
                    context.Post.AddRange(SampleData.GetPosts(users[2]));
                    context.SaveChanges();
                }
                //if (!context.Posts.Any())
                //{
                //    // context.Posts.AddRange(SampleData.GetPosts(context.Users.ToList()));
                //    // context.SaveChanges();
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