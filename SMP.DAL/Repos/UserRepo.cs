using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SMP.DAL.EF;
using SMP.DAL.Repos.Base;
using SMP.Models.Entities;
using SMP.DAL.Repos.Interfaces;

namespace SMP.DAL.Repos
{
    public class UserRepo : RepoBase<User>, IUserRepo
    {
        public UserRepo(DbContextOptions<Context> options) : base(options)
        {
        }
        public UserRepo()
        {
        }
        public override IEnumerable<User> GetAll()
            => Table.OrderBy(x => x.Id);

        public override IEnumerable<User> GetRange(int skip, int take)
            => GetRange(Table.OrderBy(x => x.Id), skip, take);
    }
}