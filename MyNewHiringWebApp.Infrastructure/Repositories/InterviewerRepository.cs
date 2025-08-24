using MyNewHiringWebApp.Application.Interface;
using MyNewHiringWebApp.Domain.Entities;
using MyNewHiringWebApp.Infrastructure.Data;

namespace MyNewHiringWebApp.Infrastructure.Repositories
{
    public class InterviewerRepository : BaseRepository<Interviewer>, IInterviewerRepository
    {
        public InterviewerRepository(ApplicationDbContext db) : base(db)
        {
        }

        
    }
}
