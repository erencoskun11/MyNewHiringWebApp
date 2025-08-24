using AutoMapper;
using MyNewHiringWebApp.Application.DTOs.TestSubmissionDtos;
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
    public class TestSubmissionService : GenericService<TestSubmission, TestSubmissionDto, TestSubmissionCreateDto, TestSubmissionUpdateDto>, ITestSubmissionService
    {
        public TestSubmissionService(IRepository<TestSubmission> repo, IMapper mapper)
            : base(repo, mapper)
        {
        }

        public async Task<IEnumerable<TestSubmissionDto>> GetByCandidateIdAsync(int candidateId, CancellationToken ct = default)
        {
            var submissions = await _repo.ListAsync(s => s.CandidateId == candidateId, ct);
            return _mapper.Map<IEnumerable<TestSubmissionDto>>(submissions);
        }

        public async Task<IEnumerable<TestSubmissionDto>> GetByTestIdAsync(int testId, CancellationToken ct = default)
        {
            var submissions = await _repo.ListAsync(s => s.TestId == testId, ct);
            return _mapper.Map<IEnumerable<TestSubmissionDto>>(submissions);
        }
    }
}
