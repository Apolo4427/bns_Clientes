using MediatR;
using ModuloClientes.Core.Ports.Commands.ClienteCommands;
using ModuloClientes.Core.Ports.Repositories;

namespace ModuloClientes.Infrastructure.Persistence.Handlers.ClienteHandler
{
    public class DeleteClienteHandler : IRequestHandler<DeleteClienteCommand>
    {
        private readonly IClienteRepository _repo;

        public DeleteClienteHandler(IClienteRepository repository)
        {
            _repo = repository;
        }

        public async Task Handle(DeleteClienteCommand command, CancellationToken ct)
        {
            await _repo.DeleteAsync(command.Id, ct); 
        }
    }
}