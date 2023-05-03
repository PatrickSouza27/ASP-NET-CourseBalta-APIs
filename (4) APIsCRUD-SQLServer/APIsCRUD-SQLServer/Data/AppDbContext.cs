using APIsCRUD_SQLServer.Data.Map;
using APIsCRUD_SQLServer.Models;
using Microsoft.EntityFrameworkCore;

namespace APIsCRUD_SQLServer.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=Dapper;User ID=sa;Password=1q2w3e4r@#$;TrustServerCertificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MapCategoria());
            modelBuilder.ApplyConfiguration(new MapCurso());
        }
    }
}
