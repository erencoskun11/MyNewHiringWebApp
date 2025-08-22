using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Application.DTOs.InterviewDtos
{
    public class InterviewDto
    {
        public int Id { get; set; }
        public int JobApplicationId { get; set; }
        public string JobApplicationTitle { get; set; } = string.Empty;

        public int InterviewerId { get; set; }
        public string InterviewerName { get; set; } = string.Empty;

        public DateTime ScheduledAt { get; set; }
        public string? Notes { get; set; }
        public string Result { get; set; } = "Pending"; // Enum string olarak dönebilir
    }
}
