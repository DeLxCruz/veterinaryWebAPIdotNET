using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IUnitOfWork
    {
        IPaisRepository Paises { get;}
        ICiudadRepository Ciudades { get;}
        IClienteRepository Clientes { get;}
        IClienteDireccionRepository ClienteDirecciones { get;}
        ICitaRepository Citas { get;}
        IClienteTelefonoRepository ClienteTel { get;}
        IDepartamento Departamentos { get;}
        IMascotaRepository Mascotas { get;}
        IRazaRepository Razas { get;}
        IServicioRepository Servicios { get;} 
        Task<int> SaveAsync();
    }
}