using Microsoft.AspNetCore.Mvc;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfessorController : ControllerBase
    {
        private readonly IRepository _repo;
        public ProfessorController(IRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _repo.GetAllProfessores(true);
            return Ok(result);
        }

        [HttpGet("ById/{id}")]
        public IActionResult GetById(int id)
        {
            var professor = _repo.GetProfessorById(id);
            if(professor == null) return BadRequest("O professor não foi encontrado.");

            return Ok(professor);
        }

        [HttpPost]
        public IActionResult Post(Professor professor)
        {
            _repo.Add(professor);
            
            if(!_repo.SaveChanges())
                return BadRequest("Professor não cadastrado.");

            return Ok(professor);
        }

        [HttpPut]
        public IActionResult Put(int id, Professor professor)
        {
            var prof = _repo.GetProfessorById(id);
            if(prof == null) return BadRequest("Professor não encontrado");

            _repo.Update(professor);
            
            if(!_repo.SaveChanges())
                return BadRequest("Aluno não atualizado.");
                
            return Ok(professor);
        }

        [HttpPatch]
        public IActionResult Patch(int id, Professor professor)
        {
            var prof = _repo.GetProfessorById(id);
            if(prof == null) return BadRequest("Professor não encontrado");

            _repo.Update(professor);
            
            if(!_repo.SaveChanges())
                return BadRequest("Aluno não atualizado.");
                
            return Ok(professor);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var prof = _repo.GetProfessorById(id);
            if(prof == null) return BadRequest("Professor não encontrado");

            _repo.Delete(prof);
            
            if(!_repo.SaveChanges())
                return BadRequest("Professor não deletado.");

            return Ok("Professor removido.");
        }
    }
}