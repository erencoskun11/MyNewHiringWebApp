using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Application.DTOs.JobPositionDtos
{
    public class JobPositionUpdateDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool? IsActive { get; set; } // opsiyonel olarak aktif/pasif durumu
        public int? DepartmentId { get; set; }
    }
}
