using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Application.DTOs.CandidateSkillDtos
{
    public class CandidateSkillCreateDto
    {
        public int CandidateId { get; set; }
        public int SkillId { get; set; }
        public int Level { get; set; } = 1; // default 1
    }
}
