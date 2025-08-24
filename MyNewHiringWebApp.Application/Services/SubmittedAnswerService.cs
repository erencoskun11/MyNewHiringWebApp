using AutoMapper;
using MyNewHiringWebApp.Application.DTOs.SubmittedAnswerCreateDtos;
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
    public class SubmittedAnswerService : GenericService<
        SubmittedAnswer,
        SubmittedAnswerDto,
        SubmittedAnswerCreateDto,
        SubmittedAnswerUpdateDto
    >, ISubmittedAnswerService
    {
        public SubmittedAnswerService(IRepository<SubmittedAnswer> repo, IMapper mapper)
            : base(repo, mapper)
        {
        }

        public async Task<IEnumerable<SubmittedAnswerDto>> GetByTestSubmissionIdAsync(int submissionId, CancellationToken ct = default)
        {
            var answers = await _repo.ListAsync(a => a.TestSubmissionId == submissionId, ct);
            return _mapper.Map<IEnumerable<SubmittedAnswerDto>>(answers);
        }

        public async Task<IEnumerable<SubmittedAnswerDto>> GetByQuestionIdAsync(int questionId, CancellationToken ct = default)
        {
            var answers = await _repo.ListAsync(a => a.QuestionId == questionId, ct);
            return _mapper.Map<IEnumerable<SubmittedAnswerDto>>(answers);
        }
    }
}
