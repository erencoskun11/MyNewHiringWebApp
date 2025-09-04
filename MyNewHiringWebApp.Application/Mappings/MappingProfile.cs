using AutoMapper;
using MyNewHiringWebApp.Application.DTOs.CandidateDtos;
using MyNewHiringWebApp.Application.DTOs.CandidateSkillDtos;
using MyNewHiringWebApp.Application.DTOs.DepartmentDtos;
using MyNewHiringWebApp.Application.DTOs.InterviesDtos;
using MyNewHiringWebApp.Application.DTOs.InterviewDtos;
using MyNewHiringWebApp.Application.DTOs.InterviewerDtos;
using MyNewHiringWebApp.Application.DTOs.JobApplicationDtos;
using MyNewHiringWebApp.Application.DTOs.JobPositionDtos;
using MyNewHiringWebApp.Application.DTOs.SkillDtos;
using MyNewHiringWebApp.Application.DTOs.SubmittedAnswerCreateDtos;
using MyNewHiringWebApp.Application.DTOs.TestDtos;
using MyNewHiringWebApp.Application.DTOs.TestQuestionDtos;
using MyNewHiringWebApp.Application.Eto;
using MyNewHiringWebApp.Application.ETOs.CandidateEtos;
using MyNewHiringWebApp.Domain.Entities;
using System.Linq;

namespace MyNewHiringWebApp.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // ---------- Candidate ----------
            CreateMap<CandidateCreateDto, Candidate>()
                .ForMember(dest => dest.Resume, opt => opt.MapFrom(src =>
                    src == null ? null : new ResumeSummary(src.ResumeSummary, src.GithubUrl, src.LinkedInUrl)))
                .ForMember(dest => dest.CandidateSkills, opt => opt.Ignore());

            CreateMap<CandidateUpdateDto, Candidate>()
                .ForMember(dest => dest.Resume, opt => opt.MapFrom(src =>
                    src == null ? null : new ResumeSummary(src.ResumeSummary, src.GithubUrl, src.LinkedInUrl)))
                .ForMember(dest => dest.CandidateSkills, opt => opt.Ignore());

            CreateMap<Candidate, CandidateDto>()
                .ForMember(dest => dest.ResumeSummary, opt => opt.MapFrom(src => src.Resume != null ? src.Resume.Summary : null))
                .ForMember(dest => dest.GithubUrl, opt => opt.MapFrom(src => src.Resume != null ? src.Resume.GithubUrl : null))
                .ForMember(dest => dest.LinkedInUrl, opt => opt.MapFrom(src => src.Resume != null ? src.Resume.LinkedInUrl : null))
                .ForMember(dest => dest.Skills,
                    opt => opt.MapFrom(src =>
                        src.CandidateSkills == null
                            ? null
                            : src.CandidateSkills.Select(cs => new CandidateSkillDto
                            {
                                SkillName = cs.Skill != null ? cs.Skill.Name : string.Empty,
                                Level = cs.Level
                            }).ToList()
                    ));

            CreateMap<CandidateSkillCreateDto, CandidateSkill>().ReverseMap();
            CreateMap<CandidateSkillUpdateDto, CandidateSkill>().ReverseMap();

            CreateMap<CandidateSkill, CandidateSkillDto>()
                .ForMember(d => d.SkillName, opt => opt.MapFrom(s => s.Skill != null ? s.Skill.Name : string.Empty))
                .ForMember(d => d.Level, opt => opt.MapFrom(s => s.Level));

            // Candidate -> ETO for RabbitMQ
            CreateMap<Candidate, CandidateCreatedEto>()
                .ForMember(dest => dest.CandidateId, opt => opt.MapFrom(src => src.Id));

            // ---------- Department ----------
            CreateMap<DepartmentCreateDto, Department>().ReverseMap();
            CreateMap<DepartmentUpdateDto, Department>().ReverseMap();
            CreateMap<Department, DepartmentDto>().ReverseMap();

            // ---------- Interview ----------
            CreateMap<InterviewCreateDto, Interview>().ReverseMap();
            CreateMap<InterviewUpdateDto, Interview>().ReverseMap();
            CreateMap<Interview, InterviewDto>()
                .ForMember(d => d.JobApplicationTitle, opt => opt.MapFrom(src => src.JobApplication != null ? src.JobApplication.JobPosition.Title : null))
                .ForMember(d => d.InterviewerName, opt => opt.MapFrom(src => src.Interviewer != null ? src.Interviewer.FullName : null));

            // ---------- Interviewer ----------
            CreateMap<InterviewerCreateDto, Interviewer>().ReverseMap();
            CreateMap<InterviewerUpdateDto, Interviewer>().ReverseMap();
            CreateMap<Interviewer, InterviewerDto>()
                .ForMember(d => d.InterviewCount, opt => opt.MapFrom(src => src.Interviews != null ? src.Interviews.Count : 0));

            // ---------- JobApplication ----------
            CreateMap<JobApplicationCreateDto, JobApplication>().ReverseMap();
            CreateMap<JobApplicationUpdateDto, JobApplication>().ReverseMap();
            CreateMap<JobApplication, JobApplicationDto>()
                .ForMember(dest => dest.CandidateName,
                    opt => opt.MapFrom(src => src.Candidate != null ? src.Candidate.FirstName + " " + src.Candidate.LastName : null))
                .ForMember(dest => dest.JobPositionTitle,
                    opt => opt.MapFrom(src => src.JobPosition != null ? src.JobPosition.Title : null));

            // ---------- JobPosition ----------
            CreateMap<JobPositionCreateDto, JobPosition>().ReverseMap();
            CreateMap<JobPositionUpdateDto, JobPosition>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<JobPosition, JobPositionDto>()
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department != null ? src.Department.Name : null))
                .ReverseMap();

            // ---------- Skill ----------
            CreateMap<SkillCreateDto, Skill>().ReverseMap();
            CreateMap<SkillUpdateDto, Skill>().ReverseMap();
            CreateMap<Skill, SkillDto>()
                .ForMember(dest => dest.CandidateCount, opt => opt.MapFrom(src => src.CandidateSkills != null ? src.CandidateSkills.Count : 0))
                .ReverseMap();

            // ---------- SubmittedAnswer ----------
            CreateMap<SubmittedAnswerCreateDto, SubmittedAnswer>()
                .ConstructUsing(dto => new SubmittedAnswer(dto.QuestionId, dto.SelectedOptionIndex, dto.AnswerText));
            CreateMap<SubmittedAnswerUpdateDto, SubmittedAnswer>().ReverseMap();
            CreateMap<SubmittedAnswer, SubmittedAnswerDto>().ReverseMap();

            // ---------- Test ----------
            CreateMap<TestCreateDto, Test>().ConstructUsing(dto => new Test(dto.Title, dto.Description)).ReverseMap();
            CreateMap<TestUpdateDto, Test>().ReverseMap();
            CreateMap<Test, TestDto>().ReverseMap();

            // ---------- TestQuestion ----------
            CreateMap<TestQuestionCreateDto, TestQuestion>()
                .ConstructUsing(dto => new TestQuestion(dto.Text, dto.Type, dto.OptionsJson, dto.CorrectOptionIndex))
                .ReverseMap();
            CreateMap<TestQuestionUpdateDto, TestQuestion>().ReverseMap();
            CreateMap<TestQuestion, TestQuestionDto>().ReverseMap();
        }
    }
}

