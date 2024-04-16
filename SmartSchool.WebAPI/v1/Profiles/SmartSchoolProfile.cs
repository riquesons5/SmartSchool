using AutoMapper;
using SmartSchool.WebAPI.Helpers;
using SmartSchool.WebAPI.Models;
using SmartSchool.WebAPI.v1.Dtos;

namespace SmartSchool.WebAPI.v1.Profiles
{
    public class SmartSchoolProfile : Profile
    {
        public SmartSchoolProfile()
        {
            CreateMap<Aluno, AlunoDto>()
                .ForMember(
                    d => d.Nome,
                    o => o.MapFrom(src => $"{src.Nome} {src.SobreNome}")
                )
                .ForMember(
                    d => d.Idade,
                    o => o.MapFrom(src => src.DataNasc.GetCurrentAge())
                );

            CreateMap<AlunoDto, Aluno>();
            CreateMap<Aluno, AlunoRegistrarDto>().ReverseMap();

            CreateMap<Professor, ProfessorDto>()
                .ForMember(
                    dest => dest.Nome,
                    opt => opt.MapFrom(src => $"{src.Nome} {src.Sobrenome}")
                );

            CreateMap<ProfessorDto, Professor>();
            CreateMap<Professor, ProfessorRegistrarDto>().ReverseMap();

            CreateMap<DisciplinaDto, Disciplina>().ReverseMap();
            CreateMap<CursoDto, Curso>().ReverseMap();
        }
    }
}