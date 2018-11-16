using System;
namespace SMP.Models.ViewModels
{
    public class MessageViewModel
    {
        public int Id { get; set; }

        public string SenderId { get; set; }
        public string SenderName { get; set; }

        public string ReceiverId { get; set; }
        public string ReceiverName { get; set; }

        public string Text { get; set; }

        public DateTime Time { get; set; }
    }
}
