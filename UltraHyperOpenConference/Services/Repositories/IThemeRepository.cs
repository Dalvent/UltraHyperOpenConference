using System.Threading.Tasks;
using UltraHyperOpenConference.Model;

namespace UltraHyperOpenConference.Services.Repositories
{
    public interface IThemeRepository : IRepository<Theme>
    {
        Task<Theme> GetThemeOfMessage(int messageId);
        Task<Theme> GetThemeFromName(string name);
    }
}