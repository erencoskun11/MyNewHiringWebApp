using Microsoft.AspNetCore.Mvc;
using MyNewHiringWebApp.Application.DTOs.SkillDtos;
using MyNewHiringWebApp.Application.InterfaceServices;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SkillsController : ControllerBase
    {
        private readonly ISkillService _service;
        public SkillsController(ISkillService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken ct = default)
        {
            var skills = await _service.GetAllAsync(ct);
            return Ok(skills);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken ct = default)
        {
            var skill = await _service.GetByIdAsync(id, ct);
            if (skill == null) return NotFound();
            return Ok(skill);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SkillCreateDto dto, CancellationToken ct = default)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var createdId = await _service.CreateAsync(dto, ct);
            if (createdId )
                return CreatedAtAction(nameof(GetById), new { id = createdId }, dto);

            return BadRequest("Skill could not be created.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] SkillUpdateDto dto, CancellationToken ct = default)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var existing = await _service.GetByIdAsync(id, ct);
            if (existing == null) return NotFound();

            await _service.UpdateAsync(id, dto, ct);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken ct = default)
        {
            var existing = await _service.GetByIdAsync(id, ct);
            if (existing == null) return NotFound();

            await _service.DeleteAsync(id, ct);
            return NoContent();
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchByName([FromQuery] string name, CancellationToken ct = default)
        {
            var results = await _service.SearchByNameAsync(name, ct);
            return Ok(results);
        }
    }
}
