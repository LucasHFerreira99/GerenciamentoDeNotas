using GerenciamentoDeNotas.DTOs;
using GerenciamentoDeNotas.Models;

namespace GerenciamentoDeNotas.Services.Turma
{
    public interface ITurmaService
    {
        Task<List<TurmaModel>> GetTurmas();
        Task<TurmaModel> GetTurmaById(int id);
        Task<TurmaModel> CriarTurma(TurmaDto turmaDto);
        Task<TurmaModel> AdicionarAlunosATurma(int idALuno, int idTurma);
        Task<TurmaModel> RemoverAlunoDaTurma(int idAluno, int idTurma);
        Task<TurmaModel> RemoverTurma(int idTurma);
        Task<TurmaModel> EditarTurma(TurmaEditarDto turmaEditarDto);
    }
}
