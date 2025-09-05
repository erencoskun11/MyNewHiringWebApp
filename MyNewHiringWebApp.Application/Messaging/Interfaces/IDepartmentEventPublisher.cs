using MyNewHiringWebApp.Application.ETOs.DepartmentEtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyNewHiringWebApp.Application.Messaging.Interfaces;

namespace MyNewHiringWebApp.Application.Messaging.Interfaces
{
    public interface IDepartmentEventPublisher
    {
        Task PublishDepartmentCreatedAsync(DepartmentCreateEtos eto);

    }
}
