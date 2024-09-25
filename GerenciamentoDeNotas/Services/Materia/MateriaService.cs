
using GerenciamentoDeNotas.Data;
using GerenciamentoDeNotas.Models;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoDeNotas.Services.Materia
{
    public class MateriaService : IMateriaService
    {
        private readonly AppDbContext _context;

        public MateriaService(AppDbContext context)
        {
            _context = context;
        }
        
        public async Task<MateriaModel> AdicionarNovaMateria(string novaMateria)
        {
            var materia = new MateriaModel();
            materia.Nome = novaMateria;
            if (await ExisteMateria(novaMateria))
            {
                return materia;
            }

            _context.Materias.Add(materia);
            await _context.SaveChangesAsync();
            return materia;
        }

        public async Task<List<MateriaModel>> EditarMateria(MateriaModel materiaEditada)
        {
            var materiaBanco = await _context.Materias.FirstOrDefaultAsync(m=> m.Id == materiaEditada.Id);
            if (materiaBanco == null) throw new Exception("Materia não encontrada no banco");

            materiaBanco.Nome = materiaEditada.Nome;

            _context.Materias.Update(materiaBanco);
            await _context.SaveChangesAsync();
            return await GetMaterias();
        }

        public async Task<MateriaModel> GetById(int id)
        {
            var materia = await _context.Materias.FirstOrDefaultAsync(mat => mat.Id == id);
            if (materia == null) throw new Exception("Materia não encontrada no banco");
            return materia;
        }

        public async Task<List<MateriaModel>> GetMaterias()
        {
            return await _context.Materias.AsNoTracking().ToListAsync();
        }

        public async Task<MateriaModel> RemoverMateria(int id)
        {
            var materiaBanco = _context.Materias.FirstOrDefault(mat => mat.Id == id);
            if (materiaBanco == null) throw new Exception("Materia não existe no banco");


            var materiaEstaUsando = await _context.MateriaProfessor.FirstOrDefaultAsync(m=> m.Materia.Id == materiaBanco.Id);

            if (materiaEstaUsando != null) throw new Exception("Materia está atrelado a um professor e sala, tem que excluir essa relação antes");

            _context.Materias.Remove(materiaBanco);
            await _context.SaveChangesAsync();
            return materiaBanco;
        }

        public async Task<bool> ExisteMateria(string materia)
        {
            var materiaBanco = await _context.Materias.AsNoTracking().FirstOrDefaultAsync(m=> m.Nome ==  materia);
            if (materiaBanco == null) return false;
            return true;
        }
    }
}
