using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Domain.Entities
{
    public sealed class Interviewer
    {
        public int Id { get; private set; }
        public string FullName { get; private set; } = null!;
        public string Email { get; private set; } = null!;

        private readonly List<Interview> _interviews = new();
        public IReadOnlyCollection<Interview> Interviews => _interviews.AsReadOnly();

        private Interviewer() { }

        public Interviewer(string fullName, string email)
        {
            if (string.IsNullOrWhiteSpace(fullName)) throw new System.ArgumentException(nameof(fullName));
            if (string.IsNullOrWhiteSpace(email)) throw new System.ArgumentException(nameof(email));
            FullName = fullName;
            Email = email;
        }
    }
}
