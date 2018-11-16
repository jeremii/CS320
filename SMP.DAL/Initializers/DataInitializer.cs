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
            DeleteRowsFromTable(context, "SMP", "Rss");
            DeleteRowsFromTable(context, "SMP", "Messages");
            DeleteRowsFromTable(context, "dbo", "AspNetUsers");
            
        }
        public static void ResetAllIdentities (Context context)
        {
            ResetIdentity(context, "SMP", "Follows");
            ResetIdentity(context, "SMP", "Posts");
            ResetIdentity(context, "SMP", "Rss");
            ResetIdentity(context, "SMP", "Messages");
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
                if (!context.Rss.Any())
                {
                    context.Rss.AddRange(SampleData.GetRss(users));
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
                if(!context.Message.Any())
                {
                    context.Message.AddRange(SampleData.MakeThread(users.Where(x => x.FirstName == "Jeremi").First(), users.Where(x => x.FirstName == "Brady").First()));
                    context.Message.AddRange(SampleData.MakeThread(users.Where(x => x.FirstName == "Jeremi").First(), users.Where(x => x.FirstName == "Ethan").First()));
                    context.Message.AddRange(SampleData.MakeThread(users.Where(x => x.FirstName == "Brady").First(), users.Where(x => x.FirstName == "Ethan").First()));
                    context.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}