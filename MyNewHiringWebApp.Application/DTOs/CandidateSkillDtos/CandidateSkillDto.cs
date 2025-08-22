using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Application.DTOs.CandidateSkillDtos
{
    public class CandidateSkillDto
    {
        public int SkillId { get; set; }
        public string SkillName { get; set; } = null!;
        public int Level { get; set; }
    }
}
