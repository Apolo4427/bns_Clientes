using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ModuloClientes.API.DTOs.Create;
using ModuloClientes.API.DTOs.Response;
using ModuloClientes.API.DTOs.Update;
using ModuloClientes.Core.Ports.Commands.ClienteCommands;
using ModuloClientes.Core.Ports.Queries.ClienteQueries;
//TODO: implementar cancelations tokens

namespace ModuloClientes.API.Controllers
{
    [ApiController]
    [Route("clientes")]
    public class ClienteController : ControllerBase
    {
        private readonly IValidator<ClienteCreateDto> _createValidator;
        private readonly ICreateClienteCommandHandler _createHandler;
        private readonly IGetClienteByIdQueryHandler _getClienteById;
        private readonly IListClientesQueryHandler _listClientes;
        private readonly IUpdateClienteCommandHandler _updateHandler;
        private readonly IVincularEmpresaCommandHandler _vincularHandler;
        private readonly IMapper _mapper;

        public ClienteController(
            IValidator<ClienteCreateDto> createValidator,
            ICreateClienteCommandHandler createClienteCommandHandler,
            IGetClienteByIdQueryHandler getClienteByIdQuery,
            IListClientesQueryHandler listClientesQueryHandler,
            IUpdateClienteCommandHandler updateHandler,
            IVincularEmpresaCommandHandler vincular,
            IMapper mapper
            )
        {
            _createValidator = createValidator;
            _createHandler = createClienteCommandHandler;
            _getClienteById = getClienteByIdQuery;
            _listClientes = listClientesQueryHandler;
            _updateHandler = updateHandler;
            _vincularHandler = vincular;
            _mapper = mapper;
        }

        /// <summary>
        /// Crea un nuevo cliente.
        /// </summary>
        [HttpPost("CreateNewCliente")]
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status201Created)]
        public async Task<ActionResult> Add([FromBody] ClienteCreateDto createDto)
        {
            var validationResult = await _createValidator.ValidateAsync(createDto);

            if (!validationResult.IsValid) 
                BadRequest(validationResult.Errors);

            var command = _mapper.Map<CreateClienteCommand>(createDto);

            int newId = await _createHandler.HandleAsync(command);

            var cliente = await _getClienteById.HandleAsync(new GetClienteByIdQuery(newId));

            var responseDto = _mapper.Map<ClienteResponseDto>(cliente);

            return    CreatedAtAction(nameof(GetById), new { id = newId }, responseDto);                     
        }

        /// <summary>
        /// Obtiene un cliente por su identificador.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ActionResult<ClienteResponseDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<ClienteResponseDto>> GetById(int id)
        {
            var cliente = await _getClienteById.HandleAsync(new GetClienteByIdQuery(id));
            var dto = _mapper.Map<ClienteResponseDto>(cliente);
            return Ok(dto);
        } 

        /// <summary>
        /// Devuelve una lista de clientes
        /// <summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ClienteResponseDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ClienteResponseDto>>> GetAll(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 15
        )
        {
            var query =  new ListClientesQuery(pageNumber, pageSize);
            var clientes = await _listClientes.HandleAsync(query);
            var listDto = _mapper.Map<IEnumerable<ClienteResponseDto>>(clientes);
            return Ok(listDto);
        }

        /// <summary>
        /// Actualiza un cliente segun su id
        /// <summary>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Update(
            int id,
            [FromBody] ClienteUpdateDto updateDto)
        {            
            var command = _mapper.Map<UpdateClienteCommand>(updateDto)
                            with { Id = id};

            try
            {
                await _updateHandler.HandleAsync(command);
            }
            catch(KeyNotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }

        /// <summary>
        /// Genera un vinculo nuevo entre un cliente y una empresa
        /// <summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {

        }


        /// <summary>
        /// Genera un vinculo nuevo entre un cliente y una empresa
        /// <summary>
        [HttpPost("{clienteId}/vincularEmpresa")]
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> VinvularEmpresa(
            int clienteId, 
            [FromBody] EmpresaClienteCreateDto dto)
        {
            var command = _mapper.Map<VincularEmpresaCommand>(dto)
                            with { ClienteId = clienteId};
            
            try
            {
                await _vincularHandler.HandleAsync(command);
            }
            catch(KeyNotFoundException)
            {
                return NotFound();
            }
            
            return NoContent();
        }
    }

}