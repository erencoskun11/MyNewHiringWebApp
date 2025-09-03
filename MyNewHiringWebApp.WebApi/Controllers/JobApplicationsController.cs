using Microsoft.AspNetCore.Mvc;
using MyNewHiringWebApp.Application.DTOs.JobApplicationDtos;
using MyNewHiringWebApp.Application.InterfaceServices;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MyNewHiringWebApp.Application.Models;
using MyNewHiringWebApp.WebApi.Attributes;

namespace MyNewHiringWebApp.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobApplicationsController : ControllerBase
    {
        private readonly IJobApplicationService _service;
        public JobApplicationsController(IJobApplicationService service) => _service = service;

        [HttpGet]
        public Task<IEnumerable<JobApplicationDto>> GetAll(CancellationToken ct = default)
            => _service.GetAllAsync(ct);

        [HttpGet("{id}")]
        public Task<JobApplicationDto?> GetById(int id, CancellationToken ct = default)
            => _service.GetByIdAsync(id, ct);

        [HttpPost]
        public async Task<bool> Create([FromBody] JobApplicationCreateDto dto, CancellationToken ct = default)
        {
            var id = await _service.CreateAsync(dto, ct);
            return id;
        }

        [HttpPut("{id}")]
        public async Task<bool> Update(int id, [FromBody] JobApplicationUpdateDto dto, CancellationToken ct = default)
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
        public Task<IEnumerable<JobApplicationDto>> GetByCandidateId(int candidateId, CancellationToken ct = default)
            => _service.GetByCandidateIdAsync(candidateId, ct);

        [HttpGet("by-jobposition/{jobPositionId}")]
        public Task<IEnumerable<JobApplicationDto>> GetByJobPosition(int jobPositionId, CancellationToken ct = default)
            => _service.GetByJobPositionIdAsync(jobPositionId, ct);

        [HttpGet("applied-after")]
        public Task<IEnumerable<JobApplicationDto>> GetAppliedAfter([FromQuery] DateTime date, CancellationToken ct = default)
            => _service.GetAppliedAfterAsync(date, ct);
    }
}
