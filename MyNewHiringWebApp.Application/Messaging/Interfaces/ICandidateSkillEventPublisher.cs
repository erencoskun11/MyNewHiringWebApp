using MyNewHiringWebApp.Application.ETOs.CandidateSkillsEtos;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Application.Messaging.Interfaces
{
    public interface ICandidateSkillEventPublisher
    {
        Task PublishCandidateSkillCreatedAsync(CandidateSkillCreatedEto eto);
    }
}
