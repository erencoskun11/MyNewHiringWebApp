using MyNewHiringWebApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Application.Interface
{
    public interface ITestQuestionRepository : IRepository<TestQuestion>
    {
        Task<IReadOnlyList<TestQuestion>> GetByTestIdAsync(int testId, CancellationToken ct = default);

    }
}
