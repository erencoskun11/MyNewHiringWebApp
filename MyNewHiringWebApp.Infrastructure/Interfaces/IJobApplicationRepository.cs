using MyNewHiringWebApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Infrastructure.Interfaces
{
    public interface IJobApplicationRepository : IRepository<JobApplication>
    {
        Task<JobApplication> GetWithDetailsAsync(int id, CancellationToken ct = default);
    }
}
