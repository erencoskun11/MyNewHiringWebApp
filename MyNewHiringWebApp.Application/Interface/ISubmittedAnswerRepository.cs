﻿using MyNewHiringWebApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNewHiringWebApp.Application.Interface
{
    public interface ISubmittedAnswerRepository : IRepository<SubmittedAnswer>

    {
        Task<IReadOnlyList<SubmittedAnswer>> GetBySubmissionIdAsync(int submissionId, CancellationToken ct = default);

    }
}
