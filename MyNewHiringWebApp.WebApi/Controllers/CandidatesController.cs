using Microsoft.AspNetCore.Mvc;
using MyNewHiringWebApp.Application.DTOs.CandidateDtos;
using MyNewHiringWebApp.Application.InterfaceServices;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CandidatesController : ControllerBase
    {
        private readonly ICandidateService _service;
        public CandidatesController(ICandidateService service) => _service = service;

        [HttpGet]
        public Task<IEnumerable<CandidateDto>> GetAll(CancellationToken ct = default)
            => _service.GetAllAsync(ct);

        [HttpGet("{id}")]
        public Task<CandidateDto?> GetById(int id, CancellationToken ct = default)
            => _service.GetByIdAsync(id, ct);

        [HttpPost]
        public async Task<bool> Create([FromBody] CandidateCreateDto dto, CancellationToken ct = default)
        {
            var id = await _service.CreateAsync(dto, ct);
            return id > 0;
        }

        [HttpPut("{id}")]
        public async Task<bool> Update(int id, [FromBody] CandidateUpdateDto dto, CancellationToken ct = default)
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
        public Task<CandidateDto?> GetByEmail([FromQuery] string email, CancellationToken ct = default)
            => _service.GetByEmailAsync(email, ct);

        [HttpGet("all-by-email")]
        public Task<IEnumerable<CandidateDto>> GetAllByEmail([FromQuery] string email, CancellationToken ct = default)
            => _service.GetAllByEmailAsync(email, ct);

        [HttpGet("search")]
        public Task<IEnumerable<CandidateDto>> SearchByName([FromQuery] string name, CancellationToken ct = default)
            => _service.SearchByNameAsync(name, ct);

        [HttpGet("applied-after")]
        public Task<IEnumerable<CandidateDto>> GetApplicatedAfter([FromQuery] DateTime after, CancellationToken ct = default)
            => _service.GetApplicatedAfterAsync(after, ct);

        [HttpGet("by-skill/{skillId}")]
        public Task<IEnumerable<CandidateDto>> GetBySkill(int skillId, CancellationToken ct = default)
            => _service.GetBySkillAsync(skillId, ct);
    }
}
