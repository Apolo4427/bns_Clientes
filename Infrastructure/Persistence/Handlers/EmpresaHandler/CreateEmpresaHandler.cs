using AutoMapper;
using ModuloClientes.Core.Models;
using ModuloClientes.Core.Ports.Commands.EmpresaCommands;
using ModuloClientes.Core.Ports.Repositories;

namespace ModuloClientes.Infrastructure.Persistence.Handlers.EmpresaHandler
{
    public class CreateEmpresaHandler : ICreateEmpresaCommandHandler
    {
        private readonly IEmpresaRepository _repo;
        private readonly IMapper _mapper;

        public CreateEmpresaHandler(IEmpresaRepository repository, IMapper mapper)
        {
            _repo = repository;
            _mapper = mapper;
        }
        public async Task<int> HandleAsync(CreateEmpresaCommand command)
        {
            var empresa = _mapper.Map<Empresa>(command);
            await _repo.AddAsync(empresa);
            return empresa.Id;
        }
    }
}