using ApiParcial.Business;
using ApiParcial.Comando;
using ApiParcial.Data;
using ApiParcial.Resultado.ListadoAviones;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiParcial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AvionController : ControllerBase
    {
        private readonly ContextBD _contexto;
        private readonly IMediator _mediator;

        public AvionController(ContextBD contexto, IMediator mediator)
        {
            _contexto = contexto;
            _mediator = mediator;
        }
        // GET: api/<AvionController>
        [HttpGet]
        [Route("GetAviones")]
        public async Task<ListadoAviones> GetAviones()
        {
            var result = await _mediator.Send(new GetAvion.GetAvion_Business
            {

            });
            return result;
        }


        // PUT api/<AvionController>/5
        [HttpPut]
        [Route("/putAviones")]
        public async Task<ListadoAviones> putAviones([FromBody] PutAvionComando avion)
        {

            var result = await _mediator.Send(new PutAvion.PutAvion_Business
            {
                Id = avion.Id,
                CantidadAsientos = avion.CantidadAsientos,
                Modelo = avion.Modelo,
                CantidadMotores = avion.CantidadMotores,
                DatosVarios = avion.DatosVarios
            });
            return result;
        }

    }
}
