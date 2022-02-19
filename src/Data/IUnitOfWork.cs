using EFCore.UowRepository.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore.UowRepository.Data
{
    public interface IUnitOfWork : IDisposable
    {
        bool Commit();
        IDepartamentoRepository DepartamentoRepository { get; }
    }
}
