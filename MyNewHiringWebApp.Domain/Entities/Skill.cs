using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Domain.Entities
{
    public sealed class Skill
    {
        public int Id { get; private set; }
        public string Name { get; private set; } = null!;

        private readonly List<CandidateSkill> _candidateSkills = new();
        public IReadOnlyCollection<CandidateSkill> CandidateSkills => _candidateSkills.AsReadOnly();

        private Skill() { }

        public Skill(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new System.ArgumentException("name required", nameof(name));
            Name = name;
        }
    }
}
