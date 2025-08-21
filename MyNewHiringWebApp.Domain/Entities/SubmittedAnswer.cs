using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Domain.Entities
{
    public sealed class SubmittedAnswer
    {
        public int Id { get; private set; }
        public int TestSubmissionId { get; private set; }
        public TestSubmission TestSubmission { get; private set; } = null!;

        public int QuestionId { get; private set; }
        public int? SelectedOptionIndex { get; private set; }
        public string? AnswerText { get; private set; }

        private SubmittedAnswer() { }

        public SubmittedAnswer(int questionId, int? selectedIndex = null, string? answerText = null)
        {
            QuestionId = questionId;
            SelectedOptionIndex = selectedIndex;
            AnswerText = answerText;
        }
    }
}
