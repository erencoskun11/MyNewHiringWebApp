
using Microsoft.AspNetCore.Mvc;
using MyNewHiringWebApp.Application.DTOs.SkillDtos;
using MyNewHiringWebApp.Application.InterfaceServices;
using MyNewHiringWebApp.Application.Models;
using MyNewHiringWebApp.Application.Services.Caching;
using MyNewHiringWebApp.WebApi.Attributes;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SkillsController : ControllerBase
    {
        private readonly ISkillService _service;
        public SkillsController(ISkillService service) => _service = service;

        [HttpGet]
        [CacheManagement(typeof(SkillCacheModel), CacheOperationType.Read)]

        public async Task<IEnumerable<SkillDto>> GetAll(CancellationToken ct = default)
        {
            return await _service.GetAllAsync(ct);
        }

        [HttpGet("{id}")]
        [CacheManagement(typeof(SkillCacheModel), CacheOperationType.Read)]

        public async Task<SkillDto?> GetById(int id, CancellationToken ct = default)
        {
            return await _service.GetByIdAsync(id, ct);
        }

        [HttpPost]
        [CacheManagement(typeof(SkillCacheModel), CacheOperationType.Refresh)]
        public async Task<bool> Create([FromBody] SkillCreateDto dto, CancellationToken ct = default)
        {
            if (!ModelState.IsValid) return false;
            return await _service.CreateAsync(dto, ct);
        }

        [HttpPut("{id}")]
        [CacheManagement(typeof(SkillCacheModel), CacheOperationType.Refresh)]
        public async Task<bool> Update(int id, [FromBody] SkillUpdateDto dto, CancellationToken ct = default)
        {
            if (!ModelState.IsValid) return false;
            return await _service.UpdateAsync(id, dto, ct);
        }

        [HttpDelete("{id}")]
        [CacheManagement(typeof(SkillCacheModel), CacheOperationType.Refresh)]
        public async Task<bool> Delete(int id, CancellationToken ct = default)
        {
            return await _service.DeleteAsync(id, ct);
        }

        [HttpGet("search")]
        public async Task<IEnumerable<SkillDto>> SearchByName([FromQuery] string name, CancellationToken ct = default)
        {
            return await _service.SearchByNameAsync(name, ct);
        }
    }
}
