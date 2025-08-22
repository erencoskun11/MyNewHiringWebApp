using MyNewHiringWebApp.Application.DTOs.SubmittedAnswerCreateDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Application.DTOs.TestSubmissionDtos
{
    public class TestSubmissionCreateDto
    {
        public int TestId { get; set; }
        public int CandidateId { get; set; }

        public List<SubmittedAnswerCreateDto>? Answers { get; set; } = new();
    }
}
