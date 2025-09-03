using Microsoft.AspNetCore.Mvc;
using MyNewHiringWebApp.Application.DTOs.TestDtos;
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
    public class TestsController : ControllerBase
    {
        private readonly ITestService _service;
        public TestsController(ITestService service) => _service = service;

        [HttpGet]
        [CacheManagement(typeof(TestCacheModel), CacheOperationType.Read)]

        public Task<IEnumerable<TestDto>> GetAll(CancellationToken ct = default)
            => _service.GetAllAsync(ct);

        [HttpGet("{id}")]
        [CacheManagement(typeof(TestCacheModel), CacheOperationType.Read)]

        public Task<TestDto?> GetById(int id, CancellationToken ct = default)
            => _service.GetByIdAsync(id, ct);

        [HttpPost]
        [CacheManagement(typeof(TestCacheModel), CacheOperationType.Refresh)]

        public async Task<bool> Create([FromBody] TestCreateDto dto, CancellationToken ct = default)
        {
            var id = await _service.CreateAsync(dto, ct);
            return id;
        }

        [HttpPut("{id}")]
        [CacheManagement(typeof(TestCacheModel), CacheOperationType.Refresh)]

        public async Task<bool> Update(int id, [FromBody] TestUpdateDto dto, CancellationToken ct = default)
        {
            await _service.UpdateAsync(id, dto, ct);
            return true;
        }

        [HttpDelete("{id}")]
        [CacheManagement(typeof(TestCacheModel), CacheOperationType.Refresh)]

        public async Task<bool> Delete(int id, CancellationToken ct = default)
        {
            await _service.DeleteAsync(id, ct);
            return true;
        }

        [HttpGet("by-title")]
        public Task<IEnumerable<TestDto>> GetByTitle([FromQuery] string title, CancellationToken ct = default)
            => _service.GetByTitleAsync(title, ct);
    }
}
