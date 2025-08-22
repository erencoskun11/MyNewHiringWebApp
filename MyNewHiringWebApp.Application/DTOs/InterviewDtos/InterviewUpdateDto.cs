using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Application.DTOs.InterviewDtos
{
    public class InterviewUpdateDto
    {
        public int Id { get; set; }
        public DateTime ScheduledAt { get; set; }
        public string? Notes { get; set; }
        public string Result { get; set; } = "Pending";
    }
}
