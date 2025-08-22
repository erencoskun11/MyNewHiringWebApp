using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Application.DTOs.CandidateDtos
{
    public class CandidateUpdateDto
    {
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }

        //Resume update
        public string? ResumeSummary { get; set; }
        public string? GithubUrl { get; set; }
        public string? LinkedInUrl { get; set; }
    }
}
