using MyNewHiringWebApp.Application.DTOs.DepartmentDtos;
using MyNewHiringWebApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Application.InterfaceServices
{
    public interface IDepartmentService
        : IGenericService<Department, DepartmentDto,DepartmentCreateDto,DepartmentUpdateDto>
    {
        Task<DepartmentDto> GetByNameAsync(string name, CancellationToken ct = default);
    }
}
