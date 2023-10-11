using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class DepartamentoRepository : GenericRepository<Departamento>, IDepartamento
    {
        private readonly VeterynaryContext _context;

        public DepartamentoRepository(VeterynaryContext context) : base(context)
        {
            _context = context;
        }
    }
}