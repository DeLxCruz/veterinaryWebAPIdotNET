using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ApiAnimals.Dtos;
public class CitaDto
{
    public int Id { get; set; }
    public DateOnly Fecha { get; set; }
    public TimeOnly Hora { get; set; }
    public ClienteDto Clientes { get; set; }
    public MascotaDto Mascota { get; set; }
    public ServicioDto Servicios { get; set; }
}