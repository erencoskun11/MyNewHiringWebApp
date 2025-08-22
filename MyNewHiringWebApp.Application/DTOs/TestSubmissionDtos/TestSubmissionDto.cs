using MyNewHiringWebApp.Application.DTOs.SubmittedAnswerCreateDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Application.DTOs.TestSubmissionDtos
{
    public class TestSubmissionDto
    {
        public int Id { get; set; }
        public int TestId { get; set; }
        public int CandidateId { get; set; }
        public DateTime SubmittedAt { get; set; }
        public double? Score { get; set; }

        public List<SubmittedAnswerDto> Answers { get; set; } = new();
    }
}
