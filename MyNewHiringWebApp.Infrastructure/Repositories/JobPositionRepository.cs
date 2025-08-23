using MyNewHiringWebApp.Application.Interface;
using MyNewHiringWebApp.Domain.Entities;
using MyNewHiringWebApp.Infrastructure.Data;

namespace MyNewHiringWebApp.Infrastructure.Repositories
{
    public class JobPositionRepository : BaseRepository<JobPosition>, IJobPositionRepository
    {
        public JobPositionRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
