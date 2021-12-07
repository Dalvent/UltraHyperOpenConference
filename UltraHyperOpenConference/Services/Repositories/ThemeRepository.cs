using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UltraHyperOpenConference.Model;

namespace UltraHyperOpenConference.Services.Repositories
{
    public class ThemeRepository : Repository<Theme>, IThemeRepository
    {
        public ThemeRepository(WwwConferenceContext dbContext) : base(dbContext)
        {
        }

        public async Task<Theme> GetThemeOfMessage(int messageId)
        {
            return await DbSet.FirstOrDefaultAsync(theme => theme.Messages.Any(message => message.Id == messageId));
        }

        public async Task<Theme> GetThemeFromName(string name)
        {
            return await DbSet.FirstOrDefaultAsync(t => t.Name == name);
        }
        
        public override Task<Theme> GetByIdAsync(int id)
        {
            return DbSet.FirstOrDefaultAsync(item => item.Id == id);
        }
    }
}