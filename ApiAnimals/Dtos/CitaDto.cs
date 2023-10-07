using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAnimals.Dtos
{
    public class CitaDto
    {
        public DateTime Fecha { get; set; }
        public TimeSpan Hora { get; set; }
        public ClienteDto Clientes { get; set; }
        public MascotaDto Mascota { get; set; }
        public ServicioDto Servicios { get; set; }
    }
}