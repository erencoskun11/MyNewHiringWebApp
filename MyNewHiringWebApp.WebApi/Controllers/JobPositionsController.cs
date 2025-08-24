using Microsoft.AspNetCore.Mvc;
using MyNewHiringWebApp.Application.DTOs.JobPositionDtos;
using MyNewHiringWebApp.Application.InterfaceServices;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobPositionsController : ControllerBase
    {
        private readonly IJobPositionService _service;
        public JobPositionsController(IJobPositionService service) => _service = service;

        [HttpGet]
        public Task<IEnumerable<JobPositionDto>> GetAll(CancellationToken ct = default)
            => _service.GetAllAsync(ct);

        [HttpGet("{id}")]
        public Task<JobPositionDto?> GetById(int id, CancellationToken ct = default)
            => _service.GetByIdAsync(id, ct);

        [HttpPost]
        public async Task<bool> Create([FromBody] JobPositionCreateDto dto, CancellationToken ct = default)
        {
            var id = await _service.CreateAsync(dto, ct);
            return id > 0;
        }

        [HttpPut("{id}")]
        public async Task<bool> Update(int id, [FromBody] JobPositionUpdateDto dto, CancellationToken ct = default)
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

        [HttpGet("by-department/{departmentId}")]
        public Task<IEnumerable<JobPositionDto>> GetByDepartment(int departmentId, CancellationToken ct = default)
            => _service.GetByDepartmentIdAsync(departmentId, ct);

        [HttpGet("active")]
        public Task<IEnumerable<JobPositionDto>> GetActive(CancellationToken ct = default)
            => _service.GetActivePositionsAsync(ct);
    }
}
