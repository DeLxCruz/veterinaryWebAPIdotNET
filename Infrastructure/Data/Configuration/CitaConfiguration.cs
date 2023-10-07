using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuracion;
public class Configuration : IEntityTypeConfiguration<Cita>
{
    public void Configure(EntityTypeBuilder<Cita> builder)
    {
        // Aquí puedes configurar las propiedades de la entidad
        // utilizando el objeto builder
        builder.ToTable("Cita");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id);

        builder.Property(p => p.Fecha)
        .HasColumnType("date");

        builder.Property(p => p.Hora)
        .HasColumnType("date");

        builder.HasOne(p => p.Clientes)
        .WithMany(p => p.Citas)
        .HasForeignKey(p => p.IdCliente);

        builder.HasOne(p => p.Mascota)
        .WithMany(p => p.Citas)
        .HasForeignKey(p => p.IdMascota);

        builder.HasOne(p => p.Servicios)
        .WithMany(p => p.Citas)
        .HasForeignKey(p => p.ServicioId);
    }
}