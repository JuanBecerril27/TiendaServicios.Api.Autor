using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaServicios.Api.Libros.Aplicacion;
using TiendaServicios.Api.Libros.Modelo;

namespace TiendaServicios.Api.Libros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibroController : ControllerBase
    {
        private readonly IMediator _mediator;
        public LibroController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<ActionResult<Unit>> Crear(Nuevo.Ejecuta data)
        {
            return await _mediator.Send(data);
        }
        [HttpGet]
        public async Task<ActionResult<List<LibreriaMaterial>>> GetLibros()
        {
            return await _mediator.Send(new Consulta.ListaLibro());
        }
        //[HttpGet("{id}")]
        //public async Task<ActionResult<LibreriaMaterial>> GetAutorLibro(string id)
        //{
        //    return await _mediator.Send(new ConsultaFiltro.LibroUnico { LibroGuid = id });

        //}
        [HttpGet("{IdLibro}")] //nombre de un parametro
        public async Task<ActionResult<LibreriaMaterial>> GetLibreriaMaterialId(Guid? IdLibro)
        {
            return await _mediator.Send(new ConsultaFiltroLibroId.LibrosUnicos { IdLibro = IdLibro });
        }
    }
}
