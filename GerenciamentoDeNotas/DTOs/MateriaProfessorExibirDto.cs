using GerenciamentoDeNotas.Models;

namespace GerenciamentoDeNotas.DTOs
{
    public class MateriaProfessorExibirDto
    {
        public List<MateriaProfessorModel> MateriaProfessor { get; set; }
        public ProfessorModel Professor { get; set; }
    }
}
