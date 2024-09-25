using GerenciamentoDeNotas.Filters;
using GerenciamentoDeNotas.Helper;
using GerenciamentoDeNotas.Models.Usuario;
using GerenciamentoDeNotas.Services.Aluno;
using GerenciamentoDeNotas.Services.Professor;
using GerenciamentoDeNotas.Services.Usuario;
using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoDeNotas.Controllers
{
    [PaginaRestritaAdmin]
    public class UsuarioController : Controller
    {

        private readonly IUsuarioRepository _repository;
        private readonly ISessao _sessao;
        private readonly IProfessorService _professorService;
        private readonly IAlunoService _alunoService;
        public UsuarioController(IUsuarioRepository repository, ISessao sessao = null, IProfessorService professorService = null, IAlunoService alunoService = null)
        {
            _repository = repository;
            _sessao = sessao;
            _professorService = professorService;
            _alunoService = alunoService;
        }

        public IActionResult Index()
        {
            List<UsuarioModel> usuarios = _repository.BuscarTodos();
            return View(usuarios);
        }

        public IActionResult Criar()
        {
            return View();
        }

        public IActionResult Editar(int id)
        {
            var usuario = _repository.BuscarPorId(id);
            return View(usuario);
        }

        public IActionResult ApagarConfirmacao(int id)
        {
            var usuario = _repository.BuscarPorId(id);
            return View(usuario);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult>  Criar(UsuarioModel usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    usuario.Login = usuario.Login.ToLower();
                    var adicionado = _repository.Adicionar(usuario);

                    if (usuario.Perfil == Enums.PerfilEnum.Professor)
                    {
                        await _professorService.CadastrarProfessor(usuario);
                    }
                    if (usuario.Perfil == Enums.PerfilEnum.Aluno)
                    {
                        await _alunoService.CadastrarAluno(usuario);
                    }

                    if (adicionado == null)
                    {
                        throw new Exception("Já existe um usuario com esse login!");
                    }
                    TempData["MensagemSucesso"] = "Usuário cadastrado com sucesso!";
                    return RedirectToAction("Index");
                }

                return View(usuario);
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = $"Ops, não foi possivel cadastrar usuário, tente novamente! Detalhe do erro: {ex.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Editar(UsuarioSemSenha usuarioSemSenha)
        {
            try
            {
                UsuarioModel usuario = null;
                if (ModelState.IsValid)
                {
                    usuario = new UsuarioModel()
                    {
                        UsuarioId = usuarioSemSenha.UsuarioId,
                        Nome = usuarioSemSenha.Nome,
                        Login = usuarioSemSenha.Login.ToLower(),
                        Email = usuarioSemSenha.Email,
                        Perfil = usuarioSemSenha.Perfil,
                        Cpf = usuarioSemSenha.Cpf,
                    };
                    usuario = await _repository.Editar(usuario);

                    TempData["MensagemSucesso"] = "Usuário editado com sucesso!";
                    return RedirectToAction("Index");
                }

                return View(usuario);
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = $"Ops, não foi possivel editar usuário, tente novamente! Detalhe do erro: {ex.Message}";
                return RedirectToAction("Index");
            }
        }

        public IActionResult Excluir(int id)
        {
            try
            {
                if (_sessao.BuscarSessaoDoUsuario() == null)
                {
                    return RedirectToAction("Index", "Login");
                }
                var confirma = _repository.Apagar(id);
                if (confirma == true)
                {
                    TempData["MensagemSucesso"] = "Usuario excluido com sucesso!";
                }
                else
                {
                    TempData["MensagemErro"] = $"Ops, não foi possivel deletar usuario, tente novamente!";
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = $"Ops, não foi possivel excluir usuario, tente novamente! Detalhe do erro: {ex.Message}";
                return RedirectToAction("Index");
            }


        }
    }
}
