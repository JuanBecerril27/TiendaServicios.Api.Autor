using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System;
using TiendaServicios.Api.Libros.Modelo;
using TiendaServicios.Api.Libros.Persistencia;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TiendaServicios.Api.Libros.Aplicacion
{
    public class ConsultaFiltro
    {
        public class LibroUnico : IRequest<LibreriaMaterial>
        {
            public string LibroGuid { get; set; }
        }

        public class Manejador : IRequestHandler<LibroUnico, LibreriaMaterial>
        {
            private readonly ContextoLibreria _contexto;
            public Manejador(ContextoLibreria contexto)
            {
                _contexto = contexto;
            }

            public async Task<LibreriaMaterial> Handle(LibroUnico request, CancellationToken cancellationToken)
            {
                var libro = await _contexto.LibreriaMaterial.Where(x => x.AutorLibro == request.LibroGuid).FirstOrDefaultAsync();
                if (libro == null)
                {
                    throw new Exception("No se encontro el autor");
                }
                return libro;
            }
        }
    }
}
