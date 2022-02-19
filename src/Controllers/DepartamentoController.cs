using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCore.UowRepository.Data;
using EFCore.UowRepository.Data.Repositories;
using EFCore.UowRepository.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EFCore.UowRepository.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DepartamentoController : ControllerBase
    {
        private readonly ILogger<DepartamentoController> _logger;
        //private readonly IDepartamentoRepository _departamentoRepository;
        private readonly IUnitOfWork _uow;

        public DepartamentoController(ILogger<DepartamentoController> logger, 
                                      IDepartamentoRepository departamentoRepository,
                                      IUnitOfWork uow)
        {
            _logger = logger;
            //_departamentoRepository = departamentoRepository;
            _uow = uow;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id/*, [FromServices] IDepartamentoRepository repository*/)
        {
            //var departamento = await repository.GetByIdAsync(id);
            //var departamento = await _departamentoRepository.GetByIdAsync(id);
            Departamento _departamento = await _uow.DepartamentoRepository.GetByIdAsync(id);

            return Ok(_departamento);
        }

        [HttpPost]
        public IActionResult CreateDepartamento(Departamento departamento)
        {
            //_departamentoRepository.Add(departamento);
            _uow.DepartamentoRepository.Add(departamento);

            //bool _saved = _departamentoRepository.Save();
            _uow.Commit();

            return Ok(departamento);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveDepartamentoAsync(int id)
        {
            Departamento _departamento = await _uow.DepartamentoRepository.GetByIdAsync(id);

            _uow.DepartamentoRepository.Remove(_departamento);

            _uow.Commit();

            return Ok(_departamento);
        }

        [HttpGet]
        public async Task<IActionResult> ConsultarDepartamentoAsync([FromQuery] string descricao)
        {
            List<Departamento> _departamentos = await _uow.DepartamentoRepository.GetDataAsync(expression: departamento => departamento.Descricao.Contains(descricao), 
                                                                                               include: departamento => departamento.Include(c => c.Colaboradores), 
                                                                                               take: 2);

            return Ok(_departamentos);
        }
    }
}
