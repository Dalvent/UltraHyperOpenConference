using System.Collections.Generic;
using UltraHyperOpenConference.Model;

namespace UltraHyperOpenConference.ViewModels
{
    public class SearchMessageByKeyword
    {
        public string Keyword { get; set; }
        public List<MessageWithUserName> Result { get; set; }
    }
}