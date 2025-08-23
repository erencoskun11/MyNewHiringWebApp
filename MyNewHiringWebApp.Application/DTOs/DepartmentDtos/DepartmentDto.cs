using MyNewHiringWebApp.Application.DTOs.JobPositionDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Application.DTOs.DepartmentDtos
{
    public class DepartmentDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public List<JobPositionDto> Positions { get; set; } = new();
    }
}
