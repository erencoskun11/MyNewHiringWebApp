using MyNewHiringWebApp.Application.DTOs.CandidateSkillDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyNewHiringWebApp.Domain.Entities;
using MyNewHiringWebApp.Application.InterfaceServices;
namespace MyNewHiringWebApp.Application.Interface

{
    public interface ICandidateSkillService
       : IGenericService<CandidateSkill, CandidateSkillDto, CandidateSkillCreateDto, CandidateSkillUpdateDto>
    {
        Task<IEnumerable<CandidateSkillDto>> GetByCandidateIdAsync(int candidateId, CancellationToken ct = default);
        Task<IEnumerable<CandidateSkillDto>> GetBySkillIdAsync(int skillId, CancellationToken ct = default);
    }
}
