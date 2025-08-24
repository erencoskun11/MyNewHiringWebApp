using MyNewHiringWebApp.Application.DTOs.InterviesDtos;
using MyNewHiringWebApp.Application.DTOs.InterviewDtos;
using MyNewHiringWebApp.Domain.Entities;
using MyNewHiringWebApp.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Application.InterfaceServices
{
    public interface IInterviewService
        : IGenericService<Interview, InterviewDto, InterviewCreateDto, InterviewUpdateDto>
    {
        Task<IEnumerable<InterviewDto>> GetByJobApplicationIdAsync(int jobApplicationId, CancellationToken ct = default);
        Task<IEnumerable<InterviewDto>> GetByInterviewerIdAsync(int interviewerId, CancellationToken ct = default);
        Task<IEnumerable<InterviewDto>> GetByResultAsync(InterviewResult result, CancellationToken ct = default);
    }
}
