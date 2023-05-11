using _8__AutenticaEAutorizaIdentityAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace _8__AutenticaEAutorizaIdentityAPI.Data.ModelData
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuario");
            builder.HasKey(x => x.Id);

            builder.Property(x=> x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnName("Name")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(60);

            builder.Property(x => x.Login)
                .IsRequired()
                .HasColumnName("Login")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(60);

            builder.Property(x => x.Password)
                .IsRequired()
                .HasColumnName("Password")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(200);


            builder
                .HasMany(x => x.RolesUser)
                .WithMany(x => x.Usuarios)
                .UsingEntity<Dictionary<string, object>>(
                    join => join
                        .HasOne<Roles>()
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .HasConstraintName("FK_UserRole_RoleId")
                        .OnDelete(DeleteBehavior.Cascade),
                    join => join
                        .HasOne<Usuario>()
                        .WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_UserRole_UserId")
                        .OnDelete(DeleteBehavior.Cascade));
        }
    }
}
