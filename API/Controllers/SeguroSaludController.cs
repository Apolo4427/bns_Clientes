using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ModuloClientes.API.DTOs.Create;
using ModuloClientes.Aplication.Command.SeguroSaludCommands;

namespace ModuloClientes.API.Controllers
{
    [ApiController]
    [Route("seguroSalud")]
    public class SeguroSaludController : ControllerBase
    {
        private readonly IValidator<SeguroSaludCreateDto> _createValidator;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public SeguroSaludController(
            IValidator<SeguroSaludCreateDto> createValidator,
            IMediator mediator,
            IMapper mapper
        )
        {
            _createValidator = createValidator;
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Crea una nueva empresa
        /// <summary>
        [HttpPost("createNewSeguro")]
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
        }

    }
}