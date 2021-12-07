using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltraHyperOpenConference.Model;
using UltraHyperOpenConference.Services.Repositories;
using UltraHyperOpenConference.ViewModels;

namespace UltraHyperOpenConference.Services
{
    public class ThemeMessageTreeService : IThemeMessageTreeService
    {
        private readonly IMessageRepository _messageRepository;

        public ThemeMessageTreeService(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }
        
        public async Task<ThemeMessageTreeLeaf> GetTreeAsync(int themeId)
        {
            var messagesGroupedByAnswer = (await _messageRepository.GetByThemeAsync(themeId))
                .GroupBy(mesage => mesage.Message.ParentMessageId)
                .ToDictionary(
                    item => item.Key ?? -1,
                    item => (IEnumerable<MessageWithUserName>)item
                );
            
            MessageWithUserName root = messagesGroupedByAnswer[-1].FirstOrDefault();
            return CreateMessageTreeByAnswer(root, messagesGroupedByAnswer);
        }

        private ThemeMessageTreeLeaf CreateMessageTreeByAnswer(MessageWithUserName root, Dictionary<int, IEnumerable<MessageWithUserName>> messagesGroupedByAnswer)
        {
            ThemeMessageTreeLeaf themeMessageTreeLeaf = new()
            {
                MessageWithAuthorName = root
            };
            if (messagesGroupedByAnswer.TryGetValue(root.Message.Id, out IEnumerable<MessageWithUserName> childMessages))
            {
                themeMessageTreeLeaf.Answers = childMessages
                    .Select(item => CreateMessageTreeByAnswer(item, messagesGroupedByAnswer))
                    .ToList();
            }
            
            return themeMessageTreeLeaf;
        }
    }
}