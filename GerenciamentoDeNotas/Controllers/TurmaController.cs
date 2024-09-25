using GerenciamentoDeNotas.DTOs;
using GerenciamentoDeNotas.Filters;
using GerenciamentoDeNotas.Services.Aluno;
using GerenciamentoDeNotas.Services.Turma;
using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoDeNotas.Controllers
{
    [PaginaRestritaAdmin]
    public class TurmaController : Controller
    {
        private readonly ITurmaService _turmaService;
        private readonly IAlunoService _alunoService;

        public TurmaController(ITurmaService turmaService, IAlunoService alunoService)
        {
            _turmaService = turmaService;
            _alunoService = alunoService;
        }

        public async Task<IActionResult> Index()
        {
            var turmas = await _turmaService.GetTurmas();
            return View(turmas);
        }

        public IActionResult Criar()
        {
            return View();
        }

        public async Task<IActionResult> Detalhes(int id)
        {
            try
            {
                var turma = await _turmaService.GetTurmaById(id);
                return View(turma);
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = $"Ops, algo aconteceu de errado! Detalhe do erro: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }

        }

        public async Task<IActionResult> Editar(int id)
        {
            var turma = await _turmaService.GetTurmaById(id);
            var turmaEditarDto = new TurmaEditarDto
            {
                Nome = turma.Nome,
                DataDeInicio = turma.DataDeInicio,
            };

            return View(turmaEditarDto);
        }

        public async Task<IActionResult> AdicionarAlunos(int id)
        {
            var alunos = await _alunoService.AlunosSemTurma();
            TurmaAlunoDto turmaAluno = new TurmaAlunoDto
            {
                idTurma = id,
                Alunos = alunos
            };
            return View(turmaAluno);
        }


        public async Task<IActionResult> Deletar(int id)
        {
            try
            {
                var turma = await _turmaService.RemoverTurma(id);
                TempData["MensagemSucesso"] = "Turma excluida com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = $"Ops, algo aconteceu de errado! Detalhe do erro: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }

        }

        public async Task<IActionResult> AdicionarAlunosATurma(int idAluno, int idTurma)
        {
            try
            {
                await _turmaService.AdicionarAlunosATurma(idAluno, idTurma);
                TempData["MensagemSucesso"] = "Alteração feita nos alunos da turma com sucesso!";
                return RedirectToAction("AdicionarAlunos", new { id = idTurma });
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = $"Ops, algo aconteceu de errado! Detalhe do erro: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> RemoverAlunoDaTurma(int idAluno, int idTurma)
        {
            try
            {
                var alunos = await _turmaService.RemoverAlunoDaTurma(idAluno, idTurma);
                TempData["MensagemSucesso"] = "Alteração feita nos alunos da turma com sucesso!";
                return RedirectToAction("Detalhes", new { id = idTurma });
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = $"Ops, algo aconteceu de errado! Detalhe do erro: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }



        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Criar(TurmaDto turmadto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _turmaService.CriarTurma(turmadto);
                    TempData["MensagemSucesso"] = "Turma criada com sucesso!";
                    return RedirectToAction("Index");
                }
                return View(turmadto);
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = $"Ops, algo aconteceu de errado! Detalhe do erro: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }

        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Editar(TurmaEditarDto turmaEditarDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _turmaService.EditarTurma(turmaEditarDto);
                    TempData["MensagemSucesso"] = "Turma editada com sucesso!";
                    return RedirectToAction("Index");
                }
                return View(turmaEditarDto);
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = $"Ops, algo aconteceu de errado! Detalhe do erro: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }

        }


    }
}
