using MyNewHiringWebApp.Application.DTOs.TestQuestionDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Application.DTOs.TestDtos
{
    public class TestUpdateDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }

        public List<TestQuestionUpdateDto>? Questions { get; set; }
    }
}
