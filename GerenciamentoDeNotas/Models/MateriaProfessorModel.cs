namespace GerenciamentoDeNotas.Models
{
    public class MateriaProfessorModel
    {
        public int Id { get; set; }
        public MateriaModel Materia { get; set; }
        public ProfessorModel Professor { get; set; }
        public TurmaModel Turma { get; set; }
    }
}
