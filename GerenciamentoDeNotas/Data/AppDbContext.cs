using GerenciamentoDeNotas.Models;
using GerenciamentoDeNotas.Models.Usuario;
using Microsoft.EntityFrameworkCore;
using GerenciamentoDeNotas.DTOs;

namespace GerenciamentoDeNotas.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<AlunoModel> Alunos { get; set; }
        public DbSet<MateriaModel> Materias { get; set; }
        public DbSet<NotaModel> Notas { get; set; }
        public DbSet<ProfessorModel> Professores { get; set; }
        public DbSet<TurmaModel> Turmas { get; set; }
        public DbSet<MateriaProfessorModel> MateriaProfessor { get; set; }
        public DbSet<UsuarioModel> Usuarios { get; set; }  
    }
}
