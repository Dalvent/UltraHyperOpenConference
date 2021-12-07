using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UltraHyperOpenConference.Model;

namespace UltraHyperOpenConference.Services.Repositories
{
    public class BanUserCapabilityRepository : Repository<BanUserCapability>, IBanUserCapabilityRepository
    {
        public BanUserCapabilityRepository(WwwConferenceContext dbContext) : base(dbContext)
        {
        }

        public override Task<BanUserCapability> GetByIdAsync(int id)
        {
            return DbSet.FirstOrDefaultAsync(item => item.Id == id);
        }
    }
}