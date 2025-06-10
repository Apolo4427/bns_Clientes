using System.Data;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ModuloClientes.API.DTOs.Create;
using ModuloClientes.API.DTOs.Response;
using ModuloClientes.API.DTOs.Update;
using ModuloClientes.Core.Ports.Commands.EmpresaCommands;
using ModuloClientes.Core.Ports.Queries.EmpresaQueries;
using NuGet.Packaging.Signing;

namespace ModuloClientes.API.Controllers
{
    [ApiController]
    [Route("empresas")]
    public class EmpresaController : ControllerBase
    {
        private readonly IValidator<EmpresaCreateDto> _createValidator;
        private readonly IValidator<EmpresaUpdateDto> _updateValidator;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public EmpresaController(
            IValidator<EmpresaCreateDto> createValidator,
            IValidator<EmpresaUpdateDto> updateValidator,
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
        [HttpPost("createNewEmpresa")]
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status201Created)]
        public async Task<ActionResult> Add(
            [FromBody] EmpresaCreateDto dto,
            CancellationToken cancellationToken
        )
        {
            var validationResult = await _createValidator.ValidateAsync(dto, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<CreateEmpresaCommand>(dto);

            Guid newId = await _mediator.Send(command, cancellationToken);

            var empresa = await _mediator.Send(new GetEmpresaByIdQuery(newId), cancellationToken);

            var responseDto = _mapper.Map<EmpresaResponseDto>(empresa);

            return CreatedAtAction(nameof(GetById), new { id = newId }, responseDto);
        }

        /// <summary>
        /// obtiene una empresa por su id
        /// <summary>
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<EmpresaResponseDto>> GetById(
            Guid id,
            CancellationToken cancellationToken
        )
        {
            var empresa = await _mediator.Send(new GetEmpresaByIdQuery(id), cancellationToken);

            var dto = _mapper.Map<EmpresaResponseDto>(empresa);

            return Ok(dto);
        }

        /// <summary>
        /// Optine una lista de las empresas 
        /// <summary>
        [HttpGet]
        [ProducesResponseType(
            typeof(IEnumerable<EmpresaResponseDto>),
            StatusCodes.Status200OK
        )]
        public async Task<ActionResult<IEnumerable<EmpresaResponseDto>>> GetAll(
            [FromQuery] int pageNumber,
            [FromQuery] int pageSize,
            CancellationToken cancellationToken
        )
        {
            var query = new ListEmpresasQuery(pageNumber, pageSize);
            var empresas = await _mediator.Send(query, cancellationToken);
            var listDto = _mapper.Map<IEnumerable<EmpresaResponseDto>>(empresas);

            return Ok(listDto);
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(ActionResult),
            StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Update(
            Guid id,
            [FromBody] EmpresaUpdateDto updateDto,
            CancellationToken cancellationToken
        )
        {
            var result = await _updateValidator.ValidateAsync(updateDto, cancellationToken);
            if (!result.IsValid)
                return BadRequest(result.Errors);

            var command = _mapper.Map<UpdateEmpresaCommand>(updateDto)
                                with { Id = id };

            try
            {
                await _mediator.Send(command, cancellationToken);
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
            CancellationToken cancellationToken
        )
        {
            try
            {
                await _mediator.Send(new DeleteEmpresaCommand(id), cancellationToken);
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