using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyNewHiringWebApp.Application.DTOs.CandidateDtos;
using MyNewHiringWebApp.Application.ETOs.CandidateEtos;
using MyNewHiringWebApp.Application.InterfaceServices;
using MyNewHiringWebApp.Application.Messaging.Interfaces;
using MyNewHiringWebApp.Application.Models;
using MyNewHiringWebApp.Application.Services.Caching;
using MyNewHiringWebApp.WebApi.Attributes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private readonly ICandidateService _candidateService;
        private readonly IMapper _mapper;
        private readonly ICandidateEventPublisher _candidateEventPublisher;

        public CandidateController(
            ICandidateService candidateService,
            IMapper mapper,
            ICandidateEventPublisher candidateEventPublisher)
        {
            _candidateService = candidateService;
            _mapper = mapper;
            _candidateEventPublisher = candidateEventPublisher;
        }

        [HttpPost("bulk")]
        [CacheManagement(typeof(CandidateCacheModel), CacheOperationType.Refresh)]
        public async Task<IActionResult> CreateBulk([FromBody] List<CandidateCreateDto> createDtos)
        {
            if (createDtos == null || createDtos.Count == 0)
                return BadRequest("Empty payload.");

            var created = new List<string>();
            var skipped = new List<string>();
            var errors = new List<string>();

            foreach (var dto in createDtos)
            {
                try
                {
                    // normalize email
                    var email = (dto.Email ?? string.Empty).Trim().ToLowerInvariant();
                    if (string.IsNullOrWhiteSpace(email))
                    {
                        errors.Add($"Missing email for candidate {dto.FirstName} {dto.LastName}");
                        continue;
                    }

                    var existing = await _candidateService.GetByEmailAsync(email);
                    if (existing != null)
                    {
                        skipped.Add(email);
                        continue;
                    }

                    var result = await _candidateService.CreateAsync(dto);
                    if (!result)
                    {
                        errors.Add($"Create failed for {email}");
                        continue;
                    }

                    var candidate = await _candidateService.GetByEmailAsync(email);
                    if (candidate != null)
                    {
                        var eto = _mapper.Map<CandidateCreatedEto>(candidate);
                        await _candidateEventPublisher.PublishCandidateCreatedAsync(eto);
                    }

                    created.Add(email);
                }
                catch (Exception ex)
                {
                    // log exception (Console / ILogger)
                    errors.Add($"Error for {dto.Email}: {ex.Message}");
                }
            }

            return Ok(new { Created = created.Count, CreatedEmails = created, Skipped = skipped, Errors = errors });
        }

        [HttpGet]
        [CacheManagement(typeof(CandidateCacheModel), CacheOperationType.Read)]
        public async Task<IEnumerable<CandidateDto>> GetAll()
        {
            return await _candidateService.GetAllAsync();
        }

        [HttpGet("{id}")]
        [CacheManagement(typeof(CandidateCacheModel), CacheOperationType.Read)]
        public async Task<CandidateDto?> GetById(int id)
        {
            return await _candidateService.GetByIdAsync(id);
        }

        [HttpPost]
        [CacheManagement(typeof(CandidateCacheModel), CacheOperationType.Refresh)]
        public async Task<bool> Create([FromBody] CandidateCreateDto createDto)
        {
            var result = await _candidateService.CreateAsync(createDto);

            if (result)
            {
                var candidate = await _candidateService.GetByEmailAsync(createDto.Email);

                var eto = _mapper.Map<CandidateCreatedEto>(candidate);

                await _candidateEventPublisher.PublishCandidateCreatedAsync(eto);
            }

            return result;
        }

        [HttpPut("{id}")]
        [CacheManagement(typeof(CandidateCacheModel), CacheOperationType.Refresh)]
        public async Task<bool> Update(int id, [FromBody] CandidateUpdateDto updateDto)
        {
            return await _candidateService.UpdateAsync(id, updateDto);
        }

        [HttpDelete("{id}")]
        [CacheManagement(typeof(CandidateCacheModel), CacheOperationType.Refresh)]
        public async Task<bool> Delete(int id)
        {
            return await _candidateService.DeleteAsync(id);
        }
    }
}

