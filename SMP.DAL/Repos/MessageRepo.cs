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

        public IEnumerable<MessageViewModel> GetThread(string userId, string oppositeUserId)
        {
            return Table.Include(x => x.Sender).Include(x => x.Receiver)
                        .Where(x => (x.SenderId == userId && x.ReceiverId == oppositeUserId) || (x.SenderId == oppositeUserId && x.ReceiverId == userId))
                        .OrderBy(x => x.Id)
                        .Select(item => GetMessage(item));
        }
        public IEnumerable<MessageInboxViewModel> GetInbox(string userId)
        {
            return Table.Include(x => x.Sender).Include(x => x.Receiver)
                        .Where(x => x.ReceiverId == userId)
                        .GroupBy(x => x.SenderId)
                        .Select(x => x.OrderBy(sender => sender.Id).LastOrDefault())
                        .Select( item => GetMessageInbox( item, item.ReceiverId == userId ? item.Sender : item.Receiver));
        }
        public MessageViewModel GetMessage(Message message)
        {
            return new MessageViewModel()
            {
                Id = message.Id,
                SenderName = message.Sender.FirstName + " " + message.Sender.LastName,
                SenderId = message.SenderId,
                ReceiverName = message.Receiver.FirstName + " " + message.Receiver.LastName,
                ReceiverId = message.ReceiverId,
                Text = message.Text,
                Time = message.Time
            };
        }

        public MessageInboxViewModel GetMessageInbox(Message lastMsg, User oppositeUser)
        {
            return new MessageInboxViewModel()
             {
                 UserId = oppositeUser.Id,
                 UserFullName = oppositeUser.FirstName + " " + oppositeUser.LastName,
                 LastMessage = lastMsg.Text,
                 Time = lastMsg.Time
             };
        }
    }
}