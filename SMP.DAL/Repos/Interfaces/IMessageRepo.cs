using System;
using SMP.DAL.Repos.Base;
using SMP.Models.Entities;
using SMP.Models.ViewModels;
using System.Collections.Generic;

namespace SMP.DAL.Repos.Interfaces
{
    public interface IMessageRepo : IRepo<Message>
    {

        IEnumerable<MessageViewModel> GetThread(string userId, string oppositeUserId);
        IEnumerable<MessageInboxViewModel> GetInbox(string userId);
        MessageViewModel GetMessage(Message message);
        MessageInboxViewModel GetMessageInbox(Message lastMsg, User oppositeUser);

    }
}
