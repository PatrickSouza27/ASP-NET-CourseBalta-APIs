using _8__AutenticaEAutorizaIdentityAPI.Data.ModelData;
using _8__AutenticaEAutorizaIdentityAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace _8__AutenticaEAutorizaIdentityAPI.Data
{
    public class ApiDataContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Roles> Roles { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=Token;User ID=sa;Password=1q2w3e4r@#$;TrustServerCertificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoriaMap());
            modelBuilder.ApplyConfiguration(new CursoMap());
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new RolesMap());
        }
    }
}
