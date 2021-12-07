using System;
using System.Collections.Generic;

#nullable disable

namespace UltraHyperOpenConference.Model
{
    public partial class Message
    {
        public Message()
        {
            InverseParentMessage = new HashSet<Message>();
        }

        public int Id { get; set; }
        public int UserAuthorId { get; set; }
        public int ThemeId { get; set; }
        public int? ParentMessageId { get; set; }
        public DateTime CreationDate { get; set; }
        public string Text { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Message ParentMessage { get; set; }
        public virtual Theme Theme { get; set; }
        public virtual User UserAuthor { get; set; }
        public virtual ICollection<Message> InverseParentMessage { get; set; }
    }
}
