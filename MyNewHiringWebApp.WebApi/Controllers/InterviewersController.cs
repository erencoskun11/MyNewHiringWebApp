using Microsoft.AspNetCore.Mvc;
using MyNewHiringWebApp.Application.DTOs.InterviewerDtos;
using MyNewHiringWebApp.Application.InterfaceServices;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InterviewersController : ControllerBase
    {
        private readonly IInterviewerService _service;
        public InterviewersController(IInterviewerService service) => _service = service;

        [HttpGet]
        public Task<IEnumerable<InterviewerDto>> GetAll(CancellationToken ct = default)
            => _service.GetAllAsync(ct);

        [HttpGet("{id}")]
        public Task<InterviewerDto?> GetById(int id, CancellationToken ct = default)
            => _service.GetByIdAsync(id, ct);

        [HttpPost]
        public async Task<bool> Create([FromBody] InterviewerCreateDto dto, CancellationToken ct = default)
        {
            var id = await _service.CreateAsync(dto, ct);
            return id;
        }

        [HttpPut("{id}")]
        public async Task<bool> Update(int id, [FromBody] InterviewerUpdateDto dto, CancellationToken ct = default)
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

        [HttpGet("by-email")]
        public Task<InterviewerDto?> GetByEmail([FromQuery] string email, CancellationToken ct = default)
            => _service.GetByEmailAsync(email, ct);

        [HttpGet("search")]
        public Task<IEnumerable<InterviewerDto>> SearchByName([FromQuery] string name, CancellationToken ct = default)
            => _service.SearchByNameAsync(name, ct);
    }
}
