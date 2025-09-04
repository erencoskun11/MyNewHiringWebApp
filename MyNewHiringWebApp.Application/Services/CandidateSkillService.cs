using AutoMapper;
using MyNewHiringWebApp.Application.Interface;
using MyNewHiringWebApp.Application.InterfaceServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyNewHiringWebApp.Application.DTOs.CandidateSkillDtos;

namespace MyNewHiringWebApp.Application.Services
{
    public class CandidateSkillService : GenericService<
        CandidateSkill, CandidateSkillDto, CandidateSkillCreateDto, CandidateSkillUpdateDto>, ICandidateSkillService
    {
        public CandidateSkillService(IRepository<CandidateSkill> repo, IMapper mapper) : base(repo, mapper)
        {
        }

        public async Task<IEnumerable<CandidateSkillDto>> GetByCandidateIdAsync(int candidateId, CancellationToken ct = default)
        {
            var skills = await _repo.ListAsync(cs => cs.CandidateId == candidateId, ct);
            return _mapper.Map<IEnumerable<CandidateSkillDto>>(skills);
        }

        public async Task<IEnumerable<CandidateSkillDto>> GetBySkillIdAsync(int skillId, CancellationToken ct = default)
        {
            var skills = await _repo.ListAsync(cs => cs.SkillId == skillId, ct);
            return _mapper.Map<IEnumerable<CandidateSkillDto>>(skills);
        }
        public async Task<CandidateSkill?> GetByCandidateAndSkillAsync(int candidateId, int skillId, CancellationToken ct = default)
        {
            // Eğer repository'nizde FirstOrDefaultAsync varsa onu kullan; yoksa ListAsync + FirstOrDefault.
            var list = await _repo.ListAsync(cs => cs.CandidateId == candidateId && cs.SkillId == skillId, ct);
            return list?.FirstOrDefault();
        }

    }
}
