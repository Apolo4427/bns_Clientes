using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ModuloClientes.API.DTOs.Create;
using ModuloClientes.API.DTOs.Response;
using ModuloClientes.API.DTOs.Update;
using ModuloClientes.Aplication.Command.ClienteCommands;
using ModuloClientes.Core.Ports.Queries.ClienteQueries;
//TODO: implementar cancelations tokens

namespace ModuloClientes.API.Controllers
{
    [ApiController]
    [Route("clientes")]
    public class ClienteController : ControllerBase
    {
        private readonly IValidator<ClienteCreateDto> _createValidator;
        private readonly IMediator _mediator;
        private readonly IValidator<ClienteUpdateDto> _updateValidator;
        private readonly IMapper _mapper;

        public ClienteController(
            IValidator<ClienteCreateDto> createValidator,
            IMediator mediator,
            IValidator<ClienteUpdateDto> updateValidator,
            IMapper mapper
            )
        {
            _createValidator = createValidator;
            _mediator = mediator;
            _updateValidator = updateValidator;
            _mapper = mapper;
        }

        /// <summary>
        /// Crea un nuevo cliente.
        /// </summary>
        [HttpPost("CreateNewCliente")]
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status201Created)]
        public async Task<ActionResult> Add(
            [FromBody] ClienteCreateDto createDto,
            CancellationToken cancellationToken
        )
        {
            var validationResult = await _createValidator.ValidateAsync(createDto);

            if (!validationResult.IsValid) 
               return BadRequest(validationResult.Errors);

            var command = _mapper.Map<CreateClienteCommand>(createDto);

            Guid newId = await _mediator.Send(command, cancellationToken);

            var cliente = await _mediator.Send(new GetClienteByIdQuery(newId), cancellationToken);

            var responseDto = _mapper.Map<ClienteResponseDto>(cliente);

            return    CreatedAtAction(nameof(GetById), new { id = newId }, responseDto);                     
        }

        /// <summary>
        /// Obtiene un cliente por su identificador.
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ActionResult<ClienteResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ClienteResponseDto>> GetById(
            Guid id,
            CancellationToken cancellationToken
        )
        {
            try
            {
                var cliente = await _mediator.Send(
                    new GetClienteByIdQuery(id),
                    cancellationToken
                );

                var dto = _mapper.Map<ClienteResponseDto>(cliente);
                return Ok(dto);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Devuelve una lista de clientes
        /// <summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ClienteResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ClienteResponseDto>>> GetAll(
            CancellationToken cancellationToken,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 15
        )
        {
            try
            {
                var query = new ListClientesQuery(pageNumber, pageSize);
                var clientes = await _mediator.Send(query, cancellationToken);
                var listDto = _mapper.Map<IEnumerable<ClienteResponseDto>>(clientes);
                return Ok(listDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Actualiza un cliente segun su id
        /// <summary>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Update(
            Guid id,
            [FromBody] ClienteUpdateDto updateDto,
            CancellationToken cancellationToken
        )
        {
            var validationResult = await _updateValidator.ValidateAsync(updateDto);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<UpdateClienteCommand>(updateDto)
                            with { Id = id};

            try
            {
                await _mediator.Send(command, cancellationToken);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Elimina un cliente segun su Id
        /// <summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                await _mediator.Send(new DeleteClienteCommand(id), cancellationToken);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"El cliente con el id {id} no se ha encontrado");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Agrega un oficio a un cliente segun si Id
        /// <summary>
        [HttpPost("{clienteId:guid}/agregarOficio")]
        [ProducesResponseType(typeof(IEnumerable<string>),
            StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<string>>> AgregarOficio(
            Guid clienteId,
            [FromBody] string oficio,
            CancellationToken cancellationToken
        )
        {
            if (string.IsNullOrWhiteSpace(oficio))
                return BadRequest("El oficio no puede estar vacio");

            try
            {
                var oficios = await _mediator.Send(new AgregarOficioCommand(clienteId, oficio), cancellationToken);
                return Ok(oficios);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Cliente con ID {clienteId} no encontrado.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// Elimina un oficio del cliente segun su Id
        /// <summary>
        [HttpPut("{clienteId:guid}/eliminarOficio")]
        [ProducesResponseType(typeof(IEnumerable<string>),
            StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<string>>> EliminarOficio(
            Guid clienteId,
            [FromBody] string oficio,
            CancellationToken cancellationToken
        )
        {
            if (string.IsNullOrWhiteSpace(oficio))
                return BadRequest("Se envio un oficio vacio o nulo");

            try
            {
                var oficios = await _mediator.Send(new EliminarOficioCommand(clienteId, oficio), cancellationToken);
                return Ok(oficios);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"El cliente con id {clienteId} no se ha encontrado");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Reemplaza la lista de oficios por una nueva, segun el id del cliente
        /// <summary>
        [HttpPost("{clienteId:guid}/reemplazarOficios")]
        [ProducesResponseType(typeof(IEnumerable<string>),
            StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<string>>> ReemplazarOficios(
            Guid clienteId,
            [FromBody] IEnumerable<string> oficiosNuevos,
            CancellationToken cancellationToken
        )
        {
            foreach (var oficio in oficiosNuevos)
            {
                if (string.IsNullOrWhiteSpace(oficio))
                    return BadRequest("No se deben agregar oficios nulos o vacios");
            }
            try
            {
                var oficios = await _mediator.Send(new UpdateOficiosCommand(clienteId, oficiosNuevos), cancellationToken);
                return Ok(oficios);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"El cliente con id {clienteId} no se ha encontrado");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Genera un vinculo nuevo entre un cliente y una empresa
        /// <summary>
        [HttpPost("{clienteId:guid}/vincularEmpresa")]
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> VinvularEmpresa(
            Guid clienteId,
            [FromBody] EmpresaClienteCreateDto dto,
            CancellationToken cancellationToken
        )
        {
            var command = _mapper.Map<VincularEmpresaCommand>(dto)
                            with
            { ClienteId = clienteId };

            try
            {
                await _mediator.Send(command, cancellationToken);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}