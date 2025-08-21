using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Domain.Entities
{
    public sealed class TestSubmission
    {
        public int Id { get; private set; }
        public int TestId { get; private set; }
        public Test Test { get; private set; } = null!;

        public int CandidateId { get; private set; }
        public Candidate Candidate { get; private set; } = null!;

        public DateTime SubmittedAt { get; private set; }
        private readonly List<SubmittedAnswer> _answers = new();
        public IReadOnlyCollection<SubmittedAnswer> Answers => _answers.AsReadOnly();

        public double? Score { get; private set; }

        private TestSubmission() { }

        public TestSubmission(int testId, int candidateId)
        {
            TestId = testId;
            CandidateId = candidateId;
            SubmittedAt = DateTime.UtcNow;
        }

        public void AddAnswer(SubmittedAnswer a) => _answers.Add(a);
        public void SetScore(double score) => Score = score;
    }
}
