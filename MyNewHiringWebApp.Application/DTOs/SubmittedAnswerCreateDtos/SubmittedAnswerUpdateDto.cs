using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Application.DTOs.SubmittedAnswerCreateDtos
{
    public class SubmittedAnswerUpdateDto
    {
        public int? SelectedOptionIndex { get; set; }
        public string? AnswerText { get; set; }
    }
}
