using Microsoft.EntityFrameworkCore;
using MyNewHiringWebApp.Application.Interface;
using MyNewHiringWebApp.Domain.Entities;
using MyNewHiringWebApp.Infrastructure.Data;

namespace MyNewHiringWebApp.Infrastructure.Repositories
{
    public class SubmittedAnswerRepository : BaseRepository<SubmittedAnswer>,ISubmittedAnswerRepository
    {
        public SubmittedAnswerRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IReadOnlyList<SubmittedAnswer>> GetBySubmissionIdAsync(int submissionId, CancellationToken ct = default)
        {
            return await _dbSet.Where(c => c.TestSubmissionId==submissionId).Include(c => c.TestSubmission).ToListAsync(ct);
        }
    }
}
