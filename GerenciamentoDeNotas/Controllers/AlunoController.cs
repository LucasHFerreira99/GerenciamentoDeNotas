using GerenciamentoDeNotas.Filters;
using GerenciamentoDeNotas.Helper;
using GerenciamentoDeNotas.Services.Aluno;
using GerenciamentoDeNotas.Services.Nota;
using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoDeNotas.Controllers
{
    public class AlunoController : Controller
    {
        private readonly IAlunoService _alunoService;
        private readonly INotaService _notaService;
        private readonly ISessao _sessao;
        public AlunoController(IAlunoService alunoService, INotaService notaService, ISessao sessao)
        {
            _alunoService = alunoService;
            _notaService = notaService;
            _sessao = sessao;
        }

        [PaginaRestritaAluno]
        public async Task<IActionResult> Index()
        {
            var usuario = _sessao.BuscarSessaoDoUsuario();
            var aluno =  await _alunoService.GetAlunoByUsuario(usuario);
            var notas = await _notaService.BuscarNotasPorAluno(aluno);
            return View(notas);
        }

        [PaginaRestritaAdmin]
        public async Task<IActionResult> DetalhesAluno(int id)
        {
            var aluno = await  _alunoService.GetAlunoById(id);
            var usuario = await _alunoService.GetUsuarioPeloAluno(aluno);
            return View(usuario);
        }

        [PaginaRestritaAluno]
        public async Task<IActionResult> AcessarNotaDaMateria(int id)
        {
            var notaDaMateria = await _notaService.BuscarNotasPorId(id);
            return View(notaDaMateria);
        }


    }
}
