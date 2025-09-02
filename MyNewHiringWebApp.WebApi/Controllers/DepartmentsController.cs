using Microsoft.AspNetCore.Mvc;
using MyNewHiringWebApp.Application.DTOs.DepartmentDtos;
using MyNewHiringWebApp.Application.InterfaceServices;
using MyNewHiringWebApp.Application.Models;
using MyNewHiringWebApp.Application.Services.Caching;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MyNewHiringWebApp.Application.Models;
using MyNewHiringWebApp.Application.Services.Caching.Attributes;

namespace MyNewHiringWebApp.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentService _service;
        public DepartmentsController(IDepartmentService service) => _service = service;

        [HttpGet]
        [CacheManagement(typeof(DepartmentCacheModel),CacheOperationType.Read)]
        public Task<IEnumerable<DepartmentDto>> GetAll(CancellationToken ct = default)
            => _service.GetAllAsync(ct);

        [HttpGet("{id}")]
        [CacheManagement(typeof(DepartmentCacheModel), CacheOperationType.Read)]

        public Task<DepartmentDto?> GetById(int id, CancellationToken ct = default)
            => _service.GetByIdAsync(id, ct);

        [HttpPost]
        [CacheManagement(typeof(DepartmentCacheModel), CacheOperationType.Refresh)]
        public async Task<bool> Create([FromBody] DepartmentCreateDto dto, CancellationToken ct = default)
        {
            var id = await _service.CreateAsync(dto, ct);
            return id;
        }

        [HttpPut("{id}")]
        [CacheManagement(typeof(DepartmentCacheModel), CacheOperationType.Refresh)]
        public async Task<bool> Update(int id, [FromBody] DepartmentUpdateDto dto, CancellationToken ct = default)
        {
            await _service.UpdateAsync(id, dto, ct);
            return true;
        }

        [HttpDelete("{id}")]
        [CacheManagement(typeof(DepartmentCacheModel), CacheOperationType.Refresh)]
        public async Task<bool> Delete(int id, CancellationToken ct = default)
        {
            await _service.DeleteAsync(id, ct);
            return true;
        }

        [HttpGet("by-name")]
        public Task<DepartmentDto?> GetByName([FromQuery] string name, CancellationToken ct = default)
            => _service.GetByNameAsync(name, ct);
    }
}
