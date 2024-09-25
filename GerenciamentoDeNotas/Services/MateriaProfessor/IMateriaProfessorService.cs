using GerenciamentoDeNotas.DTOs;
using GerenciamentoDeNotas.Models;

namespace GerenciamentoDeNotas.Services.MateriaProfessor
{
    public interface IMateriaProfessorService
    {
        Task<List<MateriaProfessorModel>> BuscarTurmasEMateriasDeUmProfessor(int id);
        Task<MateriasTurmasListarDto> BuscarTodasMateriaETurmas(int idProfessor);
        Task<MateriaProfessorModel> CriarNovaMateriaProfessor(MateriaProfessorCriarDto materiaProfessor);
        Task<bool> RemoverMateriaProfessor(int idMateriaProfessor);
    }
}
