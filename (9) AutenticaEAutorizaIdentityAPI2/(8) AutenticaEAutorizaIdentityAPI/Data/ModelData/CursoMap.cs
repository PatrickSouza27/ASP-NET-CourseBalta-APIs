using _8__AutenticaEAutorizaIdentityAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace _8__AutenticaEAutorizaIdentityAPI.Data.ModelData
{
    public class CursoMap : IEntityTypeConfiguration<Curso>
    {
        public void Configure(EntityTypeBuilder<Curso> builder)
        {
            builder.ToTable("Curso");

            builder.HasKey(x => x.IdCurso);

            builder.Property(x => x.IdCurso)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnName("Name")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(60);

            builder.HasOne(x => x.Categoria)
                .WithMany(c => c.Cursos)
                .HasForeignKey(x => x.CategoriaId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

        }
    }
}
