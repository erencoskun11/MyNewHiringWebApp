using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Domain.Entities
{
    public sealed class Candidate
    {
        public int Id { get; private set; }
        public string FirstName { get; private set; } = null!;
        public string LastName { get; private set; } = null!;
        public string Email { get; private set; } = null!;
        public string? Phone { get; private set; }
        public DateTime AppliedAt { get; private set; }

        // Owned value object
        public ResumeSummary? Resume { get; private set; }

        private readonly List<CandidateSkill> _candidateSkills = new();
        public IReadOnlyCollection<CandidateSkill> CandidateSkills => _candidateSkills.AsReadOnly();

        private readonly List<JobApplication> _applications = new();
        public IReadOnlyCollection<JobApplication> Applications => _applications.AsReadOnly();

        private readonly List<TestSubmission> _submissions = new();
        public IReadOnlyCollection<TestSubmission> Submissions => _submissions.AsReadOnly();

        private Candidate() { /* for EF */ }

        public Candidate(string firstName, string lastName, string email, string? phone = null)
        {
            if (string.IsNullOrWhiteSpace(firstName)) throw new ArgumentException("firstName required", nameof(firstName));
            if (string.IsNullOrWhiteSpace(lastName)) throw new ArgumentException("lastName required", nameof(lastName));
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("email required", nameof(email));

            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
            AppliedAt = DateTime.UtcNow;
        }

        public void UpdateContact(string email, string? phone)
        {
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("email required", nameof(email));
            Email = email;
            Phone = phone;
        }

        public void SetResume(ResumeSummary resume) => Resume = resume ?? throw new ArgumentNullException(nameof(resume));

        public void AddSkill(Skill skill, int level = 1)
        {
            if (skill == null) throw new ArgumentNullException(nameof(skill));
            if (level < 1 || level > 5) throw new ArgumentOutOfRangeException(nameof(level));
            if (_candidateSkills.Any(cs => cs.SkillId == skill.Id))
                throw new InvalidOperationException("Skill already added to candidate.");

            _candidateSkills.Add(new CandidateSkill(this, skill, level));
        }
    }

    public sealed class ResumeSummary
    {
        public string? Summary { get; private set; }
        public string? GithubUrl { get; private set; }
        public string? LinkedInUrl { get; private set; }

        private ResumeSummary() { }

        public ResumeSummary(string? summary, string? githubUrl, string? linkedInUrl)
        {
            Summary = summary;
            GithubUrl = githubUrl;
            LinkedInUrl = linkedInUrl;
        }
    }
}
