using GerenciamentoDeNotas.Data;
using GerenciamentoDeNotas.Models;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoDeNotas.Services.Nota
{
    public class NotaService : INotaService
    {
        private readonly AppDbContext _context;

        public NotaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<NotaModel>> BuscarNotasPorTurma(int idTurma)
        {
            var notas = _context.Notas.Include(a => a.Aluno).Include(t => t.Materia).Where(a => a.Materia.Id == idTurma).ToList();
            return notas;
        }


        public async Task<int> AtualizarNotas(List<NotaModel> notasAtualizadas)
        {
            foreach(var nota in notasAtualizadas)
            {
                var notaBanco = await _context.Notas.FirstOrDefaultAsync(n => n.Id == nota.Id);

                if (notaBanco == null) throw new Exception("Nota não encontrada");

                notaBanco.Prova1 = nota.Prova1;
                notaBanco.Prova2 = nota.Prova2;
                notaBanco.Prova3 = nota.Prova3;
                notaBanco.Trabalho = nota.Trabalho;

                _context.Notas.Update(notaBanco);

            }
            await _context.SaveChangesAsync();

            var turma = await _context.Notas.Include(a => a.Materia).ThenInclude(t => t.Turma).FirstOrDefaultAsync(p=> p.Id == notasAtualizadas.First().Id);
            return turma.Materia.Id;
        }

        public async Task<bool> AdicionarNotasAAlunoAdicionadoATurma(AlunoModel aluno, TurmaModel turma)
        {

            var MateriasDaTurma = await _context.MateriaProfessor.Where(p=> p.Turma.Id == turma.Id).ToListAsync();

            foreach (var materia in MateriasDaTurma)
            {
                var novaNota = new NotaModel
                {
                    Aluno = aluno,
                    Materia = materia
                };
                _context.Notas.Add(novaNota);
            }
            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<bool> RemoverNotasAAlunoRemovidoDaTurma(AlunoModel aluno, TurmaModel turma)
        {

            var notasDaTurma = await _context.Notas.Where(p=> p.Aluno.Id == aluno.Id).ToListAsync();

            foreach (var notas in notasDaTurma)
            {
                _context.Notas.Remove(notas);
            }
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<NotaModel>> BuscarNotasPorAluno(AlunoModel aluno)
        {
            var notasAluno = await _context.Notas.Include(p => p.Materia).ThenInclude(m=> m.Materia).Where(n=> n.Aluno.Id == aluno.Id).ToListAsync();
            return notasAluno;
        }

        public async Task<NotaModel> BuscarNotasPorId(int idNota)
        {
            var notaBanco = await _context.Notas.Include(p => p.Materia).ThenInclude(m => m.Materia).Include(p=> p.Materia).ThenInclude(p=> p.Professor).FirstOrDefaultAsync(n => n.Id == idNota);
            return notaBanco;
        }

    }
}
