using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MyNewHiringWebApp.Infrastructure.Data;
using MyNewHiringWebApp.Application.Interface;            // IRepository<>
using MyNewHiringWebApp.Infrastructure.Repositories;     // BaseRepository<>
using MyNewHiringWebApp.Application.InterfaceServices;   // I*Service
using MyNewHiringWebApp.Application.Services;            // *Service implementations
using AutoMapper;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Logging (daha fazla hata çýktýsý için)
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(opts =>
    {
        opts.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

// AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());



// AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());



// DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        sql => sql.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null));
});

// AutoMapper - tüm assembly'leri tarar (MappingProfile varsa otomatik bulur)
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Generic repository
builder.Services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));

// Concrete services
builder.Services.AddScoped<ICandidateService, CandidateService>();
builder.Services.AddScoped<ICandidateSkillService, CandidateSkillService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IInterviewerService, InterviewerService>();
builder.Services.AddScoped<IInterviewService, InterviewService>();
builder.Services.AddScoped<IJobApplicationService, JobApplicationService>();
builder.Services.AddScoped<IJobPositionService, JobPositionService>();
builder.Services.AddScoped<ISkillService, SkillService>();
builder.Services.AddScoped<ISubmittedAnswerService, SubmittedAnswerService>();
builder.Services.AddScoped<ITestQuestionService, TestQuestionService>();
builder.Services.AddScoped<ITestService, TestService>();
builder.Services.AddScoped<ITestSubmissionService, TestSubmissionService>();

// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyNewHiringWebApp API", Version = "v1" });

    // DTO isim çakýþmalarýný önlemek için schemaId’yi namespace ile birlikte kullan
    c.CustomSchemaIds(type =>
    {
        if (type.Namespace != null && type.Namespace.Contains("DTOs"))
            return type.FullName!.Replace(".", "_");
        return type.Name;
    });
});

// CORS (opsiyonel, frontend baþka origin’den geliyorsa lazým)
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

// Developer exception page
app.UseDeveloperExceptionPage();

// Swagger
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"));

// CORS
app.UseCors("AllowAll");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// DB migrate + seed (opsiyonel, hata varsa konsola bas)
using (var scope = app.Services.CreateScope())
{
    try
    {
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        // db.Database.Migrate(); // ihtiyaç varsa aç
        // DbSeeder.Seed(db);     // seed varsa aç
    }
    catch (Exception ex)
    {
        Console.WriteLine("DB migrate/seed hatasý: " + ex);
    }
}

app.Run();
