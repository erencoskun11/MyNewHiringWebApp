using AutoMapper;
using MyNewHiringWebApp.Application.DTOs.DepartmentDtos;
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
    public class DepartmentService : GenericService<
        Department,
        DepartmentDto,
        DepartmentCreateDto,
        DepartmentUpdateDto
        >, IDepartmentService
    {
        public DepartmentService(IRepository<Department> repo, IMapper mapper) : base(repo, mapper)
        {
        }

        public async Task<DepartmentDto> GetByNameAsync(string name, CancellationToken ct = default)
        {
            var departments = await _repo.ListAsync(c=>c.Name == name,ct);
            return _mapper.Map<DepartmentDto>(departments);
        }
    }
}
