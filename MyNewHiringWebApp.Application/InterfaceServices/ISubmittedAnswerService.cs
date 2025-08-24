using MyNewHiringWebApp.Application.DTOs.SubmittedAnswerCreateDtos;
using MyNewHiringWebApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Application.InterfaceServices
{
    public interface ISubmittedAnswerService
       : IGenericService<SubmittedAnswer, SubmittedAnswerDto, SubmittedAnswerCreateDto, SubmittedAnswerUpdateDto>
    {
        Task<IEnumerable<SubmittedAnswerDto>> GetByTestSubmissionIdAsync(int submissionId, CancellationToken ct = default);
        Task<IEnumerable<SubmittedAnswerDto>> GetByQuestionIdAsync(int questionId, CancellationToken ct = default);
    }
}
