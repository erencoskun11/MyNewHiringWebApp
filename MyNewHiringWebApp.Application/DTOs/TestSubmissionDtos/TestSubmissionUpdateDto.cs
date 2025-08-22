using MyNewHiringWebApp.Application.DTOs.SubmittedAnswerCreateDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Application.DTOs.TestSubmissionDtos
{
    public class TestSubmissionUpdateDto
    {
        public List<SubmittedAnswerUpdateDto>? Answers { get; set; }
        public double? Score { get; set; }
    }
}
