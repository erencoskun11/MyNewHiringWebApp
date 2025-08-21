using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Domain.Entities
{
    public sealed class Department
    {
        public int Id { get; private set; }
        public string Name { get; private set; } = null!;

        private readonly List<JobPosition> _positions = new();
        public IReadOnlyCollection<JobPosition> Positions => _positions.AsReadOnly();

        private Department() { }

        public Department(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new System.ArgumentException("name required", nameof(name));
            Name = name;
        }
    }
}
