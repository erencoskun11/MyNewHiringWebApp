using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Application.DTOs.SkillDtos
{
    public class SkillDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int CandidateCount { get; set; } // kaç adayda kullanılmış
    }
}
