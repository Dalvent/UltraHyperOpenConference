namespace UltraHyperOpenConference.Model
{
    public class MessageWithUserName
    {
        public MessageWithUserName(string userName, Message message)
        {
            UserName = userName;
            Message = message;
        }
        
        public string UserName { get; set; }
        public Message Message { get; set; }
    }
}