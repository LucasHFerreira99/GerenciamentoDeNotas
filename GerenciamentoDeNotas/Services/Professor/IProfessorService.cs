using GerenciamentoDeNotas.Models;
using GerenciamentoDeNotas.Models.Usuario;

namespace GerenciamentoDeNotas.Services.Professor
{
    public interface IProfessorService
    {
        Task<ProfessorModel> BuscarProfessor(UsuarioModel usuarioProfessor);
        Task<List<MateriaProfessorModel>> BuscarProfessorTurmasEMaterias(UsuarioModel usuarioProfessor);
        Task<TurmaModel> BuscarTurma(int id);
        Task<ProfessorModel> CadastrarProfessor(UsuarioModel professorCadastro);
        Task<ProfessorModel> EditarProfessor(UsuarioModel professorEditado, string login);
        Task<List<ProfessorModel>> BuscarTodosProfessores();
        Task<ProfessorModel> BuscarProfessorPorId(int id);
    }
}
