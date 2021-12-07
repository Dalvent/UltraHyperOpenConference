using System.Threading.Tasks;
using UltraHyperOpenConference.Model;
using UltraHyperOpenConference.ViewModels;

namespace UltraHyperOpenConference.Services
{
    public interface IConferenceService
    {
        Task<Theme> StartThemeAsync(string name, string startMessage);
        Task AnswerToAsync(int parentMessageId, string answer);
    }
}