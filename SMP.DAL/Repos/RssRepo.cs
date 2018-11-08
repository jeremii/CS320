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
    public class RssRepo : RepoBase<Rss>, IRssRepo
    {
        public RssRepo(DbContextOptions<Context> options) : base(options)
        {
        }
        public RssRepo()
        {
        }
        public override IEnumerable<Rss> GetAll()
            => Table.OrderBy(x => x.Id);

        public override IEnumerable<Rss> GetRange(int skip, int take)
            => GetRange(Table.OrderBy(x => x.Id), skip, take);

        public IEnumerable<Rss> GetRssOfUser(string userId)
            => Table.Where(x => x.UserId == userId);
    }
}