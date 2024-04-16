using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.Models;
using SmartSchool.WebAPI.v2.Dtos;

namespace SmartSchool.WebAPI.v2.Controllers
{
    /// <summary>
    /// Versão 2 do meu controlador de Alunos
    /// </summary>
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AlunoController : ControllerBase
    {
        private readonly IRepository _repo;
        private readonly IMapper _mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="mapper"></param>
        public AlunoController(IRepository repo,
                               IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        /// <summary>
        /// Método responsável para retornar todos os meus alunos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            var alunos = _repo.GetAllAlunos(true);

            return Ok(_mapper.Map<IEnumerable<AlunoDto>>(alunos));
        }

        /// <summary>
        /// Método responsável por retornar apenas um aluno por meio do Código ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var aluno = _repo.GetAlunoById(id, false);

            if (aluno == null)
                return BadRequest("O aluno não foi encontrado.");

            var alunoDto = _mapper.Map<AlunoDto>(aluno);

            return Ok(alunoDto);
        }

        [HttpPost]
        public IActionResult Post(AlunoRegistrarDto model)
        {
            var aluno = _mapper.Map<Aluno>(model);

            _repo.Add(aluno);

            if (!_repo.SaveChanges())
                return BadRequest("Aluno não cadastrado.");

            return Created($"/api/aluno/{aluno.Id}", _mapper.Map<AlunoDto>(aluno));
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, AlunoRegistrarDto model)
        {
            var aluno = _repo.GetAlunoById(id);
            if (aluno == null) return BadRequest("Aluno não encontrado.");

            _mapper.Map(model, aluno);

            _repo.Update(aluno);

            if (!_repo.SaveChanges())
                return BadRequest("Aluno não atualizado.");

            return Created($"/api/aluno/{aluno.Id}", _mapper.Map<AlunoDto>(aluno));
        }

        
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var aluno = _repo.GetAlunoById(id);
            if (aluno == null) return BadRequest("Aluno não encontrado.");

            _repo.Delete(aluno);

            if (!_repo.SaveChanges())
                return BadRequest("Aluno não deletado.");

            return Ok("Aluno deletado");
        }
    }
}