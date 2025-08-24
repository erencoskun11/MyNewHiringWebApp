using MyNewHiringWebApp.Application.DTOs.SkillDtos;
using MyNewHiringWebApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Application.InterfaceServices
{
    public interface ISkillService
       : IGenericService<Skill, SkillDto, SkillCreateDto, SkillUpdateDto>
    {
        Task<IEnumerable<SkillDto>> SearchByNameAsync(string name, CancellationToken ct = default);
    }
}
