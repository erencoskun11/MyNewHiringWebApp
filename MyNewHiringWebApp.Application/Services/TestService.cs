using AutoMapper;
using MyNewHiringWebApp.Application.DTOs.TestDtos;
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
    public class TestService : GenericService<Test, TestDto, TestCreateDto, TestUpdateDto>, ITestService
    {
        public TestService(IRepository<Test> repo, IMapper mapper)
            : base(repo, mapper)
        {
        }

        public async Task<IEnumerable<TestDto>> GetByTitleAsync(string title, CancellationToken ct = default)
        {
            var tests = await _repo.ListAsync(t => t.Title.Contains(title, StringComparison.OrdinalIgnoreCase), ct);
            return _mapper.Map<IEnumerable<TestDto>>(tests);
        }
    }
}