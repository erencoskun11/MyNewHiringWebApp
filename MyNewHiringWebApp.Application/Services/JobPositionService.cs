using AutoMapper;
using MyNewHiringWebApp.Application.DTOs.JobPositionDtos;
using MyNewHiringWebApp.Application.Interface;
using MyNewHiringWebApp.Application.InterfaceServices;
using MyNewHiringWebApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Application.Services
{
    public class JobPositionService : GenericService<
      JobPosition,
      JobPositionDto,
      JobPositionCreateDto,
      JobPositionUpdateDto
  >, IJobPositionService
    {
        public JobPositionService(IRepository<JobPosition> repo, IMapper mapper)
            : base(repo, mapper)
        {
        }

        public async Task<IEnumerable<JobPositionDto>> GetByDepartmentIdAsync(int departmentId, CancellationToken ct = default)
        {
            var positions = await _repo.ListAsync(p => p.DepartmentId == departmentId, ct);
            return _mapper.Map<IEnumerable<JobPositionDto>>(positions);
        }

        public async Task<IEnumerable<JobPositionDto>> GetActivePositionsAsync(CancellationToken ct = default)
        {
            var positions = await _repo.ListAsync(p => p.IsActive, ct);
            return _mapper.Map<IEnumerable<JobPositionDto>>(positions);
        }
    }
}
