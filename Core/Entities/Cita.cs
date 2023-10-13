using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities;

public class Cita : BaseEntity
{
    [Required]
    public DateOnly Fecha { get; set; }

    [Required]
    public TimeOnly Hora { get; set; }

    [Required]
    public int IdCliente { get; set; }
    public Cliente Clientes { get; set; }
    [Required]
    public int IdMascota { get; set; }
    public Mascota Mascota { get; set; }
    [Required]
    public int ServicioId { get; set; }
    public Servicio Servicios { get; set; }
}
