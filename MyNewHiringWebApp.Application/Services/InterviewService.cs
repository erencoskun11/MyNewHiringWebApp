using AutoMapper;
using MyNewHiringWebApp.Application.DTOs.InterviesDtos;
using MyNewHiringWebApp.Application.DTOs.InterviewDtos;
using MyNewHiringWebApp.Application.Interface;
using MyNewHiringWebApp.Application.InterfaceServices;
using MyNewHiringWebApp.Domain.Entities;
using MyNewHiringWebApp.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Application.Services
{
    public class InterviewService : GenericService<
        Interview,
        InterviewDto,
        InterviewCreateDto,
        InterviewUpdateDto
    >, IInterviewService
    {
        public InterviewService(IRepository<Interview> repo, IMapper mapper)
            : base(repo, mapper)
        {
        }

        public async Task<IEnumerable<InterviewDto>> GetByJobApplicationIdAsync(int jobApplicationId, CancellationToken ct = default)
        {
            var interviews = await _repo.ListAsync(i => i.JobApplicationId == jobApplicationId, ct);
            return _mapper.Map<IEnumerable<InterviewDto>>(interviews);
        }

        public async Task<IEnumerable<InterviewDto>> GetByInterviewerIdAsync(int interviewerId, CancellationToken ct = default)
        {
            var interviews = await _repo.ListAsync(i => i.InterviewerId == interviewerId, ct);
            return _mapper.Map<IEnumerable<InterviewDto>>(interviews);
        }

        public async Task<IEnumerable<InterviewDto>> GetByResultAsync(InterviewResult result, CancellationToken ct = default)
        {
            var interviews = await _repo.ListAsync(i => i.Result == result, ct);
            return _mapper.Map<IEnumerable<InterviewDto>>(interviews);
        }
    }
}