using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Domain.Entities
{
    public sealed class CandidateSkill
    {
        public int CandidateId { get; private set; }
        public Candidate Candidate { get; private set; } = null!;

        public int SkillId { get; private set; }
        public Skill Skill { get; private set; } = null!;

        public int Level { get; private set; } // 1..5

        private CandidateSkill() { }

        public CandidateSkill(Candidate candidate, Skill skill, int level = 1)
        {
            Candidate = candidate ?? throw new System.ArgumentNullException(nameof(candidate));
            Skill = skill ?? throw new System.ArgumentNullException(nameof(skill));
            SetLevel(level);
        }

        public void SetLevel(int level)
        {
            if (level < 1 || level > 5) throw new System.ArgumentOutOfRangeException(nameof(level));
            Level = level;
        }
    }
}
