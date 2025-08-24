using Microsoft.AspNetCore.Mvc;
using MyNewHiringWebApp.Application.DTOs.TestSubmissionDtos;
using MyNewHiringWebApp.Application.InterfaceServices;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestSubmissionsController : ControllerBase
    {
        private readonly ITestSubmissionService _service;
        public TestSubmissionsController(ITestSubmissionService service) => _service = service;

        [HttpGet]
        public Task<IEnumerable<TestSubmissionDto>> GetAll(CancellationToken ct = default)
            => _service.GetAllAsync(ct);

        [HttpGet("{id}")]
        public Task<TestSubmissionDto?> GetById(int id, CancellationToken ct = default)
            => _service.GetByIdAsync(id, ct);

        [HttpPost]
        public async Task<bool> Create([FromBody] TestSubmissionCreateDto dto, CancellationToken ct = default)
        {
            var id = await _service.CreateAsync(dto, ct);
            return id > 0;
        }

        [HttpPut("{id}")]
        public async Task<bool> Update(int id, [FromBody] TestSubmissionUpdateDto dto, CancellationToken ct = default)
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

        [HttpGet("by-candidate/{candidateId}")]
        public Task<IEnumerable<TestSubmissionDto>> GetByCandidateId(int candidateId, CancellationToken ct = default)
            => _service.GetByCandidateIdAsync(candidateId, ct);

        [HttpGet("by-test/{testId}")]
        public Task<IEnumerable<TestSubmissionDto>> GetByTestId(int testId, CancellationToken ct = default)
            => _service.GetByTestIdAsync(testId, ct);
    }
}
