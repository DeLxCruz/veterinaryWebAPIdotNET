using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;

namespace Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly VeterynaryContext _context;
        private PaisRespository _paises;
        private CiudadRepository _ciudades;
        private ClienteDireccionRepository _clienteDirecciones;
        private ClienteRepository _clientes;
        private CitaRepository _citas;
        private DepartamentoRepository _departamentos;
        private ClienteTelefonoRepository _clienteTelefonos;
        private MascotaRepository _mascotas;
        private RazaRepository _razas;
        private ServicioRepository _servicios;


        public UnitOfWork(VeterynaryContext context)
        {
            _context = context;
        }

        public IPaisRepository Paises
        {
            get
            {
                if (_paises == null)
                {
                    _paises = new PaisRespository(_context);
                }
                return _paises;
            }
        }

        public ICiudadRepository Ciudades
        {
            get
            {
                if (_ciudades == null)
                {
                    _ciudades = new CiudadRepository(_context);
                }
                return _ciudades;
            }
        }

        public IClienteDireccionRepository ClienteDirecciones
        {
            get
            {
                if (_clienteDirecciones == null)
                {
                    _clienteDirecciones = new ClienteDireccionRepository(_context);
                }
                return _clienteDirecciones;
            }
        }

        public IClienteRepository Clientes
        {
            get
            {
                if (_clientes == null)
                {
                    _clientes = new ClienteRepository(_context);
                }
                return _clientes;
            }
        }

        public ICitaRepository Citas
        {
            get
            {
                if (_citas == null)
                {
                    _citas = new CitaRepository(_context);
                }
                return _citas;
            }
        }

        public IClienteTelefonoRepository ClienteTel
        {
            get
            {
                if (_clienteTelefonos == null)
                {
                    _clienteTelefonos = new ClienteTelefonoRepository(_context);
                }
                return _clienteTelefonos;
            }
        }

        public IDepartamento Departamentos
        {
            get
            {
                if (_departamentos == null)
                {
                    _departamentos = new DepartamentoRepository(_context);
                }
                return _departamentos;
            }
        }

        public IMascotaRepository Mascotas
        {
            get
            {
                if (_mascotas == null)
                {
                    _mascotas = new MascotaRepository(_context);
                }
                return _mascotas;
            }
        }


        public IRazaRepository Razas
        {
            get
            {
                if (_razas == null)
                {
                    _razas = new RazaRepository(_context);
                }
                return _razas;
            }
        }

        public IServicioRepository Servicios
        {
            get
            {
                if (_servicios == null)
                {
                    _servicios = new ServicioRepository(_context);
                }
                return _servicios;
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}