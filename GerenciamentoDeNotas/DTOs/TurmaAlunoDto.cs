using GerenciamentoDeNotas.Models;

namespace GerenciamentoDeNotas.DTOs
{
    public class TurmaAlunoDto
    {
        public int idTurma { get; set; }
        public List<AlunoModel> Alunos { get; set; }

    }
}
