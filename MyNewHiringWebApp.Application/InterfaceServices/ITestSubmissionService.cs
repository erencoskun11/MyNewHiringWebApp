using MyNewHiringWebApp.Application.DTOs.TestSubmissionDtos;
using MyNewHiringWebApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Application.InterfaceServices
{
    public interface ITestSubmissionService
        : IGenericService<TestSubmission, TestSubmissionDto, TestSubmissionCreateDto, TestSubmissionUpdateDto>
    {
        Task<IEnumerable<TestSubmissionDto>> GetByCandidateIdAsync(int candidateId, CancellationToken ct = default);
        Task<IEnumerable<TestSubmissionDto>> GetByTestIdAsync(int testId, CancellationToken ct = default);
    }
}
