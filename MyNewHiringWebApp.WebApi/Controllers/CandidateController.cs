using Microsoft.AspNetCore.Mvc;
using MyNewHiringWebApp.Application.DTOs.CandidateDtos;
using MyNewHiringWebApp.Application.InterfaceServices;

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
        public async Task<IEnumerable<CandidateDto>> GetAll()
        {
            return await _candidateService.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<CandidateDto> GetById(int id)
        {
            return await _candidateService.GetByIdAsync(id);
        }

        [HttpPost]
        public async Task<bool> Create([FromBody] CandidateCreateDto createDto)
        {
            if (!ModelState.IsValid) return false;
            return await _candidateService.CreateAsync(createDto);
            
        }

        [HttpPut("{id}")]
        public async Task<bool> Update(int id, [FromBody] CandidateUpdateDto updateDto)
        {
            if (!ModelState.IsValid) return false;
            return await _candidateService.UpdateAsync(id, updateDto);
        }

        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            return await _candidateService.DeleteAsync(id); // bool döner

        }
    }
}

