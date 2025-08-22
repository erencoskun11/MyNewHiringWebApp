using MyNewHiringWebApp.Application.DTOs.TestQuestionDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Application.DTOs.TestDtos
{
    public class TestDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }

        public List<TestQuestionDto> Questions { get; set; } = new();
    }
}
