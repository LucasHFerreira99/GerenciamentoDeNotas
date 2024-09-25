using GerenciamentoDeNotas.Data;
using GerenciamentoDeNotas.DTOs;
using GerenciamentoDeNotas.Models;
using GerenciamentoDeNotas.Services.Nota;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoDeNotas.Services.Turma
{
    public class TurmaService : ITurmaService
    {
        private readonly AppDbContext _context;
        private readonly INotaService _notaService;

        public TurmaService(AppDbContext context, INotaService notaService)
        {
            _context = context;
            _notaService = notaService;
        }

        public async Task<TurmaModel> AdicionarAlunosATurma(int idALuno, int idTurma)
        {
            var turma = await  _context.Turmas.FirstOrDefaultAsync(t=> t.Id == idTurma);
            if (turma == null) throw new Exception("Turma não encontrada");

            var aluno = await _context.Alunos.FirstOrDefaultAsync(a => a.Id == idALuno);
            if (aluno == null) throw new Exception("Aluno não encontrada");

            if(turma.Alunos is null) turma.Alunos = new List<AlunoModel>();


            turma.Alunos.Add(aluno);
            aluno.Turma = turma;
            _context.Alunos.Update(aluno);
            _context.Turmas.Update(turma);
            await _context.SaveChangesAsync();

            await _notaService.AdicionarNotasAAlunoAdicionadoATurma(aluno, turma);
            return turma;
        }

        public async Task<TurmaModel> CriarTurma(TurmaDto turmaDto)
        {
            var turma = new TurmaModel
            {
                Nome = turmaDto.Nome,
                DataDeInicio = turmaDto.DataDeInicio
            };

            _context.Turmas.Add(turma);
            await _context.SaveChangesAsync();
            return turma;
        }

        public async Task<TurmaModel> EditarTurma(TurmaEditarDto turmaEditarDto)
        {
            var turma = await _context.Turmas.FirstOrDefaultAsync(t=> t.Id == turmaEditarDto.Id);
            
            if (turma == null) throw new Exception("Não existe essa turma");

            turma.Nome = turmaEditarDto.Nome;
            turma.DataDeInicio = turmaEditarDto.DataDeInicio;

            _context.Turmas.Update(turma);
            await _context.SaveChangesAsync();
            return turma;
        }

        public async Task<TurmaModel> GetTurmaById(int id)
        {
            var turma = await _context.Turmas.Include(a=>a.Alunos).FirstOrDefaultAsync(turmaBanco=> turmaBanco.Id == id);
            if (turma == null) throw new Exception("Turma não encontrada");
            return turma;
        }

        public async Task<List<TurmaModel>> GetTurmas()
        {
            return await _context.Turmas.AsNoTracking().ToListAsync();
        }

        public async Task<TurmaModel> RemoverAlunoDaTurma(int idAluno, int idTurma)
        {
            var turma = await _context.Turmas.Include(a=> a.Alunos).FirstOrDefaultAsync(t => t.Id == idTurma);
            if (turma == null) throw new Exception("Turma não encontrada");

            var aluno = await _context.Alunos.FirstOrDefaultAsync(a=> a.Id == idAluno);
            if (aluno == null) throw new Exception("Aluno não encontrada");

            turma.Alunos.Remove(aluno);
            aluno.Turma = null;
            _context.Alunos.Update(aluno);
            _context.Turmas.Update(turma);
            await _context.SaveChangesAsync();


            await _notaService.RemoverNotasAAlunoRemovidoDaTurma(aluno, turma);
            return turma;
        }

        public async Task<TurmaModel> RemoverTurma(int idTurma)
        {
            var turma = await _context.Turmas.Include(a=> a.Alunos).FirstOrDefaultAsync(t=> t.Id == idTurma);
            if (turma == null) throw new Exception("Turma não encontrada");
            if (turma.Alunos.Any())
            {
                var removeuTodos = await RemoverTodosAlunos(idTurma);
                if (!removeuTodos) throw new Exception("Não foi possivel remover todos alunos da turma");
            }
            _context.Turmas.Remove(turma);
            await _context.SaveChangesAsync();
            return turma;
        }

        public async Task<bool> RemoverTodosAlunos(int idTurma)
        {
            var turma = await _context.Turmas.Include(a => a.Alunos).FirstOrDefaultAsync(t => t.Id == idTurma);
            foreach(AlunoModel aluno in turma.Alunos)
            {
                aluno.Turma = null;
            }
            turma.Alunos = null;

            _context.Update(turma);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
