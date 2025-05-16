using ModuloClientes.Core.Ports.Commands.ClienteCommands;
using ModuloClientes.Core.Ports.Repositories;

namespace ModuloClientes.Infrastructure.Persistence.Handlers.ClienteHandler
{
    public class DeleteClienteHandler : IDeleteClienteCommandHandler
    {
        private readonly IClienteRepository _repo;

        public DeleteClienteHandler(IClienteRepository repository)
        {
            _repo = repository;
        }

        public async Task HandleAsync(DeleteClienteCommand command)
        {
            await _repo.DeleteAsync(command.Id); 
        }
    }
}