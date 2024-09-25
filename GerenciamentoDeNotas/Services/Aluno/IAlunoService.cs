using GerenciamentoDeNotas.Models.Usuario;
using GerenciamentoDeNotas.Models;
using GerenciamentoDeNotas.Data;

namespace GerenciamentoDeNotas.Services.Aluno
{
    public interface IAlunoService
    {
        Task<AlunoModel> CadastrarAluno(UsuarioModel alunoCadastro);
        Task<AlunoModel> EditarAluno(UsuarioModel alunoEditado, string login);
        Task<List<AlunoModel>> GetAlunos();
        Task<List<AlunoModel>> AlunosSemTurma();
        Task<AlunoModel> GetAlunoById(int id);
        Task<AlunoModel> GetAlunoByUsuario(UsuarioModel usuario);
        Task<UsuarioModel> GetUsuarioPeloAluno(AlunoModel aluno);
    }
}
