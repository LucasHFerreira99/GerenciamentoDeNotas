using GerenciamentoDeNotas.Models.Usuario;

namespace GerenciamentoDeNotas.Models
{
    public class AlunoModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }
        public TurmaModel? Turma { get; set; }
        public ICollection<NotaModel>? Notas { get; set; }
    }
}
