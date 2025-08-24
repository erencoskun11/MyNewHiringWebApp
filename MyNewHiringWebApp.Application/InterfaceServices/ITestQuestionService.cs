using MyNewHiringWebApp.Application.DTOs.TestQuestionDtos;
using MyNewHiringWebApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Application.InterfaceServices
{
    public interface ITestQuestionService
        : IGenericService<TestQuestion, TestQuestionDto, TestQuestionCreateDto, TestQuestionUpdateDto>
    {
        Task<IEnumerable<TestQuestionDto>> GetByTestIdAsync(int testId, CancellationToken ct = default);
    }
}
