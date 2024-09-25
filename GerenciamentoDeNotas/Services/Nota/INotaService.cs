using GerenciamentoDeNotas.Models;

namespace GerenciamentoDeNotas.Services.Nota
{
    public interface INotaService
    {
        Task<List<NotaModel>> BuscarNotasPorTurma(int idTurma);
        Task<int> AtualizarNotas(List<NotaModel> notasAtualizadas);
        Task<bool> AdicionarNotasAAlunoAdicionadoATurma(AlunoModel aluno, TurmaModel turma);
        Task<bool> RemoverNotasAAlunoRemovidoDaTurma(AlunoModel aluno, TurmaModel turma);
        Task<List<NotaModel>> BuscarNotasPorAluno(AlunoModel aluno);
        Task<NotaModel> BuscarNotasPorId(int idNota);
    }
}
