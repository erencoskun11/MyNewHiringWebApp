using AutoMapper;
using MyNewHiringWebApp.Application.DTOs.CandidateDtos;
using MyNewHiringWebApp.Application.Interface;
using MyNewHiringWebApp.Application.InterfaceServices;
using MyNewHiringWebApp.Application.Services.Caching;
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
        private readonly ICacheService _cache;
        private string GetAllKey => "candidates:all";
        private string GetByEmailKey(string email) => $"candidates:email:{email}";
        private string GetBySkillKey(int skillId) => $"candidates:skill:{skillId}";
        private string SearchByNameKey(string name) => $"candidates:search:{name}";

        public CandidateService(IRepository<Candidate> repo, IMapper mapper,ICacheService cache)
            : base(repo, mapper)
        {
            _cache = cache;
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
            var cacheKey = GetByEmailKey(email);

            //1.Cache control
            var cached = await _cache.GetAsync<CandidateDto>(cacheKey);
            if (cached != null) return cached;

            //2. pull from db

            var candidate = await _repo.FindAsync(c => c.Email == email, ct);
            if (candidate == null) return null;

            var dto = _mapper.Map<CandidateDto>(candidate);

            await _cache.SetAsync(cacheKey, dto, TimeSpan.FromMinutes(5));
            return dto;
        
        
        }

        public async Task<IEnumerable<CandidateDto>> GetBySkillAsync(int skillId, CancellationToken ct = default)
        {
            var cacheKey = GetBySkillKey(skillId);

            var cached = await _cache.GetAsync<IEnumerable<CandidateDto>>(cacheKey);
            if (cached != null) return cached;


            var candidates = await _repo.ListAsync(c => c.CandidateSkills.Any(cs => cs.SkillId == skillId), ct);
            var result = _mapper.Map<IEnumerable<CandidateDto>>(candidates);

            await _cache.SetAsync(cacheKey, result, TimeSpan.FromMinutes(10));

            return result;
        
        }

        public async Task<IEnumerable<CandidateDto>> SearchByNameAsync(string name, CancellationToken ct = default)
        {
            var cacheKey = SearchByNameKey(name);

            var cached = await _cache.GetAsync<IEnumerable<CandidateDto>>(cacheKey);
            if(cached != null) return cached;


            var candidates = await _repo.ListAsync(c =>
           c.FirstName.ToLower().Contains(name.ToLower()) ||
            c.LastName.ToLower().Contains(name.ToLower()),ct);

            var result = _mapper.Map<IEnumerable<CandidateDto>>(candidates);

            await _cache.SetAsync(cacheKey,result, TimeSpan.FromMinutes(10));
            
            return result;
            
        }
    }
}