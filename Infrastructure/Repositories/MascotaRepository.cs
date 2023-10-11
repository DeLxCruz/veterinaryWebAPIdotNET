using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class MascotaRepository : GenericRepository<Mascota>, IMascotaRepository
    {
        private readonly VeterynaryContext _context;

        public MascotaRepository(VeterynaryContext context) : base(context)
        {
            _context = context;
        }
    }
}