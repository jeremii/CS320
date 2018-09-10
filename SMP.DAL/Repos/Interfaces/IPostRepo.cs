using System;
using SMP.DAL.Repos.Base;
using SMP.Models.Entities;
//using SMP.Models.ViewModels.Base;
using System.Collections.Generic;

namespace SMP.DAL.Repos.Interfaces
{
    public interface IPostRepo : IRepo<Post>
    {
        IEnumerable<Post> GetPostsOfUser(int id);
    }
}
