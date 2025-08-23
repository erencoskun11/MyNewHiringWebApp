using MyNewHiringWebApp.Domain.Entities;
using MyNewHiringWebApp.Infrastructure.Data;
using MyNewHiringWebApp.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Infrastructure.Repositories
{
    public class InterviewerRepository : BaseRepository<Interviewer>, IInterviewerRepository
    {
        public InterviewerRepository(ApplicationDbContext db) : base(db)
        {
        }

        
    }
}
