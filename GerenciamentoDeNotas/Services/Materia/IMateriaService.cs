using GerenciamentoDeNotas.Models;

namespace GerenciamentoDeNotas.Services.Materia
{
    public interface IMateriaService
    {
        Task<MateriaModel> GetById(int id);
        Task<List<MateriaModel>> GetMaterias();
        Task<MateriaModel> AdicionarNovaMateria(string novaMateria);
        Task<List<MateriaModel>> EditarMateria(MateriaModel materiaEditada);
        Task<MateriaModel> RemoverMateria(int id);

    }
}
