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
    public class MessageRepo : RepoBase<Message>, IMessageRepo
    {
        public MessageRepo(DbContextOptions<Context> options) : base(options)
        {
        }
        public MessageRepo()
        {
        }
        public override IEnumerable<Message> GetAll()
            => Table.OrderBy(x => x.Id);

        public override IEnumerable<Message> GetRange(int skip, int take)
            => GetRange(Table.OrderBy(x => x.Id), skip, take);

        public IList<Message> GetThread(string userId, string oppositeUserId)
        {
            return Table.Include(x => x.Sender).Include(x => x.Receiver)
                        .Where(x => (x.SenderId == userId && x.ReceiverId == oppositeUserId) || (x.SenderId == oppositeUserId && x.ReceiverId == userId))
                        .OrderByDescending(x => x.Id)
                        .ToList();
        }
        public IList<Message> GetInbox(string userId)
        {
            return Table.Include(x => x.Sender).Include(x => x.Receiver)
                        .Where(x => x.ReceiverId == userId)
                        .GroupBy(x => x.SenderId)
                        .Select(x => x.OrderByDescending(sender => sender.Id).FirstOrDefault())
                        .ToList();
        }

    }
}