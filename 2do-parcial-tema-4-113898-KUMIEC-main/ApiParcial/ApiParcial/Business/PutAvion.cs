using ApiParcial.Data;
using ApiParcial.Resultado.ListadoAviones;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static ApiParcial.Business.GetAvion;
using System.Net;
using System.ComponentModel.DataAnnotations;

namespace ApiParcial.Business
{
    public class PutAvion
    {
        public class PutAvion_Business : IRequest<ListadoAviones>
        {
            public int Id { get; set; }

            public int CantidadAsientos { get; set; }

            public string Modelo { get; set; } = null!;

            public int CantidadMotores { get; set; }

            public string? DatosVarios { get; set; }
        }
        public class validacion : AbstractValidator<PutAvion_Business>
        {
            public validacion()
            {
                RuleFor(c => c.Id).NotEmpty().WithMessage("Id no puede ser nulo");
                RuleFor(c => c.CantidadAsientos).NotEmpty().WithMessage("CantidadAsientos no puede ser nulo");
                RuleFor(c => c.CantidadMotores).NotEmpty().WithMessage("CantidadMotores no puede ser nulo");
                RuleFor(c => c.Modelo).NotEmpty().WithMessage("Modelo no puede ser nulo");
                RuleFor(c => c.DatosVarios).NotEmpty().WithMessage("DatosVarios no puede ser nulo");
            }
        }
        public class Manejador : IRequestHandler<PutAvion_Business, ListadoAviones>
        {

            private readonly ContextBD _context;
            private readonly IValidator<PutAvion_Business> _validate;

            public Manejador(ContextBD context, IValidator<PutAvion_Business> validator)
            {
                this._context = context;
                this._validate = validator;
            }

            public async Task<ListadoAviones> Handle(PutAvion_Business comand, CancellationToken cancellationToken)
            {

                var result = new ListadoAviones();
                var validati = await _validate.ValidateAsync(comand);
                if (!validati.IsValid)
                {
                    var error = string.Join(Environment.NewLine, validati.Errors);
                    result.SetMensajeError(error, HttpStatusCode.InternalServerError);
                    return result;
                }



                var avions = await _context.Aviones.FirstOrDefaultAsync(c => c.Id == comand.Id);

                if (avions.Id == null)
                {
                    result.SetMensajeError("id nulo", HttpStatusCode.InternalServerError);
                    return result;
                }

                avions.Id = comand.Id;
                avions.CantidadAsientos = comand.CantidadAsientos;
                avions.Modelo = comand.Modelo;
                avions.CantidadMotores = comand.CantidadMotores;
                avions.DatosVarios = comand.DatosVarios;



                _context.Update(avions);
                await _context.SaveChangesAsync();

                return result;


            }
        }
    }
}
