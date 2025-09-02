using Microsoft.AspNetCore.Mvc;
using MyNewHiringWebApp.Application.DTOs.InterviesDtos;
using MyNewHiringWebApp.Application.DTOs.InterviewDtos;
using MyNewHiringWebApp.Application.InterfaceServices;
using MyNewHiringWebApp.Application.Models;
using MyNewHiringWebApp.Application.Services.Caching;
using MyNewHiringWebApp.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MyNewHiringWebApp.Application.Models;
using MyNewHiringWebApp.Application.Services.Caching.Attributes;

namespace MyNewHiringWebApp.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InterviewsController : ControllerBase
    {
        private readonly IInterviewService _service;
        public InterviewsController(IInterviewService service) => _service = service;

        [HttpGet]
        [CacheManagement(typeof(InterviewCacheModel),CacheOperationType.Read)]
        public Task<IEnumerable<InterviewDto>> GetAll(CancellationToken ct = default)
            => _service.GetAllAsync(ct);

        [HttpGet("{id}")]
        [CacheManagement(typeof(InterviewCacheModel), CacheOperationType.Read)]
        public Task<InterviewDto?> GetById(int id, CancellationToken ct = default)
            => _service.GetByIdAsync(id, ct);

        [HttpPost]
        [CacheManagement(typeof(InterviewCacheModel), CacheOperationType.Refresh)]
        public async Task<bool> Create([FromBody] InterviewCreateDto dto, CancellationToken ct = default)
        {
            var id = await _service.CreateAsync(dto, ct);
            return id;
        }

        [HttpPut("{id}")]
        [CacheManagement(typeof(InterviewCacheModel), CacheOperationType.Refresh)]
        public async Task<bool> Update(int id, [FromBody] InterviewUpdateDto dto, CancellationToken ct = default)
        {
            await _service.UpdateAsync(id, dto, ct);
            return true;
        }

        [HttpDelete("{id}")]
        [CacheManagement(typeof(InterviewCacheModel), CacheOperationType.Refresh)]
        public async Task<bool> Delete(int id, CancellationToken ct = default)
        {
            await _service.DeleteAsync(id, ct);
            return true;
        }

        [HttpGet("by-jobapplication/{jobApplicationId}")]
        public Task<IEnumerable<InterviewDto>> GetByJobApplicationId(int jobApplicationId, CancellationToken ct = default)
            => _service.GetByJobApplicationIdAsync(jobApplicationId, ct);

        [HttpGet("by-interviewer/{interviewerId}")]
        public Task<IEnumerable<InterviewDto>> GetByInterviewerId(int interviewerId, CancellationToken ct = default)
            => _service.GetByInterviewerIdAsync(interviewerId, ct);

        [HttpGet("by-result")]
        public Task<IEnumerable<InterviewDto>> GetByResult([FromQuery] InterviewResult result, CancellationToken ct = default)
            => _service.GetByResultAsync(result, ct);
    }
}
