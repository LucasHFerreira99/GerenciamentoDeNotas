using GerenciamentoDeNotas.DTOs;
using GerenciamentoDeNotas.Filters;
using GerenciamentoDeNotas.Helper;
using GerenciamentoDeNotas.Models;
using GerenciamentoDeNotas.Services.Nota;
using GerenciamentoDeNotas.Services.Professor;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using System.Globalization;

namespace GerenciamentoDeNotas.Controllers
{
    [PaginaRestritaProfessor]
    public class ProfessorController : Controller
    {
        private readonly IProfessorService _iProfessorService;
        private readonly INotaService _notaService;
        private readonly ISessao _sessao;

        public ProfessorController(IProfessorService iProfessorService, ISessao sessao, INotaService notaService)
        {
            _iProfessorService = iProfessorService;
            _sessao = sessao;
            _notaService = notaService;
        }

        public async Task<IActionResult> Index()
        {
            var sessaoAtual = _sessao.BuscarSessaoDoUsuario();
            var professor = await _iProfessorService.BuscarProfessorTurmasEMaterias(sessaoAtual);

            return View(professor);
        }

        public async Task<IActionResult> AcessarTurma(int id)
        {
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");
            var notas = await _notaService.BuscarNotasPorTurma(id);
            
            return View(notas);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SalvarNotas(List<NotaModel> notasAtualizada)
        {
            var idTurma = await _notaService.AtualizarNotas(notasAtualizada);
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("pt-BR");
            return RedirectToAction("AcessarTurma", new {id = idTurma });
        }
        

    }
}
