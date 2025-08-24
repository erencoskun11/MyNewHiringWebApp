
using AutoMapper;
using MyNewHiringWebApp.Application.DTOs.CandidateDtos;
using MyNewHiringWebApp.Application.DTOs.CandidateSkillDtos;
using MyNewHiringWebApp.Application.DTOs.DepartmentDtos;
using MyNewHiringWebApp.Application.DTOs.InterviewDtos;
using MyNewHiringWebApp.Application.DTOs.InterviewerDtos;
using MyNewHiringWebApp.Application.DTOs.JobApplicationDtos;
using MyNewHiringWebApp.Application.DTOs.JobPositionDtos;
using MyNewHiringWebApp.Application.DTOs.SkillDtos;
using MyNewHiringWebApp.Application.DTOs.SubmittedAnswerCreateDtos;
using MyNewHiringWebApp.Application.DTOs.TestDtos;
using MyNewHiringWebApp.Application.DTOs.TestQuestionDtos;
using MyNewHiringWebApp.Domain.Entities;
using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using MyNewHiringWebApp.Application.DTOs.InterviesDtos;
using CandidateSkillDto = MyNewHiringWebApp.Application.DTOs.CandidateSkillDtos.CandidateSkillDto;

namespace MyNewHiringWebApp.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // -------------------------
            // CandidateSkill mappings (define before Candidate so AutoMapper can map collections)
            // -------------------------
            var csMap = CreateMap<CandidateSkillCreateDto, CandidateSkill>();
            csMap.ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
            csMap.ForMember(dest => dest.Skill, opt => opt.Ignore());
            csMap.ForMember(dest => dest.Candidate, opt => opt.Ignore());

            var csUpdateMap = CreateMap<CandidateSkillUpdateDto, CandidateSkill>();
            csUpdateMap.ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<CandidateSkill, CandidateSkillDto>()
                .ForMember(d => d.SkillName, opt => opt.MapFrom(s => s.Skill != null ? s.Skill.Name : string.Empty))
                .ForMember(d => d.Level, opt => opt.MapFrom(s => s.Level));

            // -------------------------
            // Candidate mappings
            // -------------------------
            CreateMap<Candidate, CandidateDto>()
                // MapFrom overload + fallback to empty string so DTO never has null for these fields
                .ForMember(d => d.ResumeSummary,
                           opt => opt.MapFrom((src, dest) => ResolveResumeSummary(src?.Resume) ?? string.Empty))
                .ForMember(d => d.GithubUrl,
                           opt => opt.MapFrom((src, dest) => ResolveGithub(src?.Resume) ?? string.Empty))
                .ForMember(d => d.LinkedInUrl,
                           opt => opt.MapFrom((src, dest) => ResolveLinkedIn(src?.Resume) ?? string.Empty))
                // let AutoMapper map the CandidateSkills collection using the map we defined above
                .ForMember(d => d.Skills,
                           opt => opt.MapFrom(src => src.CandidateSkills ?? new List<CandidateSkill>()));

            // Create map for create/update DTOs
            CreateMap<CandidateCreateDto, Candidate>()
                .ConstructUsing((src, ctx) => new Candidate(src.FirstName, src.LastName, src.Email))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
                .AfterMap((dto, candidate) =>
                {
                    // Try to call domain SetResume if present; otherwise create Resume object and set properties by reflection
                    if (!TryInvokeSetResume(candidate, dto.ResumeSummary, dto.GithubUrl, dto.LinkedInUrl))
                    {
                        var resumeObj = EnsureResumeInstance(candidate);
                        if (resumeObj != null)
                        {
                            SetResumeProperty(resumeObj, new[] { "Summary", "ResumeSummary" }, dto.ResumeSummary);
                            SetResumeProperty(resumeObj, new[] { "Github", "GithubUrl", "GitHub" }, dto.GithubUrl);
                            SetResumeProperty(resumeObj, new[] { "LinkedIn", "LinkedInUrl", "LinkedInProfile" }, dto.LinkedInUrl);
                        }
                    }
                });

            var candidateUpdateMap = CreateMap<CandidateUpdateDto, Candidate>();
            candidateUpdateMap.ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
            candidateUpdateMap.AfterMap((dto, candidate) =>
            {
                if (!TryInvokeSetResume(candidate, dto.ResumeSummary, dto.GithubUrl, dto.LinkedInUrl))
                {
                    var resumeObj = EnsureResumeInstance(candidate);
                    if (resumeObj != null)
                    {
                        if (dto.ResumeSummary != null || dto.GithubUrl != null || dto.LinkedInUrl != null)
                        {
                            SetResumeProperty(resumeObj, new[] { "Summary", "ResumeSummary" },
                                dto.ResumeSummary ?? GetResumeValue(resumeObj, "Summary", "ResumeSummary"));

                            SetResumeProperty(resumeObj, new[] { "Github", "GithubUrl", "GitHub" },
                                dto.GithubUrl ?? GetResumeValue(resumeObj, "Github", "GithubUrl", "GitHub"));

                            SetResumeProperty(resumeObj, new[] { "LinkedIn", "LinkedInUrl", "LinkedInProfile" },
                                dto.LinkedInUrl ?? GetResumeValue(resumeObj, "LinkedIn", "LinkedInUrl", "LinkedInProfile"));
                        }
                    }
                }
            });

            // -------------------------
            // Department mappings
            // -------------------------
            CreateMap<Department, DepartmentDto>();
            CreateMap<DepartmentCreateDto, Department>().ReverseMap();
            CreateMap<DepartmentUpdateDto, Department>().ReverseMap();

            // -------------------------
            // Interview + Interviewer mappings
            // -------------------------
            CreateMap<Interview, InterviewDto>()
                .ForMember(d => d.JobApplicationTitle,
                           opt => opt.MapFrom(src => src.JobApplication != null && src.JobApplication.JobPosition != null ? src.JobApplication.JobPosition.Title : null))
                .ForMember(d => d.InterviewerName,
                           opt => opt.MapFrom(src => src.Interviewer != null ? src.Interviewer.FullName : null));

            // DTO'larda eksik alan olabilir, o yüzden sadece null olmayanları uygula
            var interviewCreateMap = CreateMap<InterviewCreateDto, Interview>();
            interviewCreateMap.ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            var interviewUpdateMap = CreateMap<InterviewUpdateDto, Interview>();
            interviewUpdateMap.ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<Interviewer, InterviewerDto>()
                .ForMember(d => d.InterviewCount, opt => opt.MapFrom(src => src.Interviews != null ? src.Interviews.Count : 0));

            CreateMap<InterviewerCreateDto, Interviewer>().ReverseMap();
            CreateMap<InterviewerUpdateDto, Interviewer>().ReverseMap();

            // -------------------------
            // JobApplication & JobPosition
            // -------------------------
            CreateMap<JobApplication, JobApplicationDto>()
                .ForMember(dest => dest.CandidateName,
                           opt => opt.MapFrom(src => src.Candidate != null ? src.Candidate.FirstName + " " + src.Candidate.LastName : null))
                .ForMember(dest => dest.JobPositionTitle,
                           opt => opt.MapFrom(src => src.JobPosition != null ? src.JobPosition.Title : null));

            CreateMap<JobApplicationCreateDto, JobApplication>().ReverseMap();

            var jobAppUpdateMap = CreateMap<JobApplicationUpdateDto, JobApplication>();
            jobAppUpdateMap.ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
            jobAppUpdateMap.ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));

            CreateMap<JobPosition, JobPositionDto>()
                .ForMember(dest => dest.DepartmentName,
                           opt => opt.MapFrom(src => src.Department != null ? src.Department.Name : null));

            CreateMap<JobPositionCreateDto, JobPosition>().ReverseMap();

            var jobPosUpdateMap = CreateMap<JobPositionUpdateDto, JobPosition>();
            jobPosUpdateMap.ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            // -------------------------
            // Skill mappings
            // -------------------------
            CreateMap<Skill, SkillDto>()
                .ForMember(dest => dest.CandidateCount, opt => opt.MapFrom(src => src.CandidateSkills != null ? src.CandidateSkills.Count : 0));

            CreateMap<SkillCreateDto, Skill>().ReverseMap();
            CreateMap<SkillUpdateDto, Skill>().ReverseMap();

            // -------------------------
            // SubmittedAnswer mappings
            // -------------------------
            CreateMap<SubmittedAnswerCreateDto, SubmittedAnswer>()
                .ConstructUsing((dto, ctx) => new SubmittedAnswer(dto.QuestionId, dto.SelectedOptionIndex, dto.AnswerText));

            var submittedAnswerUpdateMap = CreateMap<SubmittedAnswerUpdateDto, SubmittedAnswer>();
            submittedAnswerUpdateMap.ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<SubmittedAnswer, SubmittedAnswerDto>();

            // -------------------------
            // Test mappings
            // -------------------------
            CreateMap<Test, TestDto>();
            CreateMap<TestCreateDto, Test>()
                .ConstructUsing((dto, ctx) => new Test(dto.Title, dto.Description));

            var testUpdateMap = CreateMap<TestUpdateDto, Test>();
            testUpdateMap.ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            // -------------------------
            // TestQuestion mappings
            // -------------------------
            CreateMap<TestQuestion, TestQuestionDto>();
            CreateMap<TestQuestionCreateDto, TestQuestion>()
                .ConstructUsing((dto, ctx) => new TestQuestion(dto.Text, dto.Type, dto.OptionsJson, dto.CorrectOptionIndex));

            var testQuestionUpdateMap = CreateMap<TestQuestionUpdateDto, TestQuestion>();
            testQuestionUpdateMap.ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        }

        // ---------- Helper RESOLVERS (MapFrom içinde method call kısıtı için) ----------
        private static string? ResolveResumeSummary(object? resumeObj)
            => GetResumeValue(resumeObj, "Summary", "ResumeSummary");

        private static string? ResolveGithub(object? resumeObj)
            => GetResumeValue(resumeObj, "Github", "GithubUrl", "GitHub");

        private static string? ResolveLinkedIn(object? resumeObj)
            => GetResumeValue(resumeObj, "LinkedIn", "LinkedInUrl", "LinkedInProfile");

        // ---------- Helper methods using reflection ----------
        private static string? GetResumeValue(object? resumeObj, params string[] propertyNames)
        {
            if (resumeObj == null) return null;
            var t = resumeObj.GetType();
            foreach (var name in propertyNames)
            {
                var p = t.GetProperty(name, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                if (p != null)
                {
                    var v = p.GetValue(resumeObj);
                    if (v != null) return v.ToString();
                }
            }
            return null;
        }

        private static void SetResumeProperty(object resumeObj, string[] propertyNames, string? value)
        {
            if (resumeObj == null) return;
            var t = resumeObj.GetType();
            foreach (var name in propertyNames)
            {
                var p = t.GetProperty(name, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                if (p != null && p.CanWrite)
                {
                    if (value == null)
                    {
                        if (p.PropertyType == typeof(string))
                        {
                            p.SetValue(resumeObj, null);
                        }
                    }
                    else
                    {
                        if (p.PropertyType == typeof(string))
                        {
                            p.SetValue(resumeObj, value);
                        }
                        else
                        {
                            try
                            {
                                var conv = Convert.ChangeType(value, p.PropertyType);
                                p.SetValue(resumeObj, conv);
                            }
                            catch
                            {
                                // ignore conversion failures
                            }
                        }
                    }
                    break;
                }
            }
        }

        private static object? EnsureResumeInstance(object candidate)
        {
            if (candidate == null) return null;
            var candidateType = candidate.GetType();
            var resumeProp = candidateType.GetProperty("Resume", BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
            if (resumeProp == null) return null;

            var current = resumeProp.GetValue(candidate);
            if (current != null) return current;

            var resumeType = resumeProp.PropertyType ?? candidateType.Assembly.GetTypes().FirstOrDefault(t => string.Equals(t.Name, "ResumeSummary", StringComparison.OrdinalIgnoreCase));
            if (resumeType == null) return null;

            try
            {
                var instance = Activator.CreateInstance(resumeType);
                if (resumeProp.CanWrite)
                {
                    resumeProp.SetValue(candidate, instance);
                }
                return instance;
            }
            catch
            {
                return null;
            }
        }

        private static bool TryInvokeSetResume(object candidate, string? summary, string? github, string? linkedIn)
        {
            if (candidate == null) return false;
            var type = candidate.GetType();
            var setMethod = type.GetMethod("SetResume", BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
            if (setMethod == null) return false;

            try
            {
                var parameters = setMethod.GetParameters();
                if (parameters.Length == 3)
                {
                    setMethod.Invoke(candidate, new object?[] { summary, github, linkedIn });
                    return true;
                }
                if (parameters.Length == 1)
                {
                    var resumeType = parameters[0].ParameterType;
                    var resumeInstance = Activator.CreateInstance(resumeType);
                    if (resumeInstance != null)
                    {
                        SetResumeProperty(resumeInstance, new[] { "Summary", "ResumeSummary" }, summary);
                        SetResumeProperty(resumeInstance, new[] { "Github", "GithubUrl", "GitHub" }, github);
                        SetResumeProperty(resumeInstance, new[] { "LinkedIn", "LinkedInUrl", "LinkedInProfile" }, linkedIn);
                        setMethod.Invoke(candidate, new object?[] { resumeInstance });
                        return true;
                    }
                }
            }
            catch
            {
                // ignore
            }
            return false;
        }
    }
}
