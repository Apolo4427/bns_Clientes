using ModuloClientes.Core.Ports.Commands.EmpresaCommands;
using ModuloClientes.Core.Ports.Repositories;

namespace ModuloClientes.Infrastructure.Persistence.Handlers.EmpresaHandler
{
    public class DeleteEmpresaHandler : IDeleteEmpresaCommandHandler
    {
        private readonly IEmpresaRepository _repository;

        public DeleteEmpresaHandler(IEmpresaRepository repository)
        {
            _repository = repository;
        }
        public async Task HandleAsync(DeleteEmpresaCommand command)
        {
            await _repository.DeleteAsync(command.Id);
        }
    }
}