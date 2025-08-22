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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MyNewHiringWebApp.Application.Mappings
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            //Candidate
            CreateMap<CandidateCreateDto, Candidate>().ReverseMap();
            CreateMap<CandidateUpdateDto, Candidate>().ReverseMap();
            CreateMap<CandidateDto, Candidate>().ReverseMap();

            //CandidateSkill
            CreateMap<CandidateSkillCreateDto, CandidateSkill>().ReverseMap();
            CreateMap<CandidateSkillUpdateDto, CandidateSkill>().ReverseMap();
            CreateMap<DTOs.CandidateDtos.CandidateSkillDto, CandidateSkill>().ReverseMap();


            CreateMap<DepartmentCreateDto,Department>().ReverseMap();
            CreateMap<DepartmentUpdateDto, Department>().ReverseMap();
            CreateMap<DepartmentDto, Department>().ReverseMap();

            CreateMap<InterviewerCreateDto, Interviewer>().ReverseMap();
            CreateMap<InterviewerUpdateDto, Interviewer>().ReverseMap();
            CreateMap<InterviewerDto, Interviewer>().ReverseMap();

      
            
            
            
            
            
            
            
            
            
            
            
            
            
            // Interview ↔ DTO
            CreateMap<Interview, InterviewDto>()
                .ForMember(d => d.JobApplicationTitle,
                           opt => opt.MapFrom(src => src.JobApplication.JobPosition.Title)) // Örnek
                .ForMember(d => d.InterviewerName,
                           opt => opt.MapFrom(src => src.Interviewer.FullName));

            CreateMap<InterviewerCreateDto, Interview>().ReverseMap();
            CreateMap<InterviewUpdateDto, Interview>().ReverseMap();

            // Interviewer ↔ DTO
            CreateMap<Interviewer, InterviewerDto>()
                .ForMember(d => d.InterviewCount,
                           opt => opt.MapFrom(src => src.Interviews.Count));

            CreateMap<InterviewerCreateDto, Interviewer>();
            CreateMap<InterviewerUpdateDto, Interviewer>();
        
            //JobApplication
            CreateMap<JobApplicationCreateDto, JobApplication>().ReverseMap();
            CreateMap<JobApplicationUpdateDto, JobApplication>()
       .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));

            CreateMap<JobApplication, JobApplicationDto>()
                .ForMember(dest => dest.CandidateName,
                           opt => opt.MapFrom(src => src.Candidate.FirstName + " " + src.Candidate.LastName))
                .ForMember(dest => dest.JobPositionTitle,
                           opt => opt.MapFrom(src => src.JobPosition.Title));




            //JobPosition mappings
            CreateMap<JobPositionCreateDto, JobPosition>().ReverseMap();

            CreateMap<JobPositionUpdateDto, JobPosition>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<JobPositionDto,JobPosition >().ReverseMap()
                .ForMember(dest => dest.DepartmentName,
                opt =>opt.MapFrom(src => src.Department.Name));




            // Skill mappings
            CreateMap<SkillCreateDto, Skill>();
            CreateMap<SkillUpdateDto, Skill>();

            CreateMap<Skill, SkillDto>()
                .ForMember(dest => dest.CandidateCount,
                           opt => opt.MapFrom(src => src.CandidateSkills.Count));




            // SubmittedAnswer mappings
            CreateMap<SubmittedAnswerCreateDto, SubmittedAnswer>()
                .ConstructUsing(dto => new SubmittedAnswer(dto.QuestionId, dto.SelectedOptionIndex, dto.AnswerText));

            CreateMap<SubmittedAnswerUpdateDto, SubmittedAnswer>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<SubmittedAnswer, SubmittedAnswerDto>();

            // Test mappings
            CreateMap<TestCreateDto, Test>()
                .ConstructUsing(dto => new Test(dto.Title, dto.Description));

            CreateMap<TestUpdateDto, Test>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<Test, TestDto>();


            // TestQuestion mappings
            CreateMap<TestQuestionCreateDto, TestQuestion>()
                .ConstructUsing(dto => new TestQuestion(dto.Text, dto.Type, dto.OptionsJson, dto.CorrectOptionIndex));

            CreateMap<TestQuestionUpdateDto, TestQuestion>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<TestQuestion, TestQuestionDto>();


        }

    }
}
