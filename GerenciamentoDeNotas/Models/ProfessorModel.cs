using GerenciamentoDeNotas.Models.Usuario;
using System.ComponentModel.DataAnnotations;

namespace GerenciamentoDeNotas.Models
{
    public class ProfessorModel 
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Digite o nome do usuário!")]
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string CPF { get; set; } = string.Empty;
        public List<MateriaProfessorModel>? MateriasETurmas { get; set; }
    }
}
