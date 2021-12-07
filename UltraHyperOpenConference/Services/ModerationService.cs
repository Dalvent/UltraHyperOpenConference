using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using UltraHyperOpenConference.Enums;
using UltraHyperOpenConference.Exceptions;
using UltraHyperOpenConference.Model;
using UltraHyperOpenConference.Services.Repositories;

namespace UltraHyperOpenConference.Services
{
    public class ModerationService : IModerationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IBanUserCapabilityRepository _banUserCapabilityRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMessageRepository _messageRepository;

        public ModerationService(IUserRepository userRepository,
            IBanUserCapabilityRepository banUserCapabilityRepository,
            ICurrentUserService currentUserService,
            IMessageRepository messageRepository)
        {
            _userRepository = userRepository;
            _banUserCapabilityRepository = banUserCapabilityRepository;
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

        public async Task BanUserAsync(UserCapabilityBanType capabilityBanType, int userId, TimeSpan duration, string reason)
        {
            ThrowIfNotModer();

            var ban = new BanUserCapability()
            {
                CreationDate = DateTime.Now,
                Duration = duration.Seconds,
                ModeratorId = _currentUserService.GetId(),
                Reason = reason,
                UserId = userId,
                UserCapability = (int)capabilityBanType
            };
            
            await _banUserCapabilityRepository.InsertAsync(ban);
        }

        public async Task DeleteMessageAsync(int messageId)
        {
            ThrowIfNotModer();

            var message = await _messageRepository.GetByIdAsync(messageId);
            message.IsDeleted = true;
            await _messageRepository.UpdateAsync(message);
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