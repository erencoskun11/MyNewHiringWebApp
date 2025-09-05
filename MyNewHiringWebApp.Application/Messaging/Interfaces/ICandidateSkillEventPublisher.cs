using MyNewHiringWebApp.Application.ETOs.CandidateSkillsEtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Application.Messaging.Interfaces
{
    public interface ICandidateSkillEventPublisher
    {
        Task PublisherCandidateSkillCreatedAsync(CandidateSkillCreatedEto eto);
    }
}
