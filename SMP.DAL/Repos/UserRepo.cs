﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SMP.DAL.EF;
using SMP.DAL.Repos.Base;
using SMP.Models.Entities;
using SMP.DAL.Repos.Interfaces;
using SMP.Models.ViewModels;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;

namespace SMP.DAL.Repos
{
    public class UserRepo : IUserRepo
    {
        public readonly Context Db;
        public DbSet<User> Table { get; }
        //public Context Context => Db;

        public UserRepo()
        {
            Db = new Context();
            Table = Db.Set<User>();
        }


        protected UserRepo(DbContextOptions<Context> options)
        {
            Db = new Context(options);
            Table = Db.Set<User>();
        }


        private bool _disposed = false;


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
            {
                //Free any other managed objects here
            }
            Db.Dispose();
            _disposed = true;
        }


        public int SaveChanges()
        {
            try
            {
                return Db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }


        public IEnumerable<UserOverviewViewModel> GetAll()
        {
            
            return Table
                .Include(x => x.Posts)
                .Include(x => x.Follows)
                .Include(x => x.Followers)
                .Skip(0).Take(10)
                .OrderBy(x => x.LastName)
                .Select(e => GetOne(e, e.Posts, e.Follows, e.Followers));
        }


        public IEnumerable<UserOverviewViewModel> GetRange(int skip = 0, int take = 10)
        {
            
            return Table.Include(e => e.Follows).Include(e => e.Followers)
                        .Skip(skip).Take(take)
                        .OrderBy(x => x.LastName)
                        .Select(item => GetOne(item, item.Posts, item.Follows, item.Followers));
        }


        public UserOverviewViewModel GetOne(User user, IEnumerable<Post> posts, IEnumerable<Follow> follows, IEnumerable<Follow> followers)
        {
            //IEnumerable<Post> posts = GetUserPosts(user.Id);
            //IEnumerable<Follow> follows = Db.Set<Follow>().Where(e => e.UserId == user.Id);
            //IEnumerable<Follow> followers = Db.Set<Follow>().Where(e => e.FollowerId == user.Id);
            return new UserOverviewViewModel()
            {
                FullName = user.FirstName + " " + user.LastName,
                UserId = user.Id,
                PostCount = posts.Count(),
                FollowerCount = followers.Count(),
                FollowingCount = follows.Count()
            };
        }


        public IEnumerable<Post> GetUserPosts(string id)
        {
            return Db.Set<Post>().Where(e => e.UserId == id);
        }

        public IEnumerable<UserOverviewViewModel> FindUsers(string keyword)
        {
            var results = Table
                .Where(e => e.FirstName.ToLower().Contains(keyword.ToLower()) || e.LastName.ToLower().Contains(keyword.ToLower()));

            List<UserOverviewViewModel> returnProfiles = new List<UserOverviewViewModel>();
            foreach (User user in results)
            {
                returnProfiles.Add(GetUser(user.Id));
            }
            return returnProfiles;
        }


        public async Task<User> GetUserModel(string id)
        {
            return await Table.FindAsync(id);
        }


        public UserOverviewViewModel GetUser(string id)
        {
            User user = Table.Include(e => e.Follows).Include(e => e.Followers).Include(e => e.Posts)
                .First(x => x.Id == id);

            return GetOne(user, user.Posts, user.Follows, user.Followers);
        }


        public int Update(User user, bool persist = true )
        {
            //user.ConcurrencyStamp = System.Guid.NewGuid().ToString();
            user.ConcurrencyStamp = Table.AsNoTracking()
                .Where(w => w.Id == user.Id)
                .FirstOrDefault().ConcurrencyStamp;
            Table.Update(user);
            return persist ? SaveChanges() : 0;
        }

    }
}