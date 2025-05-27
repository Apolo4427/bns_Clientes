using System.Data;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ModuloClientes.API.DTOs.Create;
using ModuloClientes.API.DTOs.Response;
using ModuloClientes.API.DTOs.Update;
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
        private readonly IValidator<EmpresaUpdateDto> _updateValidator;
        private readonly ICreateEmpresaCommandHandler _createEmpresaHandler;
        private readonly IUpdateEmpresaCommandHandler _updateHandler;
        private readonly IDeleteEmpresaCommandHandler _deleteHandler;
        private readonly IGetEmpresaByIdQueryHandler _getEmpresaByIdHandler;
        private readonly IListEmpresasQueryHandler _listEmprsaHandler;
        private readonly IMapper _mapper;

        public EmpresaController(
            IValidator<EmpresaCreateDto> createValidator,
            IValidator<EmpresaUpdateDto> updateValidator,
            ICreateEmpresaCommandHandler createEmpresa,
            IUpdateEmpresaCommandHandler updateHandler,
            IDeleteEmpresaCommandHandler deleteHandler,
            IGetEmpresaByIdQueryHandler getEmpresa,
            IListEmpresasQueryHandler listEmpresasQuery,
            IMapper mapper
        )
        {
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _createEmpresaHandler = createEmpresa;
            _updateHandler = updateHandler;
            _deleteHandler = deleteHandler;
            _getEmpresaByIdHandler = getEmpresa;
            _listEmprsaHandler = listEmpresasQuery;
            _mapper = mapper;
        }

        /// <summary>
        /// Crea una nueva empresa
        /// <summary>
        [HttpPost("createNewEmpresa")]
        public async Task<ActionResult> Add([FromBody] EmpresaCreateDto dto)
        {
            var validationResult = await _createValidator.ValidateAsync(dto);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<CreateEmpresaCommand>(dto);

            Guid newId = await _createEmpresaHandler.HandleAsync(command);

            var empresa = await _getEmpresaByIdHandler.HandleAsync(new GetEmpresaByIdQuery(newId));

            var responseDto = _mapper.Map<EmpresaResponseDto>(empresa);

            return CreatedAtAction(nameof(GetById), new { id = newId }, responseDto);
        }

        /// <summary>
        /// obtiene una empresa por su id
        /// <summary>
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<EmpresaResponseDto>> GetById(Guid id)
        {
            var empresa = await _getEmpresaByIdHandler.HandleAsync(new GetEmpresaByIdQuery(id));

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
            [FromQuery] int pageSize
        )
        {
            var query = new ListEmpresasQuery(pageNumber, pageSize);
            var empresas = await _listEmprsaHandler.HandleAsync(query);
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
            [FromBody] EmpresaUpdateDto updateDto
        )
        {
            var result = await _updateValidator.ValidateAsync(updateDto);
            if (!result.IsValid)
                return BadRequest(result.Errors);

            var command = _mapper.Map<UpdateEmpresaCommand>(updateDto)
                                with
            { Id = id };

            try
            {
                await _updateHandler.HandleAsync(command);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (DBConcurrencyException)
            {
                return Conflict();
            }
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(typeof(ActionResult),
            StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                await _deleteHandler.HandleAsync(new DeleteEmpresaCommand(id));
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