using GerenciamentoDeNotas.Models.Usuario;
using GerenciamentoDeNotas.Models;
using GerenciamentoDeNotas.Data;
using Microsoft.EntityFrameworkCore;
using GerenciamentoDeNotas.Services.Usuario;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace GerenciamentoDeNotas.Services.Aluno
{
    public class AlunoService : IAlunoService
    {
        private readonly AppDbContext _context;
        public AlunoService(AppDbContext context)
        {
            _context = context;
        }

        public Task<List<AlunoModel>> AlunosSemTurma()
        {
            return _context.Alunos.AsNoTracking().Where(a=> a.Turma == null).ToListAsync();
        }

        public async Task<AlunoModel> CadastrarAluno(UsuarioModel alunoCadastro)
        {
            var alunoNovo = new AlunoModel
            {
                Nome = alunoCadastro.Nome,
                Login = alunoCadastro.Login,
                Turma = null
            };

            _context.Alunos.Add(alunoNovo);
            await _context.SaveChangesAsync();
            return alunoNovo;
        }

        public async Task<AlunoModel> EditarAluno(UsuarioModel alunoEditado, string login)
        {
            var alunoDb = await _context.Alunos.FirstOrDefaultAsync(p => p.Login == login);
            if (alunoDb == null) throw new System.Exception("Houve um erro ao atualizar o usuário! Usuário não encontrado!");

            alunoDb.Nome = alunoEditado.Nome;
            alunoDb.Login = alunoEditado.Login;

            _context.Alunos.Update(alunoDb);
            await _context.SaveChangesAsync();
            return alunoDb;
        }

        public async Task<List<AlunoModel>> GetAlunos()
        {
            return await _context.Alunos.ToListAsync();
        }

        public async Task<AlunoModel> GetAlunoById(int id)
        {
            var aluno = await _context.Alunos.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
            return aluno == null ? throw new Exception("Aluno não encotrado") : aluno;
        }


        public async Task<AlunoModel> GetAlunoByUsuario(UsuarioModel usuario)
        {
            var aluno = await _context.Alunos.AsNoTracking().FirstOrDefaultAsync(a => a.Login == usuario.Login);
            return aluno == null ? throw new Exception("Aluno não encontrado") : aluno;
        }

        public async Task<UsuarioModel> GetUsuarioPeloAluno(AlunoModel aluno)
        {
            return _context.Usuarios.FirstOrDefault(x => x.Login.ToUpper() == aluno.Login.ToUpper());
        }
    }
}
