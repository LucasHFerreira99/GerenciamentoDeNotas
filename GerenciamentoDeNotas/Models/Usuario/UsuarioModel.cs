using GerenciamentoDeNotas.Enums;
using GerenciamentoDeNotas.Helper;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GerenciamentoDeNotas.Models.Usuario
{
    public class UsuarioModel
    {
        [Key]
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "Digite o nome do usuário!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Digite o login do usuário!")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Digite a senha do usuário!")]
        public string Senha { get; set; }

        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "Digite o e-mail do usuário!")]
        [EmailAddress(ErrorMessage = "O e-mail informado não é valido! ")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Informe o perfil do usuário!")]
        public PerfilEnum? Perfil { get; set; }

        [Display(Name = "Data de cadastro")]
        public DateTime DataCadastro { get; set; } = DateTime.Now;

        [Display(Name = "Data de atualização")]
        public DateTime? DataAtualizacao { get; set; }

        [Display(Name = "CPF")]
        [Required(ErrorMessage = "Digite o CPF do usuário!")]
        public string Cpf { get; set; }

        //public virtual AlunoModel Aluno { get; set; }
        //public virtual ProfessorModel Professor { get; set; }


        public bool SenhaValida(string senha)
        {
            return Senha == senha.GerarHash();
        }

        public void SetSenhaHash()
        {
            Senha = Senha.GerarHash();
        }

        public void SetNovaSenha(string novaSenha)
        {
            Senha = novaSenha.GerarHash();
        }
        public string GerarNovaSenha()
        {
            string novaSenha = Guid.NewGuid().ToString().Substring(0, 8);
            Senha = novaSenha.GerarHash();
            return novaSenha;
        }

    }
}
