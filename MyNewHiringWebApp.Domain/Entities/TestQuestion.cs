using MyNewHiringWebApp.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Domain.Entities
{
    public sealed class TestQuestion
    {
        public int Id { get; private set; }
        public int TestId { get; private set; }
        public Test Test { get; private set; } = null!;

        public string Text { get; private set; } = null!;
        public QuestionType Type { get; private set; }
        public string? OptionsJson { get; private set; } // JSON stored options
        public int? CorrectOptionIndex { get; private set; }

        private TestQuestion() { }

        public TestQuestion(string text, QuestionType type, string? optionsJson = null, int? correctIndex = null)
        {
            Text = text ?? throw new System.ArgumentNullException(nameof(text));
            Type = type;
            OptionsJson = optionsJson;
            CorrectOptionIndex = correctIndex;
        }
    }
}
