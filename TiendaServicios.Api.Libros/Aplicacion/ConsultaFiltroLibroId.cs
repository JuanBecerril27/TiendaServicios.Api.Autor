using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.Libros.Modelo;
using TiendaServicios.Api.Libros.Persistencia;

namespace TiendaServicios.Api.Libros.Aplicacion
{
    public class ConsultaFiltroLibroId
    {
        public class LibrosUnicos : IRequest<LibreriaMaterial>
        {
            public Guid? IdLibro { get; set; }
        }

        public class Manejador : IRequestHandler<LibrosUnicos, LibreriaMaterial>
        {
            private readonly ContextoLibreria _contexto;
            public Manejador(ContextoLibreria contexto)
            {
                _contexto = contexto;
            }

            public async Task<LibreriaMaterial> Handle(LibrosUnicos request, CancellationToken cancellationToken)
            {
                var libroId = await _contexto.LibreriaMaterial.Where(x => x.LibreriaMaterialId == request.IdLibro).FirstOrDefaultAsync();
                if (libroId == null)
                {
                    throw new Exception("No se encontro el libro");
                }
                return libroId;
            }

        }
    }
}
