using GerenciamentoDeNotas.Data;
using GerenciamentoDeNotas.Models.Usuario;
using GerenciamentoDeNotas.Services.Aluno;
using GerenciamentoDeNotas.Services.Professor;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoDeNotas.Services.Usuario
{
    public class UsuarioRepository : IUsuarioRepository

    {
        private readonly AppDbContext _context;
        private readonly IProfessorService _professorService;
        private readonly IAlunoService _alunoService;

        public UsuarioRepository(AppDbContext context, IProfessorService professorService, IAlunoService alunoService)
        {
            _context = context;
            _professorService = professorService;
            _alunoService = alunoService;
        }


        public UsuarioModel BuscarPorLogin(string login)
        {
            return _context.Usuarios.FirstOrDefault(x => x.Login.ToUpper() == login.ToUpper());
        }
        public UsuarioModel BuscarPorEmailELogin(string email, string login)
        {
            return _context.Usuarios.FirstOrDefault(x => x.Login.ToUpper() == login.ToUpper() && x.Email.ToUpper() == email.ToUpper());
        }

        public List<UsuarioModel> BuscarTodos()
        {
            return _context.Usuarios
                .ToList();
        }

        public UsuarioModel BuscarPorId(int id)
        {
            return _context.Usuarios.Find(id);
        }

        public async Task<UsuarioModel> Adicionar(UsuarioModel usuario)
        {
            var existeLogin = verificaLoginExistente(usuario.Login);
            if(!existeLogin)
            {
                usuario.SetSenhaHash();
                _context.Usuarios.Add(usuario);
                _context.SaveChanges();
                return usuario;
            }

            return null;
        }

        public async Task<UsuarioModel> Editar(UsuarioModel usuario)
        {
            var usuarioDb = BuscarPorId(usuario.UsuarioId);
            var loginAtual = usuarioDb.Login;


            if (usuarioDb == null) throw new System.Exception("Houve um erro ao atualizar o usuário! Usuário não encontrado!");

            if (usuarioDb.Perfil == Enums.PerfilEnum.Professor)
            {
                await _professorService.EditarProfessor(usuario, loginAtual);
            }
            if (usuarioDb.Perfil == Enums.PerfilEnum.Aluno)
            {
                await _alunoService.EditarAluno(usuario, loginAtual);
            }

            usuarioDb.Nome = usuario.Nome;
            usuarioDb.Email = usuario.Email;
            usuarioDb.Login = usuario.Login;
            usuarioDb.Perfil = usuario.Perfil;
            usuarioDb.Cpf = usuario.Cpf;
            usuarioDb.DataAtualizacao = DateTime.Now;


            _context.Usuarios.Update(usuarioDb);
            _context.SaveChanges();
            return usuarioDb;
        }
        public UsuarioModel AlterarSenha(AlterarSenhaModel alterarSenhaModel)
        {
            UsuarioModel usuarioDb = BuscarPorId(alterarSenhaModel.Id);

            if (usuarioDb == null) throw new Exception("Houve um erro na atualização da senha, usuário não encontrado!");
        
            if(!usuarioDb.SenhaValida(alterarSenhaModel.SenhaAtual)) throw new Exception("Senha atual não confere!");

            if (usuarioDb.SenhaValida(alterarSenhaModel.NovaSenha)) throw new Exception("Nova senha deve ser diferente da senha atual!!");
        
            usuarioDb.SetNovaSenha(alterarSenhaModel.NovaSenha);
            usuarioDb.DataAtualizacao = DateTime.Now;

            _context.Usuarios.Update(usuarioDb);
            _context.SaveChanges();

            return usuarioDb;
        }   

        public bool Apagar(int id)
        {
            var usuario = _context.Usuarios.FirstOrDefault(usuario => usuario.UsuarioId == id);

            if (usuario is null) throw new System.Exception("Houve um erro ao encontrar o usuário para exclusão!");

            if (usuario.Perfil == Enums.PerfilEnum.Professor)
            {
                var professorExcluir = _context.Professores.FirstOrDefault(p => p.Login == usuario.Login);
                _context.Professores.Remove(professorExcluir);
            }
            if (usuario.Perfil == Enums.PerfilEnum.Aluno)
            {
                var alunoExcluir = _context.Alunos.FirstOrDefault(p => p.Login == usuario.Login);
                _context.Alunos.Remove(alunoExcluir);
            }

            _context.Usuarios.Remove(usuario);
            _context.SaveChanges();
            return true;
        }

        public bool verificaLoginExistente(string login)
        {
            var existeLogin = _context.Usuarios.FirstOrDefault(usuarioBanco => usuarioBanco.Login == login);

            if (existeLogin == null) return false;

            return true;
        }
    }
}