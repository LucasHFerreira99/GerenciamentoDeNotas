using GerenciamentoDeNotas.Enums;
using System.ComponentModel.DataAnnotations;

namespace GerenciamentoDeNotas.Models.Usuario
{
    public class UsuarioSemSenha
    {
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "Digite o nome do usuário!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Digite o login do usuário!")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Digite o CPF do usuário!")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "Digite o e-mail do usuário!")]
        [EmailAddress(ErrorMessage = "O e-mail informado não é valido! ")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Informe o perfil do usuário!")]
        public PerfilEnum? Perfil { get; set; }
    }
}
