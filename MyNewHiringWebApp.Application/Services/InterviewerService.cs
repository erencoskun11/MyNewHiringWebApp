using AutoMapper;
using MyNewHiringWebApp.Application.DTOs.InterviewerDtos;
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
    public class InterviewerService : GenericService<
         Interviewer,
         InterviewerDto,
         InterviewerCreateDto,
         InterviewerUpdateDto
     >, IInterviewerService
    {
        public InterviewerService(IRepository<Interviewer> repo, IMapper mapper)
            : base(repo, mapper)
        {
        }

        public async Task<InterviewerDto?> GetByEmailAsync(string email, CancellationToken ct = default)
        {
            var interviewer = await _repo.FindAsync(i => i.Email == email, ct);
            return interviewer == null ? null : _mapper.Map<InterviewerDto>(interviewer);
        }

        public async Task<IEnumerable<InterviewerDto>> SearchByNameAsync(string name, CancellationToken ct = default)
        {
            var interviewers = await _repo.ListAsync(
                i => i.FullName.Contains(name, StringComparison.OrdinalIgnoreCase), ct);
            return _mapper.Map<IEnumerable<InterviewerDto>>(interviewers);
        }
    }
}