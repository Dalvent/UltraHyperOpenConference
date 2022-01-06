using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using UltraHyperOpenConference.Exceptions;
using UltraHyperOpenConference.Model;
using UltraHyperOpenConference.Services.Repositories;

namespace UltraHyperOpenConference.Services
{
    public class ModerationService : IModerationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IBanUserRepository _banUserRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMessageRepository _messageRepository;

        public ModerationService(IUserRepository userRepository,
            IBanUserRepository banUserRepository,
            ICurrentUserService currentUserService,
            IMessageRepository messageRepository)
        {
            _userRepository = userRepository;
            _banUserRepository = banUserRepository;
            _currentUserService = currentUserService;
            _messageRepository = messageRepository;
        }
        
        public async Task ApproveUserAsync(int id)
        {
            ThrowIfNotModer();
            
            User user = await _userRepository.GetByIdAsync(id);
            user.IsActive = true;
            await _userRepository.UpdateAsync(user);
        }

        public async Task BanUserAsync(int userId, int hours, string reason)
        {
            ThrowIfNotModer();

            var ban = new BanUser()
            {
                CreationDate = DateTime.Now,
                Duration = hours,
                ModeratorId = _currentUserService.GetId(),
                Reason = reason,
                UserId = userId
            };
            
            await _banUserRepository.InsertAsync(ban);
        }

        public async Task<Message> DeleteMessageAsync(int messageId)
        {
            ThrowIfNotModer();

            var message = await _messageRepository.GetByIdAsync(messageId);
            message.IsDeleted = true;
            await _messageRepository.UpdateAsync(message);
            return message;
        }

        private void ThrowIfNotModer()
        {
            if (!_currentUserService.IsModer())
            {
                throw new UserIsNotModerException();
            }
        }
    }
}