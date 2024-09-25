using GerenciamentoDeNotas.Filters;
using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoDeNotas.Controllers
{
    [PaginaUsuarioLogado]
    public class RestritoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
