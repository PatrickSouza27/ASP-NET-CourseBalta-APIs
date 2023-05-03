using APIsCRUD_SQLServer.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace APIsCRUD_SQLServer.Data.Map
{
    public class MapCurso : IEntityTypeConfiguration<Curso>
    {
        public void Configure(EntityTypeBuilder<Curso> builder)
        {
            builder.ToTable("Curso");
            builder.HasKey(c => c.IdCurso);
            builder.Property(x => x.IdCurso).ValueGeneratedOnAdd().UseIdentityColumn();

            builder.Property(x => x.Name).IsRequired()
                .HasColumnName("Name")
                .HasColumnType("NVARCHAR")
            .HasMaxLength(20);

            builder.HasOne(x => x.Categoria).WithMany().HasForeignKey(x => x.CategoriaId);

        }
    }
}
