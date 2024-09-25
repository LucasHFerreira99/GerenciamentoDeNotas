using GerenciamentoDeNotas.Models;

namespace GerenciamentoDeNotas.DTOs
{
    public class MateriaProfessorCriarDto
    {
        public int IdMateria { get; set; }
        public int IdProfessor { get; set; }
        public int IdTurma { get; set; }
    }
}
