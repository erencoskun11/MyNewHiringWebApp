using AutoMapper;
using MyNewHiringWebApp.Application.DTOs.CandidateDtos;
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
    public class CandidateService : GenericService<
        Candidate,           
        CandidateDto,        
        CandidateCreateDto,   
        CandidateUpdateDto    
    >, ICandidateService     
    {
        public CandidateService(IRepository<Candidate> repo, IMapper mapper)
            : base(repo, mapper)
        {
        }
        public async Task<IEnumerable<CandidateDto>> GetAllByEmailAsync(string email, CancellationToken ct = default)
        {
            var candidate = await _repo.ListAsync(c => c.Email == email,ct);
            return _mapper.Map<IEnumerable<CandidateDto>>(candidate);
        }

        public async Task<IEnumerable<CandidateDto>> GetApplicatedAfterAsync(DateTime appliedAfter, CancellationToken ct = default)
        {
            var candidates = await _repo.ListAsync(c => c.AppliedAt > appliedAfter, ct);
            return _mapper.Map<IEnumerable<CandidateDto>>(candidates);
        }

        public async Task<CandidateDto?> GetByEmailAsync(string email, CancellationToken ct = default)
        {
            var candidate = await _repo.FindAsync(c => c.Email == email, ct);
            return candidate == null ? null : _mapper.Map<CandidateDto>(candidate);
        }

        public async Task<IEnumerable<CandidateDto>> GetBySkillAsync(int skillId, CancellationToken ct = default)
        {
            var candidates = await _repo.ListAsync(c => c.CandidateSkills.Any(cs => cs.SkillId == skillId), ct);
            return _mapper.Map<IEnumerable<CandidateDto>>(candidates);
        }

        public async Task<IEnumerable<CandidateDto>> SearchByNameAsync(string name, CancellationToken ct = default)
        {
            var candidates = await _repo.ListAsync(c =>
           c.FirstName.ToLower().Contains(name.ToLower()) ||
            c.LastName.ToLower().Contains(name.ToLower()),ct);


            return _mapper.Map<IEnumerable<CandidateDto>>(candidates);

            
        }
    }
}