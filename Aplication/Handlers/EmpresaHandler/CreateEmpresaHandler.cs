using AutoMapper;
using MediatR;
using ModuloClientes.Core.Models;
using ModuloClientes.Core.Ports.Commands.EmpresaCommands;
using ModuloClientes.Core.Ports.IRepositories;

namespace ModuloClientes.Aplication.Handlers.EmpresaHandler
{
    public class CreateEmpresaHandler : IRequestHandler<CreateEmpresaCommand, Guid>
    {
        private readonly IEmpresaRepository _repo;
        private readonly IMapper _mapper;

        public CreateEmpresaHandler(IEmpresaRepository repository, IMapper mapper)
        {
            _repo = repository;
            _mapper = mapper;
        }
        public async Task<Guid> Handle(CreateEmpresaCommand command, CancellationToken ct)
        {
            var empresa = _mapper.Map<Empresa>(command);
            await _repo.AddAsync(empresa, ct);
            return empresa.Id;
        }
    }
}