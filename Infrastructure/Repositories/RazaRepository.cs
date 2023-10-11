using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class RazaRepository : GenericRepository<Raza>, IRazaRepository
    {
        private readonly VeterynaryContext _context;

        public RazaRepository(VeterynaryContext context) : base(context)
        {
            _context = context;
        }
    }
}