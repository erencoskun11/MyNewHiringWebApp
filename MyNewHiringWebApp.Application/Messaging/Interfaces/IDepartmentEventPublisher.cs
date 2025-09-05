using MyNewHiringWebApp.Application.ETOs.DepartmentEtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Application.Messaging.Interfaces
{
    public interface IDepartmentEventPublisher
    {
        Task PublisherDepartmentCreatedAsync(DepartmentCreateEtos eto);

    }
}
