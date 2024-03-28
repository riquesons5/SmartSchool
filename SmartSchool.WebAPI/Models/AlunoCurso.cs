namespace SmartSchool.WebAPI.Models
{
    public class AlunoCurso
    {
        public AlunoCurso() { }
        public AlunoCurso(int alunoId, int cursoId) 
        {
            this.AlunoId = alunoId;
            this.CursoId = cursoId;
        }
        public int AlunoId { get; set; }
        public int CursoId { get; set; }
        public DateTime DataIni { get; set; } = DateTime.Now;
        public DateTime? DataFim { get; set; }
        public int? Nota { get; set; } = null;

        public Aluno Aluno { get; set; }
        public Curso Disciplina { get; set; }
    }
}