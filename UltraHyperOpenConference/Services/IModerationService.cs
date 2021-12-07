using System;
using System.Threading.Tasks;
using UltraHyperOpenConference.Enums;
using UltraHyperOpenConference.Model;

namespace UltraHyperOpenConference.Services
{
    public interface IModerationService
    {
        Task ApproveUserAsync(int id);
        Task BanUserAsync(UserCapabilityBanType capabilityBanType, int userId, TimeSpan duration, string reason);
        Task DeleteMessageAsync(int messageId);
    }
}