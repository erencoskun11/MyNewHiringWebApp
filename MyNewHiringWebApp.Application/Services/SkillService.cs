using AutoMapper;
using MyNewHiringWebApp.Application.DTOs.SkillDtos;
using MyNewHiringWebApp.Application.Interface;
using MyNewHiringWebApp.Application.InterfaceServices;
using MyNewHiringWebApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Application.Services
{
    public class SkillService : GenericService<
       Skill,
       SkillDto,
       SkillCreateDto,
       SkillUpdateDto
   >, ISkillService
    {
        public SkillService(IRepository<Skill> repo, IMapper mapper)
            : base(repo, mapper)
        {
        }

        public async Task<IEnumerable<SkillDto>> SearchByNameAsync(string name, CancellationToken ct = default)
        {
            var skills = await _repo.ListAsync(
                s => s.Name.Contains(name, StringComparison.OrdinalIgnoreCase),ct);
            return _mapper.Map<IEnumerable<SkillDto>>(skills);
        }
    }
}
