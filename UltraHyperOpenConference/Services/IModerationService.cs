using System;
using System.Threading.Tasks;
using UltraHyperOpenConference.Model;

namespace UltraHyperOpenConference.Services
{
    public interface IModerationService
    {
        Task ApproveUserAsync(int id);
        Task BanUserAsync(int userId, int hours, string reason);
        Task<Message> DeleteMessageAsync(int messageId);
    }
}