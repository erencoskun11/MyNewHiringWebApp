using MyNewHiringWebApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Application.Interface
{
    public interface ITestSubmissionRepository : IRepository<TestSubmission>

    {
        Task<TestSubmission?> GetWithAnswesAsync(int id, CancellationToken ct = default);
        Task<IReadOnlyList<TestSubmission>> GetByCandidateAsync(int candidateId, CancellationToken ct = default);
    }
}
