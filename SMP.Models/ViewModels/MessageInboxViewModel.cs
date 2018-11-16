using System;
namespace SMP.Models.ViewModels
{
    public class MessageInboxViewModel
    {
        public string UserId { get; set; }

        public string UserFullName { get; set; }

        public string LastMessage { get; set; }

        public DateTime Time { get; set; }
    }
}
