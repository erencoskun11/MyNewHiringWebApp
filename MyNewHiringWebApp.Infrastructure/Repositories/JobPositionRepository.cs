using MyNewHiringWebApp.Domain.Entities;
using MyNewHiringWebApp.Infrastructure.Data;
using MyNewHiringWebApp.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Infrastructure.Repositories
{
    public class JobPositionRepository : BaseRepository<JobPosition>, IJobPositionRepository
    {
        public JobPositionRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
