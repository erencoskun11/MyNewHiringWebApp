using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Application.DTOs.JobPositionDtos
{
    public class JobPositionCreateDto
    {
        public string Title { get; set; } = null!;
        public int DepartmentId { get; set; }
        public string? Description { get; set; }
    }
}
