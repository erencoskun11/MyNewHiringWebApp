using Microsoft.AspNetCore.Mvc;
using MyNewHiringWebApp.Application.DTOs.TestQuestionDtos;
using MyNewHiringWebApp.Application.InterfaceServices;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestQuestionsController : ControllerBase
    {
        private readonly ITestQuestionService _service;
        public TestQuestionsController(ITestQuestionService service) => _service = service;

        [HttpGet]
        public Task<IEnumerable<TestQuestionDto>> GetAll(CancellationToken ct = default)
            => _service.GetAllAsync(ct);

        [HttpGet("{id}")]
        public Task<TestQuestionDto?> GetById(int id, CancellationToken ct = default)
            => _service.GetByIdAsync(id, ct);

        [HttpPost]
        public async Task<bool> Create([FromBody] TestQuestionCreateDto dto, CancellationToken ct = default)
        {
            var id = await _service.CreateAsync(dto, ct);
            return id > 0;
        }

        [HttpPut("{id}")]
        public async Task<bool> Update(int id, [FromBody] TestQuestionUpdateDto dto, CancellationToken ct = default)
        {
            await _service.UpdateAsync(id, dto, ct);
            return true;
        }

        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id, CancellationToken ct = default)
        {
            await _service.DeleteAsync(id, ct);
            return true;
        }

        [HttpGet("by-test/{testId}")]
        public Task<IEnumerable<TestQuestionDto>> GetByTestId(int testId, CancellationToken ct = default)
            => _service.GetByTestIdAsync(testId, ct);
    }
}
