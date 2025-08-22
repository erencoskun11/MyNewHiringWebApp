using MyNewHiringWebApp.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Application.DTOs.JobApplicationDtos
{
    public class JobApplicationUpdateDto
    {
        public JobApplicationStatus Status { get; set; }
    }
}
