using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MyNewHiringWebApp.Application.InterfaceServices;
using MyNewHiringWebApp.Application.Mappings;
using MyNewHiringWebApp.Application.Services;
using MyNewHiringWebApp.Domain.Entities;
using MyNewHiringWebApp.Infrastructure.Data;
using MyNewHiringWebApp.Application.Interface;
using MyNewHiringWebApp.Infrastructure.Repositories;
using AutoMapper;
using MyNewHiringWebApp.Application.Services.Caching;
using MyNewHiringWebApp.Infrastructure.Caching;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddControllers()
    .AddJsonOptions(opts =>
    {
        opts.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        sql => sql.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null));
});

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));

builder.Services.AddScoped<ICandidateService, CandidateService>();
builder.Services.AddScoped<IRepository<Candidate>, BaseRepository<Candidate>>();

builder.Services.AddScoped<ICandidateSkillService, CandidateSkillService>();
builder.Services.AddScoped<IRepository<CandidateSkill>, BaseRepository<CandidateSkill>>();

builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IRepository<Department>, BaseRepository<Department>>();

builder.Services.AddScoped<IInterviewService, InterviewService>();
builder.Services.AddScoped<IRepository<Interview>, BaseRepository<Interview>>();

builder.Services.AddScoped<IInterviewerService, InterviewerService>();
builder.Services.AddScoped<IRepository<Interviewer>, BaseRepository<Interviewer>>();

builder.Services.AddScoped<IJobApplicationService, JobApplicationService>();
builder.Services.AddScoped<IRepository<JobApplication>, BaseRepository<JobApplication>>();

builder.Services.AddScoped<IJobPositionService, JobPositionService>();
builder.Services.AddScoped<IRepository<JobPosition>, BaseRepository<JobPosition>>();

builder.Services.AddScoped<ISkillService, SkillService>();
builder.Services.AddScoped<IRepository<Skill>, BaseRepository<Skill>>();

builder.Services.AddScoped<ISubmittedAnswerService, SubmittedAnswerService>();
builder.Services.AddScoped<IRepository<SubmittedAnswer>, BaseRepository<SubmittedAnswer>>();

builder.Services.AddScoped<ITestService, TestService>();
builder.Services.AddScoped<IRepository<Test>, BaseRepository<Test>>();

builder.Services.AddScoped<ITestQuestionService, TestQuestionService>();
builder.Services.AddScoped<IRepository<TestQuestion>, BaseRepository<TestQuestion>>();

builder.Services.AddScoped<ITestSubmissionService, TestSubmissionService>();
builder.Services.AddScoped<IRepository<TestSubmission>, BaseRepository<TestSubmission>>();


builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
    ConnectionMultiplexer.Connect(builder.Configuration["Redis:Configuration"]));
builder.Services.AddSingleton<ICacheService, RedisCacheService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyNewHiringWebApp API", Version = "v1" });
    c.CustomSchemaIds(type =>
    {
        if (type.Namespace != null && type.Namespace.Contains("DTOs"))
            return type.FullName!.Replace(".", "_");
        return type.Name;
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseDeveloperExceptionPage();
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"));
app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    try
    {
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        db.Database.Migrate();
    }
    catch (Exception ex)
    {
        Console.WriteLine("DB migrate hatası: " + ex);
    }
}

app.Run();
