using MyNewHiringWebApp.Application.DTOs.InterviewerDtos;
using MyNewHiringWebApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Application.InterfaceServices
{
    public interface IInterviewerService
        : IGenericService<Interviewer, InterviewerDto, InterviewerCreateDto, InterviewerUpdateDto>
    {
        Task<InterviewerDto?> GetByEmailAsync(string email, CancellationToken ct = default);
        Task<IEnumerable<InterviewerDto>> SearchByNameAsync(string name, CancellationToken ct = default);
    }
}
