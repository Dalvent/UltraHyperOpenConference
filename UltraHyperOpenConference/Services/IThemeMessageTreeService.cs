using System.Threading.Tasks;
using UltraHyperOpenConference.ViewModels;

namespace UltraHyperOpenConference.Services
{
    public interface IThemeMessageTreeService
    {
        Task<ThemeMessageTreeLeaf> GetTreeAsync(int themeId);
    }
}