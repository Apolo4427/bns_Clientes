using AutoMapper;
using ModuloClientes.Core.Ports.IRepositories;
using ModuloClientes.Core.Models;
using MediatR;
using ModuloClientes.Aplication.Command.ClienteCommands;

namespace ModuloClientes.Aplication.Handlers.ClienteHandler
{
    public class CreateClienteHandler : IRequestHandler<CreateClienteCommand, Guid>
    {
        private readonly IClienteRepository _repository;
        private readonly IMapper _mapper;
    
        public CreateClienteHandler(IClienteRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        
        public async Task<Guid> Handle(
            CreateClienteCommand command,
            CancellationToken ct
        )
        {
            var cliente = _mapper.Map<Cliente>(command);  
            await _repository.AddAsync(cliente, ct);
            return cliente.Id;  
        }
    }
}