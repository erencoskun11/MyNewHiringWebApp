using MyNewHiringWebApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Application.Interface
{
    public interface ISkillRepository : IRepository<Skill>
    {
        Task<Skill?> GetByNameAsync(string name, CancellationToken ct = default);
    }
}
