using System.Data;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ModuloClientes.API.DTOs.Create;
using ModuloClientes.API.DTOs.Response;
using ModuloClientes.API.DTOs.Update;
using ModuloClientes.Aplication.Command.SeguroSaludCommands;
using ModuloClientes.Core.Ports.Queries.SeguroSaludQueries;

namespace ModuloClientes.API.Controllers
{
    [ApiController]
    [Route("seguroSalud")]
    public class SeguroSaludController : ControllerBase
    {
        private readonly IValidator<SeguroSaludCreateDto> _createValidator;
        private readonly IValidator<SeguroSaludUpdateDto> _updateValidator;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public SeguroSaludController(
            IValidator<SeguroSaludCreateDto> createValidator,
            IValidator<SeguroSaludUpdateDto> updateValidator,
            IMediator mediator,
            IMapper mapper
        )
        {
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Crea una nueva empresa
        /// <summary>
        [HttpPost("createNewSeguro")]
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status201Created)]
        public async Task<ActionResult> Add(
            [FromBody] SeguroSaludCreateDto dto,
            CancellationToken ct
        )
        {
            var validationResult = await _createValidator.ValidateAsync(dto, ct);

            if (!validationResult.IsValid)
                return BadRequest();

            var command = _mapper.Map<CreateSeguroSaludCommand>(dto);

            Guid newId = await _mediator.Send(command, ct);

            var newSeguroSalud = _mediator.Send(new GetSeguroSaludByIdQuery(newId), ct);

            var responseDto = _mapper.Map<SeguroSaludResponseDto>(newSeguroSalud);

            return CreatedAtAction(nameof(GetById), new { id = newId }, responseDto);
        }

        /// <summary>
        /// obtiene una empresa por su id
        /// <summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ActionResult<SeguroSaludResponseDto>),
            StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SeguroSaludResponseDto>> GetById(
            Guid id,
            CancellationToken ct
        )
        {
            try
            {
                var seguroSalud = await _mediator.Send(new GetSeguroSaludByIdQuery(id), ct);

                var responseDto = _mapper.Map<SeguroSaludResponseDto>(seguroSalud);

                return Ok(responseDto);
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
        /// Optine una lista de los Seguros de salud
        /// <summary>
        [HttpGet]
        [ProducesResponseType(
            typeof(IEnumerable<SeguroSaludResponseDto>),
            StatusCodes.Status200OK
        )]
        public async Task<ActionResult<IEnumerable<SeguroSaludResponseDto>>> GetAll(
            [FromQuery] int pageNumber,
            [FromQuery] int pageSize,
            CancellationToken ct
        )
        {
            var query = new ListSegurosSaludQuery(pageNumber, pageSize);
            var segurosSalud = await _mediator.Send(query, ct);
            var responseDtoList = _mapper.Map<IEnumerable<SeguroSaludResponseDto>>(segurosSalud);
            return Ok(responseDtoList);
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(ActionResult),
            StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Update(
            Guid id,
            [FromBody] SeguroSaludUpdateDto createDto,
            CancellationToken ct
        )
        {
            var validationResult = await _updateValidator.ValidateAsync(createDto, ct);

            if (!validationResult.IsValid)
                return BadRequest();

            var command = _mapper.Map<UpdateSeguroSaludCommand>(createDto)
                                with
            { Id = id };

            try
            {
                await _mediator.Send(command, ct);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (DBConcurrencyException ex)
            {
                return Conflict(ex.Message);
            }

        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(typeof(ActionResult),
            StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(
            Guid id,
            CancellationToken ct
        )
        {
            try
            {
                await _mediator.Send(new DeleteSeguroSaludCommand(id), ct);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

    }
}