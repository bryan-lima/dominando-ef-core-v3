using EFCore.UowRepository.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore.UowRepository.Data.Repositories
{
    public class DepartamentoRepository : IDepartamentoRepository
    {
        private readonly ApplicationContext _context;

        public DepartamentoRepository(ApplicationContext context)
        {
            _context = context;
        }

        public void Add(Departamento departamento)
        {
            _context.Departamentos.Add(departamento);
        }

        public async Task<Departamento> GetByIdAsync(int id)
        {
            return await _context.Departamentos.Include(departamento => departamento.Colaboradores)
                                               .FirstOrDefaultAsync(departamento => departamento.Id == id);
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
