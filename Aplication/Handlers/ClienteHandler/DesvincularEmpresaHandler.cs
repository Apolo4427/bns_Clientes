using MediatR;
using ModuloClientes.Aplication.Command.ClienteCommands;
using ModuloClientes.Core.Ports.Repositories;

namespace ModuloClientes.Infrastructure.Persistence.Handlers.ClienteHandler
{
    public class DesvincularEmpresaHandler : IRequestHandler<DesvincularEmpresaCommand>
    {
        private readonly IClienteRepository _clienteRepo;

        public DesvincularEmpresaHandler(IClienteRepository clienteRepo)
        {
            _clienteRepo = clienteRepo;
        }
        public async Task Handle(DesvincularEmpresaCommand command, CancellationToken ct)
        {
            
            var cliente = await _clienteRepo.GetByIdAsync(command.ClienteId, ct);

            cliente.DesvincularEmpresa(command.EmpresaId);

            await _clienteRepo.UpdateAsync(cliente, ct);
        }
    }
}