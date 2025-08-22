using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Application.DTOs.InterviesDtos
{
    public class InterviewCreateDto
    {
        public int JobApplicationId { get; set; }
        public int InterviewerId { get; set; }
        public DateTime ScheduledAt { get; set; }
        public string? Notes { get; set; }
    }
}
