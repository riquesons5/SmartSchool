using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.Models;
using SmartSchool.WebAPI.v1.Dtos;

namespace SmartSchool.WebAPI.v1.Controllers
{
    /// <summary>
    /// Versão 1 do meu controlador de Alunos
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProfessorController : ControllerBase
    {
        private readonly IRepository _repo;
        private readonly IMapper _mapper;
        public ProfessorController(IRepository repo,
                                   IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var professor = _repo.GetAllProfessores(true);
            return Ok(_mapper.Map<IEnumerable<ProfessorDto>>(professor));
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var professor = _repo.GetProfessorById(id, true);
            if (professor == null) return BadRequest("O professor não foi encontrado.");

            var professorDto = _mapper.Map<ProfessorDto>(professor);

            return Ok(professorDto);
        }

        // [HttpGet("byaluno/{alunoId}")]
        // public IActionResult GetByAlunoId(int alunoId)
        // {
        //     var Professores = _repo.GetProfessoresByAlunoId(alunoId, true);
        //     if (Professores == null) return BadRequest("Professores não encontrados");

        //     return Ok(_mapper.Map<IEnumerable<ProfessorDto>>(Professores));
        // }

        [HttpPost]
        public IActionResult Post(ProfessorRegistrarDto model)
        {
            var professor = _mapper.Map<Professor>(model);

            _repo.Add(professor);

            if (!_repo.SaveChanges())
                return BadRequest("Professor não cadastrado.");

            return Created($"/api/professor/{professor.Id}", _mapper.Map<ProfessorDto>(professor));
        }

        [HttpPut]
        public IActionResult Put(int id, ProfessorRegistrarDto model)
        {
            var professor = _repo.GetProfessorById(id, false);
            if (professor == null) return BadRequest("Professor não encontrado");

            _mapper.Map(model, professor);

            _repo.Update(professor);

            if (!_repo.SaveChanges())
                return BadRequest("Professor não atualizado.");

            return Created($"/api/professor/{professor.Id}", _mapper.Map<ProfessorDto>(professor));
        }

        [HttpPatch]
        public IActionResult Patch(int id, ProfessorRegistrarDto model)
        {
            var professor = _repo.GetProfessorById(id, false);
            if (professor == null) return BadRequest("Professor não encontrado");

            _mapper.Map(model, professor);

            _repo.Update(professor);

            if (!_repo.SaveChanges())
                return BadRequest("Professor não atualizado.");

            return Created($"/api/professor/{professor.Id}", _mapper.Map<ProfessorDto>(professor));
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var prof = _repo.GetProfessorById(id);
            if (prof == null) return BadRequest("Professor não encontrado");

            _repo.Delete(prof);

            if (!_repo.SaveChanges())
                return BadRequest("Professor não deletado.");

            return Ok("Professor removido.");
        }
    }
}