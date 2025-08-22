using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Application.DTOs.CandidateDtos
{
    public class CandidateCreateDto
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }

        //for resume
        public string? ResumeSummary { get; set; }
        public string? GithubUrl { get; set; }
        public string? LinkedInUrl { get; set; }
    }
}
