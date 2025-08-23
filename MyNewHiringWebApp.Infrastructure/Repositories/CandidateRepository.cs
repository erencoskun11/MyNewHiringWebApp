using Microsoft.EntityFrameworkCore;
using MyNewHiringWebApp.Application.Interface;
using MyNewHiringWebApp.Domain.Entities;
using MyNewHiringWebApp.Infrastructure.Data;

namespace MyNewHiringWebApp.Infrastructure.Repositories
{
    internal class CandidateRepository : BaseRepository<Candidate>, ICandidateRepository
    {
        public CandidateRepository(ApplicationDbContext db) : base(db)
        {
        }

        public async Task<Candidate?> GetByEmailAsync(string email, CancellationToken ct = default)
        {
            return await _dbSet.FirstOrDefaultAsync(c => c.Email == email, ct);

        }

        public async Task<Candidate?> GetWithSkillsAsync(int id, CancellationToken ct = default)
        {
            return await _dbSet.Include(c => c.CandidateSkills).FirstOrDefaultAsync(c=>c.Id == id, ct);
        }
    }
}
