using GerenciamentoDeNotas.Data;
using GerenciamentoDeNotas.Models;
using GerenciamentoDeNotas.Models.Usuario;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace GerenciamentoDeNotas.Services.Professor
{
    public class ProfessorService : IProfessorService
    {
        private readonly AppDbContext _context;

        public ProfessorService(AppDbContext context)
        {
            _context = context;
        }


        public async Task<ProfessorModel> BuscarProfessor(UsuarioModel usuarioProfessor)
        {
            try
            {
                var professor = await _context.Professores.FirstOrDefaultAsync(p => p.Login == usuarioProfessor.Login);

                if(professor == null) return professor = new ProfessorModel();

                return professor;
            }
            catch(Exception ex)
            {
                throw new Exception("Algum erro ao buscar professor");
            }
        }

        public async Task<ProfessorModel> BuscarProfessorPorId(int id)
        {
            try
            {
                var professor = await _context.Professores.FirstOrDefaultAsync(p => p.Id == id);
                if (professor == null) return professor = new ProfessorModel();
                return professor;
            }
            catch (Exception ex)
            {
                throw new Exception("Algum erro ao buscar professor");
            }
        }


        public async Task<List<ProfessorModel>> BuscarTodosProfessores()
        {
            try
            {
                var professor = await _context.Professores.AsNoTracking().ToListAsync();
                if (professor == null) return null;
                return professor;
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possivel buscar todos professores");
            }
        }


        public async Task<List<MateriaProfessorModel>> BuscarProfessorTurmasEMaterias(UsuarioModel usuarioProfessor)
        {
            try
            {
                var professor = await BuscarProfessor(usuarioProfessor);

                var materiasProfessor = await _context.MateriaProfessor.Include(p => p.Professor).Include(p => p.Materia).Include(p => p.Turma).Where(p => p.Professor.Id == professor.Id).ToListAsync();

                if (materiasProfessor == null) return materiasProfessor = new List<MateriaProfessorModel>();

                return materiasProfessor;
            }
            catch (Exception ex)
            {
                throw new Exception("Algum erro ao buscar professor");
            }
        }

        

        public async Task<TurmaModel> BuscarTurma(int id)
        {
            try
            {
                var turma = await _context.Turmas.Include(m=> m.Alunos).ThenInclude(a=> a.Notas).ThenInclude(m => m.Materia).ThenInclude(t => t.Turma).FirstOrDefaultAsync(t=> t.Id == id);

                if (turma == null) throw new Exception("Turma não encontrada");

                return turma;

            }catch(Exception ex)
            {
                throw new Exception("Algum erro ao buscar turma");
            }
        }

        public async Task<ProfessorModel> CadastrarProfessor(UsuarioModel professorCadastro)
        {
            var professorNovo = new ProfessorModel
            {
                Nome = professorCadastro.Nome,
                CPF = professorCadastro.Cpf,
                Email = professorCadastro.Email,
                Login = professorCadastro.Login
            };

            _context.Professores.Add(professorNovo);
            await _context.SaveChangesAsync();
            return professorNovo;
        }

        public async Task<ProfessorModel> EditarProfessor(UsuarioModel professorEditado, string login)
        {
            var professorDB = await _context.Professores.FirstOrDefaultAsync(p => p.Login == login);
            if (professorDB == null) throw new System.Exception("Houve um erro ao atualizar o usuário! Usuário não encontrado!");


            professorDB.Nome = professorEditado.Nome;
            professorDB.Email = professorEditado.Email;
            professorDB.Login = professorEditado.Login;
            professorDB.CPF = professorEditado.Cpf;

            _context.Professores.Update(professorDB);
            await _context.SaveChangesAsync();
            return professorDB;
        }

    }
}
