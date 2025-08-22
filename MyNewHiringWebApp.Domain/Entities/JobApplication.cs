using MyNewHiringWebApp.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Domain.Entities
{
    public sealed class JobApplication
    {
        public int Id { get; private set; }

        public int CandidateId { get; private set; }
        public Candidate Candidate { get; private set; } = null!;

        public int JobPositionId { get; private set; }
        public JobPosition JobPosition { get; private set; } = null!;

        public DateTime AppliedAt { get; private set; }
        public JobApplicationStatus Status { get; private set; }

        
        public byte[]? RowVersion { get; private set; }

        private JobApplication() { }

        public JobApplication(int candidateId, int jobPositionId)
        {
            CandidateId = candidateId;
            JobPositionId = jobPositionId;
            AppliedAt = DateTime.UtcNow;
            Status = JobApplicationStatus.Applied;
        }

        public void UpdateStatus(JobApplicationStatus status) => Status = status;
    }
}
