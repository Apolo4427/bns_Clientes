using MediatR;
using ModuloClientes.Aplication.Command.ClienteCommands;
using ModuloClientes.Core.Ports.IRepositories;

namespace ModuloClientes.Aplication.Handlers.ClienteHandler
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