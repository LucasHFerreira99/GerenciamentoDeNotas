using GerenciamentoDeNotas.Models;

namespace GerenciamentoDeNotas.DTOs
{
    public class MateriasTurmasListarDto
    {
        public List<TurmaModel> Turmas { get; set; }
        public List<MateriaModel> Materias { get; set; }
        public ProfessorModel Professor { get; set; }
    }
}
