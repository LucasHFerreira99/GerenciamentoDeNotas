using GerenciamentoDeNotas.Data;
using GerenciamentoDeNotas.DTOs;
using GerenciamentoDeNotas.Models;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Drawing.Drawing2D;

namespace GerenciamentoDeNotas.Services.MateriaProfessor
{
    public class MateriaProfessorService : IMateriaProfessorService
    {
        private readonly AppDbContext _context;

        public MateriaProfessorService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<MateriaProfessorModel>> BuscarTurmasEMateriasDeUmProfessor(int id)
        {
            var materiaETurmas =  await _context.MateriaProfessor.Include(t=> t.Turma).Include(m=> m.Materia).Where(m=> m.Professor.Id == id).ToListAsync();
            if (materiaETurmas == null) return new List<MateriaProfessorModel>();
            return materiaETurmas;
        }

        public async Task<MateriasTurmasListarDto> BuscarTodasMateriaETurmas(int idProfessor)
        {
            var professor = await _context.Professores.AsNoTracking().FirstOrDefaultAsync(p=> p.Id == idProfessor);
            var materias = await _context.Materias.AsNoTracking().ToListAsync();
            var turmas = await _context.Turmas.AsNoTracking().ToListAsync();
            MateriasTurmasListarDto data = new MateriasTurmasListarDto
            {
                Professor = professor,
                Materias = materias,
                Turmas = turmas
            };
            if (data == null) throw new Exception("Algum erro aconteceu ao pegar as materias e turmas");
            return data;
        }

        public async Task<MateriaProfessorModel> CriarNovaMateriaProfessor(MateriaProfessorCriarDto materiaProfessor)
        {
            var professor = await _context.Professores.FirstOrDefaultAsync(p => p.Id == materiaProfessor.IdProfessor);
            var materia = await _context.Materias.FirstOrDefaultAsync(m => m.Id == materiaProfessor.IdMateria);
            var turma = await _context.Turmas.FirstOrDefaultAsync(m => m.Id == materiaProfessor.IdTurma);

            MateriaProfessorModel novaMateriaProfessor = new MateriaProfessorModel
            {
                Professor = professor,
                Materia = materia,
                Turma = turma
            };
            _context.MateriaProfessor.Add(novaMateriaProfessor);
            await _context.SaveChangesAsync();

            var deuCerto = await AdicionarNotasAosAlunos(novaMateriaProfessor);

            return novaMateriaProfessor;
        }


        public async Task<bool> RemoverMateriaProfessor(int idMateriaProfessor)
        {
            var materiaProfessor = await _context.MateriaProfessor.FirstOrDefaultAsync(p => p.Id == idMateriaProfessor);

            if (materiaProfessor == null) throw new Exception("Materia de professor não encontrada!");

            _context.MateriaProfessor.Remove(materiaProfessor);
            await _context.SaveChangesAsync();
            return true;
        }

        private async Task<bool> AdicionarNotasAosAlunos(MateriaProfessorModel novaMateriaProfessor)
        {
            try
            {
                var listaDeAlunos = await _context.Alunos.Include(t => t.Turma).Where(p => p.Turma.Id == novaMateriaProfessor.Turma.Id).ToListAsync();

                foreach (var aluno in listaDeAlunos)
                {
                    NotaModel notaAluno = new NotaModel
                    {
                        Aluno = aluno,
                        Materia = novaMateriaProfessor,
                    };
                    _context.Notas.Add(notaAluno);
                    await _context.SaveChangesAsync();
                }
                return true;
            }catch(Exception ex)
            {
                throw new Exception("Deu erro ao adicionar as notas aos alunos dessa nova materia");
            }
        }

        private async Task<bool> RemoverNotasDosAlunos(MateriaProfessorModel materiaProfessor)
        {
            try
            {
                var notas = await _context.Notas.Where(p => p.Materia.Id == materiaProfessor.Id).ToListAsync();

                foreach (var nota in notas)
                {
                    _context.Notas.Remove(nota);
                    await _context.SaveChangesAsync();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Deu erro ao remover as notas aos alunos dessa materia");
            }
        }
    }
}
