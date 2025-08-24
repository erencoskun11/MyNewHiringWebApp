using MyNewHiringWebApp.Application.DTOs.JobApplicationDtos;
using MyNewHiringWebApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Application.InterfaceServices
{
    public interface IJobApplicationService
        : IGenericService<JobApplication, JobApplicationDto, JobApplicationCreateDto, JobApplicationUpdateDto>
    {
        Task<IEnumerable<JobApplicationDto>> GetByCandidateIdAsync(int candidateId, CancellationToken ct = default);
        Task<IEnumerable<JobApplicationDto>> GetByJobPositionIdAsync(int jobPositionId, CancellationToken ct = default);
        Task<IEnumerable<JobApplicationDto>> GetAppliedAfterAsync(DateTime date, CancellationToken ct = default);
    }
}
