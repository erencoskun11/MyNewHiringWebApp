using MyNewHiringWebApp.Application.Interface;
using MyNewHiringWebApp.Infrastructure.Data;

namespace MyNewHiringWebApp.Infrastructure.Repositories
{
    public class CandidateSkillRepository : BaseRepository<CandidateSkill>, ICandidateSkillRepository
    {
        public CandidateSkillRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
