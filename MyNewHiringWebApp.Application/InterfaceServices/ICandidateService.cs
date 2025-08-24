using MyNewHiringWebApp.Application.DTOs.CandidateDtos;
using MyNewHiringWebApp.Domain.Entities;

namespace MyNewHiringWebApp.Application.InterfaceServices
{
    public interface ICandidateService
        : IGenericService<Candidate, CandidateDto, CandidateCreateDto, CandidateUpdateDto>
    {
        Task<CandidateDto?> GetByEmailAsync(string email, CancellationToken ct = default);

        Task<IEnumerable<CandidateDto>> GetAllByEmailAsync(string email, CancellationToken ct = default);

        Task<IEnumerable<CandidateDto>> SearchByNameAsync(string name, CancellationToken ct = default);

        Task<IEnumerable<CandidateDto>> GetApplicatedAfterAsync(DateTime appliedAfter, CancellationToken ct = default);

        Task<IEnumerable<CandidateDto>> GetBySkillAsync(int skillId, CancellationToken ct = default);

    }
}
