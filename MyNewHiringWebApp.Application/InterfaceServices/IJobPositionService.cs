using MyNewHiringWebApp.Application.DTOs.JobPositionDtos;
using MyNewHiringWebApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Application.InterfaceServices
{
    public interface IJobPositionService
       : IGenericService<JobPosition, JobPositionDto, JobPositionCreateDto, JobPositionUpdateDto>
    {
        Task<IEnumerable<JobPositionDto>> GetByDepartmentIdAsync(int departmentId, CancellationToken ct = default);
        Task<IEnumerable<JobPositionDto>> GetActivePositionsAsync(CancellationToken ct = default);
    }
}
