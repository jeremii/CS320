using System;
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


        public IEnumerable<User> GetAll()
        {
            IEnumerable<User> results = Table.OrderBy(x => x.LastName);
            return results;
        }
        public IEnumerable<UserFollowViewModel> GetAll(string myId)
        {

            IEnumerable<User> results = Table.OrderBy(x => x.LastName);
            List<UserFollowViewModel> users = new List<UserFollowViewModel>();

            foreach (User user in results)
            {
                users.Add(GetUser2(myId, user.Id).Result);
            }
            return users;
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
                FollowingCount = follows.Count(),
                Bio = user.Bio,
                PhotoPath = user.PicturePath
            };
        }

        public IEnumerable<Post> GetUserPosts(string id)
        {
            return Db.Set<Post>().Where(e => e.UserId == id);
        }

        public IEnumerable<UserFollowViewModel> FindUsers(string userId, string keyword)
        {
            IEnumerable<User> results = Table
                .Where(e => 
                       e.FirstName.ToLower().Contains(keyword.ToLower()) || 
                       e.LastName.ToLower().Contains(keyword.ToLower()));

            List<UserFollowViewModel> users = new List<UserFollowViewModel>();

            foreach (User user in results) users.Add(GetUser2(userId, user.Id).Result);

            return users;
        }


        public async Task<User> GetUserModel(string id)
        {
            return await Table.FindAsync(id);
        }


        public UserOverviewViewModel GetUser(string id)
        {

            var user = Table.Include(e=>e.Posts).Include(e => e.Follows).Include(e => e.Followers)
                .First(x => x.Id == id);

            return GetOne(user, user.Posts, user.Follows, user.Followers);
        }
        public async Task<UserFollowViewModel> GetUser2(string userId, string id)
        {
            UserOverviewViewModel foundUser = GetUser(id);
            FollowRepo followRepo = new FollowRepo();
            UserFollowViewModel item = new UserFollowViewModel()
            {
                FullName = foundUser.FullName,
                UserId = foundUser.UserId,
                isFollowing = await followRepo.IsFollowingAsync(userId, id),
                isFollowingBack = await followRepo.IsFollowingAsync(id, userId)
            };
            return item;
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