using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuracion;

public class PaisConfiguration : IEntityTypeConfiguration<Pais>
{
    public void Configure(EntityTypeBuilder<Pais> builder)
    {
        // Aquí puedes configurar las propiedades de la entidad
        // utilizando el objeto builder
        builder.ToTable("Pais");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id);

        builder.Property(e => e.NombrePais)
        .IsRequired()
        .HasMaxLength(50);

    }

}