using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SMP.DAL.EF;
using SMP.DAL.Repos.Base;
using SMP.Models.Entities;
using SMP.DAL.Repos.Interfaces;
using SMP.Models.ViewModels;

namespace SMP.DAL.Repos
{
    public class PostRepo : RepoBase<Post>, IPostRepo
    {
        public PostRepo(DbContextOptions<Context> options) : base(options)
        {
        }
        public PostRepo()
        {
        }
        public override IEnumerable<Post> GetAll()
            => Table.OrderBy(x => x.Id);

        public override IEnumerable<Post> GetRange(int skip, int take)
            => GetRange(Table.OrderBy(x => x.Id), skip, take);
        public IEnumerable<Post> GetPostsOfUser(int id)
            => Table
            .Where(p =>
                  p.UserId.Equals(id))
            .OrderBy(x => x.Time);
        
        public UserPostViewModel GetUserPost(Post post, User user)
        {
            return new UserPostViewModel()
            {
                FullName = user.FirstName + " " + user.LastName,
                UserId = post.UserId,
                PostId = post.Id,
                Text = post.Text,
                Time = post.Time
            };
        }

        public IEnumerable<UserPostViewModel> GetFollowingPosts(string id)
        {
            return from posts in Context.Post
                   join follows in Context.Follow
                   on posts.UserId equals follows.FollowerId
                   where follows.UserId == id
                   select GetUserPost(posts, posts.User);
        }

    }
}