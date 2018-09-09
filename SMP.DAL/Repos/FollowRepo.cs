using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SMP.DAL.EF;
using SMP.DAL.Repos.Base;
using SMP.Models.Entities;
using SMP.DAL.Repos.Interfaces;

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
    }
}