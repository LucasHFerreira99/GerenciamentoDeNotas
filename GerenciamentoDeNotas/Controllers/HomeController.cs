using GerenciamentoDeNotas.Filters;
using GerenciamentoDeNotas.Helper;
using GerenciamentoDeNotas.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GerenciamentoDeNotas.Controllers
{
    [PaginaUsuarioLogado]
    public class HomeController : Controller
    {
        private readonly ISessao _sessao;

        public HomeController(ISessao sessao)
        {
            _sessao = sessao;
        }

        public IActionResult Index()
        {
            var usuario = _sessao.BuscarSessaoDoUsuario();

            if (usuario == null)
            {
                return RedirectToAction("Index", "Login");
            }

            switch (usuario.Perfil)
            {
                case GerenciamentoDeNotas.Enums.PerfilEnum.Professor:
                    return RedirectToAction("Index", "Professor");

                case GerenciamentoDeNotas.Enums.PerfilEnum.Aluno:
                    return RedirectToAction("Index", "Aluno");

                case GerenciamentoDeNotas.Enums.PerfilEnum.Admin:
                    return View();
                default:
                    return View("Error");
            }
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}