using System;
using System.Threading.Tasks;
using UltraHyperOpenConference.Model;

namespace UltraHyperOpenConference.Services
{
    public interface IModerationService
    {
        Task ApproveUserAsync(int id);
        Task BanUserAsync(int userId, long totalSeconds, string reason);
        Task Unban(int banId);
        Task<Message> DeleteMessageAsync(int messageId);
    }
}