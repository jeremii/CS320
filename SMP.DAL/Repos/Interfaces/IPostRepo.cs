using System;
using SMP.DAL.Repos.Base;
using SMP.Models.Entities;
using SMP.Models.ViewModels;
using System.Collections.Generic;

namespace SMP.DAL.Repos.Interfaces
{
    public interface IPostRepo : IRepo<Post>
    {
        IEnumerable<Post> GetPostsOfUser(string id);
        //IEnumerable<UserPostViewModel> GetFollowingPosts(string followerId);
        UserPostViewModel GetUserPost(Post post, User user);
        IEnumerable<UserPostViewModel> GetFollowingPosts(string id);
    }
}
