using Microsoft.AspNetCore.Mvc;
using MyNewHiringWebApp.Application.DTOs.InterviewerDtos;
using MyNewHiringWebApp.Application.InterfaceServices;
using MyNewHiringWebApp.Application.Models;
using MyNewHiringWebApp.Application.Services.Caching;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MyNewHiringWebApp.Application.Models;
using MyNewHiringWebApp.WebApi.Attributes;

namespace MyNewHiringWebApp.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InterviewersController : ControllerBase
    {
        private readonly IInterviewerService _service;
        public InterviewersController(IInterviewerService service) => _service = service;

        [HttpGet]
        [CacheManagement(typeof(InterviewerCacheModel), CacheOperationType.Read)]
        public Task<IEnumerable<InterviewerDto>> GetAll(CancellationToken ct = default)
            => _service.GetAllAsync(ct);

        [HttpGet("{id}")]
        [CacheManagement(typeof(InterviewerCacheModel), CacheOperationType.Read)]

        public Task<InterviewerDto?> GetById(int id, CancellationToken ct = default)
            => _service.GetByIdAsync(id, ct);

        [HttpPost]
        [CacheManagement(typeof(InterviewerCacheModel), CacheOperationType.Refresh)]
        public async Task<bool> Create([FromBody] InterviewerCreateDto dto, CancellationToken ct = default)
        {
            var id = await _service.CreateAsync(dto, ct);
            return id;
        }

        [HttpPut("{id}")]
        [CacheManagement(typeof(InterviewerCacheModel), CacheOperationType.Refresh)]
        public async Task<bool> Update(int id, [FromBody] InterviewerUpdateDto dto, CancellationToken ct = default)
        {
            await _service.UpdateAsync(id, dto, ct);
            return true;
        }

        [HttpDelete("{id}")]
        [CacheManagement(typeof(InterviewerCacheModel),CacheOperationType.Refresh)]
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
