using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CopaFilmes.Api.Dominio.Usuario;

internal class UsuarioMap : IEntityTypeConfiguration<UsuarioEntity>
{
	public void Configure(EntityTypeBuilder<UsuarioEntity> builder)
	{
		var table = builder.ToTable("TS_USUARIO");

		table.HasKey(u => u.Id);
		table.HasIndex(u => u.Usuario).IsUnique();

		builder
			.Property(u => u.Id)
			.HasColumnName("PK_ID");

		builder
			.Property(u => u.Usuario)
			.HasColumnName("DS_USUARIO")
			.HasMaxLength(50)
			.IsRequired();
		builder
			.Property(u => u.Senha)
			.HasColumnName("HX_SENHA")
			.IsRequired();
	}
}
