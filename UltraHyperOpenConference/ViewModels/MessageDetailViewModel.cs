using System.Collections.Generic;
using UltraHyperOpenConference.Model;

namespace UltraHyperOpenConference.ViewModels
{
    public class MessageDetailViewModel
    {
        public MessageWithUserName Message { get; set; }
        public MessageWithUserName ParentMessage { get; set; }
        public List<MessageWithUserName> Answers { get; set; }
    }
}