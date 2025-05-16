using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ModuloClientes.API.DTOs.Create;
using ModuloClientes.API.DTOs.Response;
using ModuloClientes.Core.Models;
using ModuloClientes.Core.Ports.Commands.EmpresaCommands;
using ModuloClientes.Core.Ports.Queries.EmpresaQueries;

namespace ModuloClientes.API.Controllers
{
    [ApiController]
    [Route("empresas")]
    public class EmpresaController : ControllerBase
    {
        private readonly IValidator<EmpresaCreateDto> _createValidator;
        private readonly ICreateEmpresaCommandHandler _createEmpresaHandler;
        private readonly IGetEmpresaByIdQueryHandler _getEmpresaByIdHandler;
        private readonly IMapper _mapper;

        public EmpresaController(
            IValidator<EmpresaCreateDto> createValidator,
            ICreateEmpresaCommandHandler createEmpresa,
            IGetEmpresaByIdQueryHandler getEmpresa,
            IMapper mapper
        )
        {
            _createValidator = createValidator;
            _createEmpresaHandler = createEmpresa;
            _getEmpresaByIdHandler = getEmpresa;
            _mapper = mapper;
        }

        /// <summary>
        /// Crea una nueva empresa
        /// <summary>
        [HttpPost("createNewEmpresa")]
        public async Task<ActionResult> Add([FromBody] EmpresaCreateDto dto)
        {
            var validationResult = await _createValidator.ValidateAsync(dto);

            if(!validationResult.IsValid)
                BadRequest(validationResult.Errors);

            var command = _mapper.Map<CreateEmpresaCommand>(dto);

            int newId = await _createEmpresaHandler.HandleAsync(command);

            var empresa = await _getEmpresaByIdHandler.HandleAsync(new GetEmpresaByIdQuery(newId));

            var responseDto = _mapper.Map<EmpresaResponseDto>(empresa);

            return CreatedAtAction(nameof(GetById), new { id = newId}, responseDto);
        }

        /// <summary>
        /// obtiene una empresa por su id
        /// <summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<EmpresaResponseDto>> GetById(int id)
        {
            var empresa = await _getEmpresaByIdHandler.HandleAsync(new GetEmpresaByIdQuery(id));

            var dto = _mapper.Map<EmpresaResponseDto>(empresa);

            return Ok(dto);
        }
    }
}