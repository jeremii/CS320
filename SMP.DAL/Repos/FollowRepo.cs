﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SMP.DAL.EF;
using SMP.DAL.Repos.Base;
using SMP.Models.Entities;
using SMP.DAL.Repos.Interfaces;
using SMP.Models.ViewModels;
using System.Threading.Tasks;

namespace SMP.DAL.Repos
{
    public class FollowRepo : RepoBase<Follow>, IFollowRepo
    {
        public FollowRepo(DbContextOptions<Context> options) : base(options)
        {
        }
        public FollowRepo()
        {
        }
        public override IEnumerable<Follow> GetAll()
            => Table.OrderBy(x => x.Id);

        public override IEnumerable<Follow> GetRange(int skip, int take)
            => GetRange(Table.OrderBy(x => x.Id), skip, take);

        public async Task<UserFollowViewModel> GetUserFollow( bool followers, Follow follow, User user, string myId )
        {
            if (followers)
            {
                return new UserFollowViewModel()
                {
                    FullName = user.FirstName + " " + user.LastName,
                    UserId = user.Id,
                    isFollowing = await IsFollowingAsync( user.Id, myId),
                    isFollowingBack = await IsFollowingAsync( myId, user.Id)
                };
            }
            else
            {
                return new UserFollowViewModel()
                {
                    FullName = follow.Follower.FirstName + " " + follow.Follower.LastName,
                    UserId = follow.Follower.Id,
                    isFollowing = await IsFollowingAsync( myId, follow.FollowerId),
                    isFollowingBack = await IsFollowingAsync( follow.FollowerId, myId)
                };
            }
        }
        public async Task<bool> IsFollowingAsync(string userId, string followerId )
        {
            return await Table.AnyAsync(x => x.UserId == userId && x.FollowerId == followerId);
        }
        public IEnumerable<UserFollowViewModel> GetFollowers( string id, string myId )
        {
            return GetFollowersOfUser(id, myId).Result;
        }
        public IEnumerable<UserFollowViewModel> GetFollowing( string id, string myId )
        {
            return GetWhoUserIsFollowing(id, myId).Result;
        }
        public async Task<IEnumerable<UserFollowViewModel>> GetFollowersOfUser(string id, string myId)
        {
            var result = await Table
                    .Include(e => e.User)
                    .Where(x => x.FollowerId == id )
                    .ToListAsync();
            return result.Select( async item => await GetUserFollow(true, item, item.User, myId))
                        .Select(t => t.Result);
            //IEnumerable<Follow> result = Table
            //        .Include(e => e.User)
            //        .Where(x => x.FollowerId == id);
            //IEnumerable<Task<UserFollowViewModel>> task1 = result
            //        .Select(s => GetUserFollow(true, s, s.User));
            //Task<UserFollowViewModel[]> task2 = Task.WhenAll(task1);
            //IEnumerable<UserFollowViewModel> result2 = await task2;
            //return result2;
        }

        public async Task<IEnumerable<UserFollowViewModel>> GetWhoUserIsFollowing(string id, string myId )
        {
            var result = await Table
                    .Include(e => e.Follower)
                    .Include(e => e.User)
                    .Where(x => x.UserId == id )
                    .ToListAsync();
            return result.Select(async item => await GetUserFollow(false, item, item.User, myId))
                    .Select(t => t.Result);
            //IEnumerable<Follow> result = Table
            //        .Include(e => e.Follower)
            //        .Include(e => e.User)
            //        .Where(x => x.UserId == id);
            //IEnumerable<Task<UserFollowViewModel>> task1 = result
            //        .Select(s => GetUserFollow(false, s, s.User));
            //Task<UserFollowViewModel[]> task2 = Task.WhenAll(task1);
            //IEnumerable<UserFollowViewModel> result2 = await task2;
            //return result2;
        }
        public Follow GetOne(string userId, string followId)
        {
            return Table
                .Where(x => x.UserId == userId && x.FollowerId == followId)
                .FirstOrDefault();
        }

    }
}