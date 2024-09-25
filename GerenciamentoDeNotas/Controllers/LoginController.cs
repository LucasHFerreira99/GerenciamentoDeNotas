using GerenciamentoDeNotas.Helper;
using GerenciamentoDeNotas.Models;
using GerenciamentoDeNotas.Models.Usuario;
using GerenciamentoDeNotas.Services.Usuario;
using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoDeNotas.Controllers
{
    public class LoginController : Controller
    {

        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ISessao _sessao;
        private readonly IEmail _email;

        public LoginController(IUsuarioRepository usuarioRepository, ISessao sessao, IEmail email)
        {
            _usuarioRepository = usuarioRepository;
            _sessao = sessao;
            _email = email;
        }

        public IActionResult Index()
        {
            // Se o usuario estiver logado, redirecionar para a HOME
            if(_sessao.BuscarSessaoDoUsuario() != null)
                return RedirectToAction("Index", "Home");

            return View();
        }


        [HttpPost]
        public IActionResult Entrar(LoginModel loginModel)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var usuario = _usuarioRepository.BuscarPorLogin(loginModel.Login);

                    if (usuario != null)
                    {
                        if (usuario.SenhaValida(loginModel.Senha))
                        {
                            _sessao.CriarSessaoDoUsuario(usuario);
                            return RedirectToAction("Index", "Home");
                        }
                        TempData["MensagemErro"] = $"Senha do usuário é inválida. Por favor, tente novamente!";
                        return View("Index");
                    }
                    TempData["MensagemErro"] = $"Usuário e/ou senha inválido(s). Por favor, tente novamente!";
                }
                return View("Index");
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos realizar o seu login, tente novamente. Detalhes do erro: {ex.Message}";
                return RedirectToAction("Index");
            }
        }

        public IActionResult RedefinirSenha()
        {
            return View();
        }

        [HttpPost]
        public IActionResult EnviarLinkParaRedefinirSenha(RedefinirSenhaModel redefinirSenhaModel)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var usuario = _usuarioRepository.BuscarPorEmailELogin(redefinirSenhaModel.Email, redefinirSenhaModel.Login);
                 
                    if(usuario != null)
                    {
                        string novaSenha = usuario.GerarNovaSenha();
                        string mensagem = $"Sua nova senha é: {novaSenha}";

                        bool emailEnviado = _email.Enviar(usuario.Email, "Sistem de contatos - Nova senha", mensagem);
                        
                        if(emailEnviado)
                        {
                            _usuarioRepository.Editar(usuario);
                            TempData["MensagemSucesso"] = $"Enviamos para o seu e-mail cadastrado uma nova senha.";
                        }
                        else
                        {
                            TempData["MensagemErro"] = $"Não conseguimos enviar o e-mail. Por favor, tente novamente!";
                        }
                        return RedirectToAction("Index", "Login");
                    }
                    TempData["MensagemErro"] = $"Não conseguimos redefinir a sua senha. Por favor, verifique os dados informados!";
                }
                return View("Index");
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos redefinir a sua senha, tente novamente. Detalhes do erro: {ex.Message}";
                return RedirectToAction("Index");
            }
        }
        public IActionResult Sair()
        {
            _sessao.RemoverSessaoDoUsuario();
            return RedirectToAction("Index", "Login");
        }
    }
}
