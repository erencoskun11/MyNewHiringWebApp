using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Application.ETOs.CandidateSkillsEtos
{
    public sealed class CandidateSkillCreatedEto
    {
        public int CandidateId { get; set; }
        public int SkillId { get; set; }
        public string SkillName { get; set; } = null!;
        public int Level { get; set; } // from 1 to 5
    }
}
