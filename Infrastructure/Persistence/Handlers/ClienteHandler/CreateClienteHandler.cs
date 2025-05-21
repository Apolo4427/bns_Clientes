using AutoMapper;
using ModuloClientes.Core.Ports.Commands.ClienteCommands;
using ModuloClientes.Core.Ports.Repositories;
using ModuloClientes.Core.Models;

namespace ModuloClientes.Infrastructure.Persistence.Handlers.ClienteHandler
{
    public class CreateClienteHandler : ICreateClienteCommandHandler
    {
        private readonly IClienteRepository _repository;
        private readonly IMapper _mapper;
    
        public CreateClienteHandler(IClienteRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        
        public async Task<Guid> HandleAsync(CreateClienteCommand command)
        {
            var cliente = _mapper.Map<Cliente>(command);  
            await _repository.AddAsync(cliente);
            return cliente.Id;  
        }
    }
}