using MediatR;
using ModuloClientes.Core.Ports.Commands.EmpresaCommands;
using ModuloClientes.Core.Ports.Repositories;

namespace ModuloClientes.Infrastructure.Persistence.Handlers.EmpresaHandler
{
    public class DeleteEmpresaHandler : IRequestHandler<DeleteEmpresaCommand>
    {
        private readonly IEmpresaRepository _repository;

        public DeleteEmpresaHandler(IEmpresaRepository repository)
        {
            _repository = repository;
        }
        public async Task Handle(DeleteEmpresaCommand command, CancellationToken ct)
        {
            await _repository.DeleteAsync(command.Id, ct);
        }
    }
}