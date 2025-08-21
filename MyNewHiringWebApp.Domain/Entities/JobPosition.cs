using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Domain.Entities
{
    public sealed class JobPosition
    {
        public int Id { get; private set; }
        public string Title { get; private set; } = null!;
        public string? Description { get; private set; }
        public bool IsActive { get; private set; } = true;

        public int DepartmentId { get; private set; }
        public Department Department { get; private set; } = null!;

        private readonly List<JobApplication> _applications = new();
        public IReadOnlyCollection<JobApplication> Applications => _applications.AsReadOnly();

        private JobPosition() { }

        public JobPosition(string title, int departmentId, string? description = null)
        {
            if (string.IsNullOrWhiteSpace(title)) throw new System.ArgumentException("title required", nameof(title));
            Title = title;
            DepartmentId = departmentId;
            Description = description;
        }

        public void Deactivate() => IsActive = false;
    }
}
