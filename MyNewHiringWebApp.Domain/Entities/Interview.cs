using MyNewHiringWebApp.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Domain.Entities
{
    public sealed class Interview
    {
        public int Id { get; private set; }

        public int JobApplicationId { get; private set; }
        public JobApplication JobApplication { get; private set; } = null!;

        public int InterviewerId { get; private set; }
        public Interviewer Interviewer { get; private set; } = null!;

        public DateTime ScheduledAt { get; private set; }
        public string? Notes { get; private set; }
        public InterviewResult Result { get; private set; } = InterviewResult.Pending;

        private Interview() { }

        public Interview(int jobApplicationId, int interviewerId, DateTime scheduledAt)
        {
            JobApplicationId = jobApplicationId;
            InterviewerId = interviewerId;
            ScheduledAt = scheduledAt;
        }

        public void SetResult(InterviewResult result, string? notes = null)
        {
            Result = result;
            Notes = notes;
        }
    }
}
