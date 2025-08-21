using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Domain.Entities
{
    public sealed class Test
    {
        public int Id { get; private set; }
        public string Title { get; private set; } = null!;
        public string? Description { get; private set; }

        private readonly List<TestQuestion> _questions = new();
        public IReadOnlyCollection<TestQuestion> Questions => _questions.AsReadOnly();

        private Test() { }

        public Test(string title, string? description = null)
        {
            Title = title ?? throw new System.ArgumentNullException(nameof(title));
            Description = description;
        }

        public void AddQuestion(TestQuestion q) => _questions.Add(q);
    }
}
