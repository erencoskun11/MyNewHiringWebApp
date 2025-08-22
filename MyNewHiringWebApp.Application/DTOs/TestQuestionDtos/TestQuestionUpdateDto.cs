using MyNewHiringWebApp.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Application.DTOs.TestQuestionDtos
{
    public class TestQuestionUpdateDto
    {
        public string? Text { get; set; }
        public QuestionType? Type { get; set; }
        public string? OptionsJson { get; set; }
        public int? CorrectOptionIndex { get; set; }
    }
}
