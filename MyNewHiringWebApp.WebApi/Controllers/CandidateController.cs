using Microsoft.AspNetCore.Mvc;
using MyNewHiringWebApp.Application.DTOs.CandidateDtos;
using MyNewHiringWebApp.Application.InterfaceServices;
using MyNewHiringWebApp.Application.Models;
using MyNewHiringWebApp.Application.Services.Caching;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyNewHiringWebApp.Application.Models;
using MyNewHiringWebApp.WebApi.Attributes;



namespace MyNewHiringWebApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private readonly ICandidateService _candidateService;

        public CandidateController(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        [HttpGet]
        [CacheManagement(typeof(CandidateCacheModel),CacheOperationType.Read)]
        public async Task<IEnumerable<CandidateDto>> GetAll()
        {
            return await _candidateService.GetAllAsync();
        }

        [HttpGet("{id}")]
        [CacheManagement(typeof(CandidateCacheModel),CacheOperationType.Read)]
        public async Task<CandidateDto?> GetById(int id)
        {
            return await _candidateService.GetByIdAsync(id);
        }

        [HttpPost]
        [CacheManagement(typeof(CandidateCacheModel),CacheOperationType.Refresh)]
        public async Task<bool> Create([FromBody] CandidateCreateDto createDto)
        {
            return await _candidateService.CreateAsync(createDto);
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

