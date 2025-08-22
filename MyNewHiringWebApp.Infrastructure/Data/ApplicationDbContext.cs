using Microsoft.EntityFrameworkCore;
using MyNewHiringWebApp.Domain.Entities;
using MyNewHiringWebApp.Domain.Enum;

namespace MyNewHiringWebApp.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Candidate> Candidates { get; set; } = null!;
        public DbSet<Skill> Skills { get; set; } = null!;
        public DbSet<CandidateSkill> CandidateSkills { get; set; } = null!;
        public DbSet<Department> Departments { get; set; } = null!;
        public DbSet<JobPosition> JobPositions { get; set; } = null!;
        public DbSet<JobApplication> JobApplications { get; set; } = null!;
        public DbSet<Interviewer> Interviewers { get; set; } = null!;
        public DbSet<Interview> Interviews { get; set; } = null!;
        public DbSet<Test> Tests { get; set; } = null!;
        public DbSet<TestQuestion> TestQuestions { get; set; } = null!;
        public DbSet<TestSubmission> TestSubmissions { get; set; } = null!;
        public DbSet<SubmittedAnswer> SubmittedAnswers { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Candidate
            modelBuilder.Entity<Candidate>(b =>
            {
                b.ToTable("Candidates");
                b.HasKey(x => x.Id);
                b.Property(x => x.FirstName).IsRequired().HasMaxLength(100);
                b.Property(x => x.LastName).IsRequired().HasMaxLength(100);
                b.Property(x => x.Email).IsRequired().HasMaxLength(255);
                b.HasIndex(x => x.Email).IsUnique();
                b.Property(x => x.AppliedAt).HasDefaultValueSql("SYSUTCDATETIME()");

                b.OwnsOne(x => x.Resume, r =>
                {
                    r.Property(p => p.Summary).HasColumnName("Resume_Summary").HasMaxLength(2000);
                    r.Property(p => p.GithubUrl).HasColumnName("Resume_GithubUrl").HasMaxLength(500);
                    r.Property(p => p.LinkedInUrl).HasColumnName("Resume_LinkedInUrl").HasMaxLength(500);
                });
            });

            // Skill
            modelBuilder.Entity<Skill>(b =>
            {
                b.ToTable("Skills");
                b.HasKey(x => x.Id);
                b.Property(x => x.Name).IsRequired().HasMaxLength(150);
                b.HasIndex(x => x.Name);
            });

            // CandidateSkill (junction)
            modelBuilder.Entity<CandidateSkill>(b =>
            {
                b.ToTable("CandidateSkills");
                b.HasKey(cs => new { cs.CandidateId, cs.SkillId });
                b.Property(cs => cs.Level).IsRequired();
                b.HasOne(cs => cs.Candidate)
                 .WithMany(c => c.CandidateSkills)
                 .HasForeignKey(cs => cs.CandidateId)
                 .OnDelete(DeleteBehavior.Cascade);
                b.HasOne(cs => cs.Skill)
                 .WithMany(s => s.CandidateSkills)
                 .HasForeignKey(cs => cs.SkillId)
                 .OnDelete(DeleteBehavior.Cascade);
                b.HasCheckConstraint("CK_CandidateSkill_Level", "[Level] BETWEEN 1 AND 5");
            });

            // Department
            modelBuilder.Entity<Department>(b =>
            {
                b.ToTable("Departments");
                b.HasKey(x => x.Id);
                b.Property(x => x.Name).IsRequired().HasMaxLength(150);
            });

            // JobPosition
            modelBuilder.Entity<JobPosition>(b =>
            {
                b.ToTable("JobPositions");
                b.HasKey(x => x.Id);
                b.Property(x => x.Title).IsRequired().HasMaxLength(200);
                b.Property(x => x.IsActive).HasDefaultValue(true);
                b.HasOne(x => x.Department)
                 .WithMany(d => d.Positions)
                 .HasForeignKey(x => x.DepartmentId)
                 .OnDelete(DeleteBehavior.Restrict);
                b.HasIndex(x => new { x.Title, x.DepartmentId });
            });

            // JobApplication
            modelBuilder.Entity<JobApplication>(b =>
            {
                b.ToTable("JobApplications");
                b.HasKey(x => x.Id);
                b.Property(x => x.AppliedAt).HasDefaultValueSql("SYSUTCDATETIME()");
                b.Property<byte[]>("RowVersion").IsRowVersion();
                b.HasOne(x => x.Candidate)
                 .WithMany(c => c.Applications)
                 .HasForeignKey(x => x.CandidateId)
                 .OnDelete(DeleteBehavior.Cascade);
                b.HasOne(x => x.JobPosition)
                 .WithMany(p => p.Applications)
                 .HasForeignKey(x => x.JobPositionId)
                 .OnDelete(DeleteBehavior.Restrict);
                b.HasIndex(x => new { x.CandidateId, x.JobPositionId });
            });

            // Interviewer
            modelBuilder.Entity<Interviewer>(b =>
            {
                b.ToTable("Interviewers");
                b.HasKey(x => x.Id);
                b.Property(x => x.FullName).IsRequired().HasMaxLength(200);
                b.Property(x => x.Email).IsRequired().HasMaxLength(255);
            });

            // Interview
            modelBuilder.Entity<Interview>(b =>
            {
                b.ToTable("Interviews");
                b.HasKey(x => x.Id);
                b.Property(x => x.ScheduledAt).IsRequired();
                b.Property(x => x.Result).IsRequired();
                b.HasOne(x => x.JobApplication)
                 .WithMany()
                 .HasForeignKey(x => x.JobApplicationId)
                 .OnDelete(DeleteBehavior.Cascade);
                b.HasOne(x => x.Interviewer)
                 .WithMany(i => i.Interviews)
                 .HasForeignKey(x => x.InterviewerId)
                 .OnDelete(DeleteBehavior.Restrict);
                b.HasIndex(x => x.ScheduledAt);
            });

            // Test
            modelBuilder.Entity<Test>(b =>
            {
                b.ToTable("Tests");
                b.HasKey(x => x.Id);
                b.Property(x => x.Title).IsRequired().HasMaxLength(250);
            });

            // TestQuestion
            modelBuilder.Entity<TestQuestion>(b =>
            {
                b.ToTable("TestQuestions");
                b.HasKey(x => x.Id);
                b.Property(x => x.Text).IsRequired();
                b.Property<string>("OptionsJson").HasColumnName("OptionsJson");
                b.HasOne(x => x.Test)
                 .WithMany(t => t.Questions)
                 .HasForeignKey(x => x.TestId)
                 .OnDelete(DeleteBehavior.Cascade);
            });

            // TestSubmission
            modelBuilder.Entity<TestSubmission>(b =>
            {
                b.ToTable("TestSubmissions");
                b.HasKey(x => x.Id);
                b.Property(x => x.SubmittedAt).HasDefaultValueSql("SYSUTCDATETIME()");
                b.Property(x => x.Score).HasColumnType("decimal(5,2)");
                b.HasOne(x => x.Test)
                 .WithMany()
                 .HasForeignKey(x => x.TestId)
                 .OnDelete(DeleteBehavior.Restrict);
                b.HasOne(x => x.Candidate)
                 .WithMany(c => c.Submissions)
                 .HasForeignKey(x => x.CandidateId)
                 .OnDelete(DeleteBehavior.Cascade);
            });

            // SubmittedAnswer
            modelBuilder.Entity<SubmittedAnswer>(b =>
            {
                b.ToTable("SubmittedAnswers");
                b.HasKey(x => x.Id);
                b.Property(x => x.AnswerText).HasMaxLength(2000);
                b.HasOne(x => x.TestSubmission)
                 .WithMany(ts => ts.Answers)
                 .HasForeignKey(x => x.TestSubmissionId)
                 .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}

