using ApiParcial.Data;
using ApiParcial.Resultado.ListadoAviones;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace ApiParcial.Business
{
    public class GetAvion
    {
        public class GetAvion_Business : IRequest<ListadoAviones>
        {

        }
        public class validacion : AbstractValidator<GetAvion_Business>
        {


        }
        public class Manejador : IRequestHandler<GetAvion_Business, ListadoAviones>
        {

            private readonly ContextBD _context;
            private readonly IValidator<GetAvion_Business> _validate;

            public Manejador(ContextBD context, IValidator<GetAvion_Business> validator)
            {
                this._context = context;
                this._validate = validator;
            }



            public async Task<ListadoAviones> Handle(GetAvion_Business request, CancellationToken cancellationToken)
            {
                var result = new ListadoAviones();
                var validation = await _validate.ValidateAsync(request);

                if (!validation.IsValid)
                {
                    var error = string.Join(Environment.NewLine, validation.Errors);
                    result.SetMensajeError(error, HttpStatusCode.InternalServerError);
                    return result;
                }

                var avion = await _context.Aviones
                    .Where(f => f.IdFabricanteNavigation.Nombre != "Boeing"
                                && f.CantidadAsientos < 5 && f.CantidadMotores == 1)
                    .Include(x => x.IdFabricanteNavigation)
                    .FirstOrDefaultAsync();

                if (avion == null)
                {
                    result.SetMensajeError("No hay aviones con esas caracteristicas", HttpStatusCode.InternalServerError);
                    return result;
                }

                var itemAvion = new ItemAvion();
                itemAvion.Id = avion.Id;
                itemAvion.Modelo = avion.Modelo;
                itemAvion.CantidadMotores = avion.CantidadMotores;
                itemAvion.CantidadAsientos = avion.CantidadAsientos;
                itemAvion.DatosVarios = avion.DatosVarios;
                itemAvion.FabricanteNavigation = avion.IdFabricanteNavigation.Nombre;
                result.ListAviones.Add(itemAvion);

                return result;
            }

        }
    }
}

