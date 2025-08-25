using Microsoft.AspNetCore.Mvc;
using MyNewHiringWebApp.Application.DTOs.SubmittedAnswerCreateDtos;
using MyNewHiringWebApp.Application.InterfaceServices;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubmittedAnswersController : ControllerBase
    {
        private readonly ISubmittedAnswerService _service;
        public SubmittedAnswersController(ISubmittedAnswerService service) => _service = service;

        [HttpGet]
        public Task<IEnumerable<SubmittedAnswerDto>> GetAll(CancellationToken ct = default)
            => _service.GetAllAsync(ct);

        [HttpGet("{id}")]
        public Task<SubmittedAnswerDto?> GetById(int id, CancellationToken ct = default)
            => _service.GetByIdAsync(id, ct);

        [HttpPost]
        public async Task<bool> Create([FromBody] SubmittedAnswerCreateDto dto, CancellationToken ct = default)
        {
            var id = await _service.CreateAsync(dto, ct);
            return id;
        }

        [HttpPut("{id}")]
        public async Task<bool> Update(int id, [FromBody] SubmittedAnswerUpdateDto dto, CancellationToken ct = default)
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

        [HttpGet("by-submission/{submissionId}")]
        public Task<IEnumerable<SubmittedAnswerDto>> GetBySubmissionId(int submissionId, CancellationToken ct = default)
            => _service.GetByTestSubmissionIdAsync(submissionId, ct);

        [HttpGet("by-question/{questionId}")]
        public Task<IEnumerable<SubmittedAnswerDto>> GetByQuestionId(int questionId, CancellationToken ct = default)
            => _service.GetByQuestionIdAsync(questionId, ct);
    }
}
