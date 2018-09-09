using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SMP.DAL.EF;
using SMP.DAL.Repos.Base;
using SMP.Models.Entities;
using SMP.DAL.Repos.Interfaces;

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
    }
}