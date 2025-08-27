using Microsoft.AspNetCore.Mvc;
using MyNewHiringWebApp.Application.DTOs.CandidateDtos;
using MyNewHiringWebApp.Application.InterfaceServices;
using System.Threading.Tasks;

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
        public async Task<IActionResult> GetAll()
        {
            var list = await _candidateService.GetAllAsync();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var candidate = await _candidateService.GetByIdAsync(id);
            if (candidate == null) return NotFound();
            return Ok(candidate);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CandidateCreateDto createDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var created = await _candidateService.CreateAsync(createDto);
            if (created)
            {
                return Ok(); // Veya service created id döndürebilirse: CreatedAtAction(nameof(GetById), new { id = createdId }, createdDto);
            }

            return BadRequest("Unable to create candidate.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CandidateUpdateDto updateDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var updated = await _candidateService.UpdateAsync(id, updateDto);
            if (updated) return NoContent();

            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _candidateService.DeleteAsync(id);
            if (deleted) return NoContent();

            return NotFound();
        }
    }
}

