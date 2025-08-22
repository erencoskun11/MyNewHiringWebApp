using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Application.DTOs.CandidateDtos
{
    public class CandidateDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }
        public DateTime AppliedAt { get; set; }

        // Resume bilgileri
        public string? ResumeSummary { get; set; }
        public string? GithubUrl { get; set; }
        public string? LinkedInUrl { get; set; }

        // İsteğe bağlı: Candidate skill listesi
        public List<CandidateSkillDto>? Skills { get; set; }
    }

    public class CandidateSkillDto
    {
        public string SkillName { get; set; } = null!;
        public int Level { get; set; }
    }

}

