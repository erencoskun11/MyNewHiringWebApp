using MyNewHiringWebApp.Application.Messaging.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Application.Messaging.Interfaces
{
    public class ICandidateEventPublisher
    {
        Task PublishCandidateCreatedAsync(CandidateCreateEvent @event);
    }
}
