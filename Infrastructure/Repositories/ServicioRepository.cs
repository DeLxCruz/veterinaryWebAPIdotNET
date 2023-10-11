using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class ServicioRepository : GenericRepository<Servicio>, IServicioRepository
    {
        private readonly VeterynaryContext _context;

        public ServicioRepository(VeterynaryContext context) : base(context)
        {
            _context = context;
        }
    }
}