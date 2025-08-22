using MyNewHiringWebApp.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Application.DTOs.TestQuestionDtos
{
    public class TestQuestionCreateDto
    {
        public string Text { get; set; } = null!;
        public QuestionType Type { get; set; }
        public string? OptionsJson { get; set; }
        public int? CorrectOptionIndex { get; set; }
    }
}
