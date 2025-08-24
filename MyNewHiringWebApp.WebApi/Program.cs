using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MyNewHiringWebApp.Application.InterfaceServices;
using MyNewHiringWebApp.Application.Mappings;
using MyNewHiringWebApp.Application.Services;
using MyNewHiringWebApp.Domain.Entities;
using MyNewHiringWebApp.Infrastructure.Data;
using MyNewHiringWebApp.Infrastructure.Repositories;

using MyAppRepo = MyNewHiringWebApp.Application.Interface;
using MyAppServices = MyNewHiringWebApp.Application.Services;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        sql => sql.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null));
});


// AutoMapper DI
builder.Services.AddAutoMapper(typeof(MappingProfile));





// Generic Repository DI
builder.Services.AddScoped(typeof(MyAppRepo.IRepository<>), typeof(BaseRepository<>));

// Candidate
builder.Services.AddScoped<ICandidateService, CandidateService>();
builder.Services.AddScoped<MyAppRepo.IRepository<Candidate>, BaseRepository<Candidate>>();

// CandidateSkill
builder.Services.AddScoped<ICandidateSkillService, MyAppServices.CandidateSkillService>();
builder.Services.AddScoped<MyAppRepo.IRepository<CandidateSkill>, BaseRepository<CandidateSkill>>();

// Department
builder.Services.AddScoped<IDepartmentService, MyAppServices.DepartmentService>();
builder.Services.AddScoped<MyAppRepo.IRepository<Department>, BaseRepository<Department>>();

// Interview
builder.Services.AddScoped<IInterviewService, MyAppServices.InterviewService>();
builder.Services.AddScoped<MyAppRepo.IRepository<Interview>, BaseRepository<Interview>>();

// Interviewer
builder.Services.AddScoped<IInterviewerService, MyAppServices.InterviewerService>();
builder.Services.AddScoped<MyAppRepo.IRepository<Interviewer>, BaseRepository<Interviewer>>();

// JobApplication
builder.Services.AddScoped<IJobApplicationService, MyAppServices.JobApplicationService>();
builder.Services.AddScoped<MyAppRepo.IRepository<JobApplication>, BaseRepository<JobApplication>>();

// JobPosition
builder.Services.AddScoped<IJobPositionService, MyAppServices.JobPositionService>();
builder.Services.AddScoped<MyAppRepo.IRepository<JobPosition>, BaseRepository<JobPosition>>();

// Skill
builder.Services.AddScoped<ISkillService, MyAppServices.SkillService>();
builder.Services.AddScoped<MyAppRepo.IRepository<Skill>, BaseRepository<Skill>>();

// SubmittedAnswer
builder.Services.AddScoped<ISubmittedAnswerService, MyAppServices.SubmittedAnswerService>();
builder.Services.AddScoped<MyAppRepo.IRepository<SubmittedAnswer>, BaseRepository<SubmittedAnswer>>();

// Test
builder.Services.AddScoped<ITestService, MyAppServices.TestService>();
builder.Services.AddScoped<MyAppRepo.IRepository<Test>, BaseRepository<Test>>();

// TestQuestion
builder.Services.AddScoped<ITestQuestionService, MyAppServices.TestQuestionService>();
builder.Services.AddScoped<MyAppRepo.IRepository<TestQuestion>, BaseRepository<TestQuestion>>();

// TestSubmission
builder.Services.AddScoped<ITestSubmissionService, MyAppServices.TestSubmissionService>();
builder.Services.AddScoped<MyAppRepo.IRepository<TestSubmission>, BaseRepository<TestSubmission>>();
















// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyNewHiringWebApp API", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
