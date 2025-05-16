using ModuloClientes.Core.Ports.Commands.ClienteCommands;
using ModuloClientes.Core.Ports.Repositories;

namespace ModuloClientes.Infrastructure.Persistence.Handlers.ClienteHandler
{
    public class DesvincularEmpresaHandler : IDesvincularEmpresaCommandHandler
    {
        private readonly IClienteRepository _clienteRepo;

        public DesvincularEmpresaHandler(IClienteRepository clienteRepo)
        {
            _clienteRepo = clienteRepo;
        }
        public async Task HandleAsync(DesvincularEmpresaCommand command)
        {
            
            var cliente = await _clienteRepo.GetByIdAsync(command.ClienteId);

            cliente.DesvincularEmpresa(command.EmpresaId);

            await _clienteRepo.UpdateAsync(cliente);
        }
    }
}