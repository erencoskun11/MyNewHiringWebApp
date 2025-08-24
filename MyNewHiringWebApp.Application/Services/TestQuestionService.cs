using AutoMapper;
using MyNewHiringWebApp.Application.DTOs.TestQuestionDtos;
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
    public class TestQuestionService : GenericService<TestQuestion, TestQuestionDto, TestQuestionCreateDto, TestQuestionUpdateDto>, ITestQuestionService
    {
        public TestQuestionService(IRepository<TestQuestion> repo, IMapper mapper)
            : base(repo, mapper)
        {
        }

        public async Task<IEnumerable<TestQuestionDto>> GetByTestIdAsync(int testId, CancellationToken ct = default)
        {
            var questions = await _repo.ListAsync(q => q.TestId == testId, ct);
            return _mapper.Map<IEnumerable<TestQuestionDto>>(questions);
        }
    }
}
