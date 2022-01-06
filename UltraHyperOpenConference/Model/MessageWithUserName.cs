namespace UltraHyperOpenConference.Model
{
    public class MessageWithUserName
    {
        public MessageWithUserName(string userName, Message message, bool isBanned)
        {
            UserName = userName;
            Message = message;
            IsBanned = isBanned;
        }
        
        public string UserName { get; set; }
        public bool IsBanned { get; set; }
        public Message Message { get; set; }
    }
}