using MyNewHiringWebApp.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Application.DTOs.JobApplicationDtos
{
    public class JobApplicationDto
    {
        public int Id { get; set; }
        public int CandidateId { get; set; }
        public string CandidateName { get; set; } = string.Empty;
        public int JobPositionId { get; set; }
        public string JobPositionTitle { get; set; } = string.Empty;
        public DateTime AppliedAt { get; set; }
        public JobApplicationStatus Status { get; set; }



    }
}
