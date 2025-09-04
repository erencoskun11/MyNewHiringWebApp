using System.Threading.Tasks;
using MyNewHiringWebApp.Application.ETOs.CandidateEtos;
using MyNewHiringWebApp.Application.ETOs.CandidateSkillsEtos;

namespace MyNewHiringWebApp.Application.Messaging.Interfaces
{
    public interface ICandidateEventPublisher
    {
        Task PublishCandidateCreatedAsync(CandidateCreatedEto eto);
        Task PublishCandidateSkillCreatedAsync(CandidateSkillCreatedEto eto);
    }
}

