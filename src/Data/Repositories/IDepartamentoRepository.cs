using EFCore.UowRepository.Data.Repositories.Base;
using EFCore.UowRepository.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore.UowRepository.Data.Repositories
{
    public interface IDepartamentoRepository : IGenericRepository<Departamento>
    {
        //Task<Departamento> GetByIdAsync(int id);
        //void Add(Departamento departamento);
        //bool Save();
    }
}
