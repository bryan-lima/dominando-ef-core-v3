using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCore.UowRepository.Data.Repositories;
using EFCore.UowRepository.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EFCore.UowRepository.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DepartamentoController : ControllerBase
    {
        private readonly ILogger<DepartamentoController> _logger;
        private readonly IDepartamentoRepository _departamentoRepository;

        public DepartamentoController(ILogger<DepartamentoController> logger, IDepartamentoRepository departamentoRepository)
        {
            _logger = logger;
            _departamentoRepository = departamentoRepository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id/*, [FromServices] IDepartamentoRepository repository*/)
        {
            //var departamento = await repository.GetByIdAsync(id);
            var departamento = await _departamentoRepository.GetByIdAsync(id);

            return Ok(departamento);
        }

        [HttpPost]
        public IActionResult CreateDepartamento(Departamento departamento)
        {
            _departamentoRepository.Add(departamento);
            bool _saved = _departamentoRepository.Save();

            return Ok(departamento);
        }
    }
}
