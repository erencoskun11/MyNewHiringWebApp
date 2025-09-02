using Microsoft.AspNetCore.Mvc;
using MyNewHiringWebApp.Application.DTOs.CandidateSkillDtos;
using MyNewHiringWebApp.Application.Interface;
using MyNewHiringWebApp.Application.InterfaceServices;
using MyNewHiringWebApp.Application.Models;
using MyNewHiringWebApp.Application.Services.Caching;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CandidateSkillsController : ControllerBase
    {
        private readonly ICandidateSkillService _service;
        public CandidateSkillsController(ICandidateSkillService service) => _service = service;

        [HttpGet]
        [CacheManagement(typeof(CandidateSkillCacheModel),CacheOperationType.Read)]
        public Task<IEnumerable<CandidateSkillDto>> GetAll(CancellationToken ct = default)
            => _service.GetAllAsync(ct);

        [HttpGet("{id}")]
        [CacheManagement(typeof(CandidateSkillCacheModel), CacheOperationType.Read)]

        public Task<CandidateSkillDto?> GetById(int id, CancellationToken ct = default)
            => _service.GetByIdAsync(id, ct);

        [HttpPost]
        [CacheManagement(typeof(CandidateSkillCacheModel), CacheOperationType.Refresh)]

        public async Task<bool> Create([FromBody] CandidateSkillCreateDto dto, CancellationToken ct = default)
        {
            var id = await _service.CreateAsync(dto, ct);
            return id;
        }

        [HttpPut("{id}")]
        [CacheManagement(typeof(CandidateSkillCacheModel), CacheOperationType.Refresh)]

        public async Task<bool> Update(int id, [FromBody] CandidateSkillUpdateDto dto, CancellationToken ct = default)
        {
            await _service.UpdateAsync(id, dto, ct);
            return true;
        }

        [HttpDelete("{id}")]
        [CacheManagement(typeof(CandidateSkillCacheModel), CacheOperationType.Refresh)]
        public async Task<bool> Delete(int id, CancellationToken ct = default)
        {
            await _service.DeleteAsync(id, ct);
            return true;
        }

        [HttpGet("by-candidate/{candidateId}")]
        public Task<IEnumerable<CandidateSkillDto>> GetByCandidate(int candidateId, CancellationToken ct = default)
            => _service.GetByCandidateIdAsync(candidateId, ct);

        [HttpGet("by-skill/{skillId}")]
        public Task<IEnumerable<CandidateSkillDto>> GetBySkill(int skillId, CancellationToken ct = default)
            => _service.GetBySkillIdAsync(skillId, ct);
    }
}
