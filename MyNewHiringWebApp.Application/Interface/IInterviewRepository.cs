using MyNewHiringWebApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Application.Interface
{
    public interface IInterviewRepository : IRepository<Interview>
    {
        Task<Interview?> GetWithRelationsAsync(int id, CancellationToken ct = default);

    }
}
