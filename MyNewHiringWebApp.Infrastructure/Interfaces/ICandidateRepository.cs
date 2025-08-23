using MyNewHiringWebApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Infrastructure.Interfaces
{
    public interface ICandidateRepository : IRepository<Candidate>
    {
        Task<Candidate?> GetByEmailAsync(string email, CancellationToken ct = default);
        Task<Candidate?> GetWithSkillsAsync(int id, CancellationToken ct = default);
    }
}
