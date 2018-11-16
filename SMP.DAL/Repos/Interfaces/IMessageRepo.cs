using System;
using SMP.DAL.Repos.Base;
using SMP.Models.Entities;
using SMP.Models.ViewModels;
using System.Collections.Generic;

namespace SMP.DAL.Repos.Interfaces
{
    public interface IMessageRepo : IRepo<Message>
    {

        IList<Message> GetThread(string userId, string oppositeUserId);
        IList<Message> GetInbox(string userId);

    }
}
