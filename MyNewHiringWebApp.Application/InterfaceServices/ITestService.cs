using MyNewHiringWebApp.Application.DTOs.TestDtos;
using MyNewHiringWebApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Application.InterfaceServices
{
    public interface ITestService
       : IGenericService<Test, TestDto, TestCreateDto, TestUpdateDto>
    {
        Task<IEnumerable<TestDto>> GetByTitleAsync(string title, CancellationToken ct = default);
    }
}
