using AutoMapper;
using MyNewHiringWebApp.Application.DTOs.JobApplicationDtos;
using MyNewHiringWebApp.Application.Interface;
using MyNewHiringWebApp.Application.InterfaceServices;
using MyNewHiringWebApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Application.Services
{
    public class JobApplicationService : GenericService<
        JobApplication,
        JobApplicationDto,
        JobApplicationCreateDto,
        JobApplicationUpdateDto
    >, IJobApplicationService
    {
        public JobApplicationService(IRepository<JobApplication> repo, IMapper mapper)
            : base(repo, mapper)
        {
        }

        public async Task<IEnumerable<JobApplicationDto>> GetByCandidateIdAsync(int candidateId, CancellationToken ct = default)
        {
            var applications = await _repo.ListAsync(j => j.CandidateId == candidateId, ct);
            return _mapper.Map<IEnumerable<JobApplicationDto>>(applications);
        }

        public async Task<IEnumerable<JobApplicationDto>> GetByJobPositionIdAsync(int jobPositionId, CancellationToken ct = default)
        {
            var applications = await _repo.ListAsync(j => j.JobPositionId == jobPositionId, ct);
            return _mapper.Map<IEnumerable<JobApplicationDto>>(applications);
        }

        public async Task<IEnumerable<JobApplicationDto>> GetAppliedAfterAsync(DateTime date, CancellationToken ct = default)
        {
            var applications = await _repo.ListAsync(j => j.AppliedAt > date, ct);
            return _mapper.Map<IEnumerable<JobApplicationDto>>(applications);
        }
    }
}