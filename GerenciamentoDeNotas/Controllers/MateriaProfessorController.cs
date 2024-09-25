using GerenciamentoDeNotas.DTOs;
using GerenciamentoDeNotas.Filters;
using GerenciamentoDeNotas.Models;
using GerenciamentoDeNotas.Services.MateriaProfessor;
using GerenciamentoDeNotas.Services.Professor;
using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoDeNotas.Controllers
{
    [PaginaRestritaAdmin]
    public class MateriaProfessorController : Controller
    {
        private readonly IProfessorService _professorService;
        private readonly IMateriaProfessorService _materiaProfessorService;

        public MateriaProfessorController(IProfessorService professorService, IMateriaProfessorService materiaProfessorService)
        {
            _professorService = professorService;
            _materiaProfessorService = materiaProfessorService;
        }

        public async Task<IActionResult> Index()
        {
            var professores = await _professorService.BuscarTodosProfessores();
            return View(professores);
        }

        public async Task<IActionResult> GerenciarTurmas(int id)
        {
            var materiasETurmaDeUmProfessor = await _materiaProfessorService.BuscarTurmasEMateriasDeUmProfessor(id);
            var professor = await _professorService.BuscarProfessorPorId(id);
            var materiaProfessorExibirDto = new MateriaProfessorExibirDto
            {
                MateriaProfessor = materiasETurmaDeUmProfessor,
                Professor = professor
            };
            return View(materiaProfessorExibirDto);
        }

        public async Task<IActionResult> CriarNovaMateriaProfessorTurma(int id)
        {
            try
            {
                var materiasETurmas = await _materiaProfessorService.BuscarTodasMateriaETurmas(id);
                return View(materiasETurmas);
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = $"Ops, algo aconteceu de errado! Detalhe do erro: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }

        }


        public async Task<IActionResult> DeletarMateriaProfessorTurma(int id, int IdProfessor)
        {
            try
            {
                var materiaExcluida = await _materiaProfessorService.RemoverMateriaProfessor(id);
                TempData["MensagemSucesso"] = "Materia do professor deletado com sucesso!";
                return RedirectToAction("GerenciarTurmas", new { id = IdProfessor });
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = $"Ops, algo aconteceu de errado! Detalhe do erro: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }


        [HttpPost]
        public async Task<IActionResult> CriarNovaMateriaProfessorTurma(MateriaProfessorCriarDto materiaProfessorModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _materiaProfessorService.CriarNovaMateriaProfessor(materiaProfessorModel);
                    TempData["MensagemSucesso"] = "Materia do professor criada com sucesso!";
                    return RedirectToAction("GerenciarTurmas", new { id = materiaProfessorModel.IdProfessor });
                }
                var materiasETurmas = await _materiaProfessorService.BuscarTodasMateriaETurmas(materiaProfessorModel.IdProfessor);
                return View(materiasETurmas);
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = $"Ops, algo aconteceu de errado! Detalhe do erro: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }

        }

    }
}
