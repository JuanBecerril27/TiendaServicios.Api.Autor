using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System;
using TiendaServicios.Api.Libros.Persistencia;
using TiendaServicios.Api.Libros.Modelo;
using FluentValidation;

namespace TiendaServicios.Api.Libros.Aplicacion
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {
            public string Titulo { get; set; }
            public DateTime? FechaPublicacion { get; set; }
            public Guid? AutorLibro { get; set; }
        }
        public class EjecutaValidacion : AbstractValidator<Ejecuta>
        {
            public EjecutaValidacion()
            {
                RuleFor(x => x.Titulo).NotEmpty();
                RuleFor(x => x.FechaPublicacion).NotEmpty();
                RuleFor(x => x.AutorLibro).NotEmpty();
            }
        } 
        public class Manejador : IRequestHandler<Ejecuta>
        {
            public readonly ContextoLibreria _contexto;

            public Manejador(ContextoLibreria contexto)
            {
                _contexto = contexto;
            }
            public async Task<Unit> Handle(Ejecuta request, CancellationToken CancellationToken)
            {
                var libreriaMaterial = new LibreriaMaterial
                {
                    Titulo = request.Titulo,
                    FechaPublicacion = request.FechaPublicacion,
                    //agregar
                    AutorLibro = Convert.ToString(Guid.NewGuid())
                };

                _contexto.LibreriaMaterial.Add(libreriaMaterial);
                var valor = await _contexto.SaveChangesAsync();  //devuelve el valor segun inserciones a la BD

                if (valor > 0)
                {
                    return Unit.Value;
                }
                throw new Exception("no se pudo insertar el libro");
            }
        }
    }
}
