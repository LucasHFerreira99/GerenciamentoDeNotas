using GerenciamentoDeNotas.Filters;
using GerenciamentoDeNotas.Models;
using GerenciamentoDeNotas.Services.Materia;
using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoDeNotas.Controllers
{
    [PaginaRestritaAdmin]
    public class MateriaController : Controller
    {
        private readonly IMateriaService _materiaService;

        public MateriaController(IMateriaService materiaService)
        {
            _materiaService = materiaService;
        }

        public async Task<IActionResult> Index()
        {
            var materias =  await _materiaService.GetMaterias();
            return View(materias);
        }

        public IActionResult CriarMateria()
        {
            return View();
        }

        
        public async Task<IActionResult> EditarMateria(int id)
        {
            try
            {
                var materia = await _materiaService.GetById(id);
                return View(materia);
            }catch (Exception ex)
            {
                TempData["MensagemErro"] = $"Ops, algo aconteceu de errado! Detalhe do erro: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }

        }

        public async Task<IActionResult> Deletar(int id)
        {
            try{
                await _materiaService.RemoverMateria(id);
                TempData["MensagemSucesso"] = "Matéria excluída com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                TempData["MensagemErro"] = $"Matéria não pode ser excluida! Detalhe do erro: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }

        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> CriarMateria(MateriaModel materia)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _materiaService.AdicionarNovaMateria(materia.Nome);
                    TempData["MensagemSucesso"] = "Matéria criada com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
                return View(materia);
            }catch(Exception ex)
            {
                TempData["MensagemErro"] = $"Ops, não foi possivel criar a matéria! Detalhe do erro: {ex.Message}";
                return RedirectToAction("Index");
            }
            
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> EditarMateria (MateriaModel materia)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _materiaService.EditarMateria(materia);
                    TempData["MensagemSucesso"] = "Matéria editada com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
                return View(materia);
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = $"Ops, não foi possivel editar a matéria! Detalhe do erro: {ex.Message}";
                return RedirectToAction("Index");
            }
        }



    }
}
