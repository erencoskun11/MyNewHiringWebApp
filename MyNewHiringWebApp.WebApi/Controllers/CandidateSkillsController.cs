using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyNewHiringWebApp.Application.DTOs.CandidateSkillDtos;
using MyNewHiringWebApp.Application.ETOs.CandidateSkillsEtos;
using MyNewHiringWebApp.Application.Interface;
using MyNewHiringWebApp.Application.InterfaceServices;
using MyNewHiringWebApp.Application.Messaging.Interfaces;
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
    public class CandidateSkillsController : ControllerBase
    {
        private readonly ICandidateSkillService _service;
        private readonly IMapper _mapper;
        private readonly ICandidateEventPublisher _candidateEventPublisher;
        

        
        
        public CandidateSkillsController(ICandidateSkillService service,IMapper mapper,ICandidateEventPublisher candidateEventPublisher)
        {
            _service = service;
            _mapper = mapper;
            _candidateEventPublisher = candidateEventPublisher;
        }

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
            // 1) Create (bool)
            var created = await _service.CreateAsync(dto, ct);
            if (!created) return false;

            // 2) Read created entity from DB
            // IMPORTANT: implement this read method in your service if it doesn't exist yet.
            // Suggested signature: Task<CandidateSkill> GetByCandidateAndSkillAsync(int candidateId, int skillId, CancellationToken ct = default)
            var candidateSkill = await _service.GetByCandidateAndSkillAsync(dto.CandidateId, dto.SkillId, ct);
            if (candidateSkill == null)
            {
                // Eğer hemen görünmüyorsa transaction/commit beklemesi olabilir.
                // Burada false döndürmüyoruz çünkü create başarılı oldu; sadece publish atlamış olacağız.
                return true;
            }

            // 3) Map entity -> ETO
            var eto = _mapper.Map<CandidateSkillCreatedEto>(candidateSkill);

            // 4) Publish (fire-and-forget style: hata publish'i bozmasın)
            try
            {
                await _candidateEventPublisher.PublishCandidateSkillCreatedAsync(eto);
            }
            catch
            {
                // İsteğe bağlı: ILogger ile logla. Publish hatası create'yi etkilemesin.
                // _logger?.LogError(ex, "Failed to publish CandidateSkillCreated for candidate {CandidateId} skill {SkillId}", dto.CandidateId, dto.SkillId);
            }

            return true;
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
