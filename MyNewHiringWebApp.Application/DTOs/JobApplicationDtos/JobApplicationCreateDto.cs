using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Application.DTOs.JobApplicationDtos
{
    public class JobApplicationCreateDto
    {
        public int CandidateId { get; set; }
        public int JobPositionId { get; set; }
    }
}
