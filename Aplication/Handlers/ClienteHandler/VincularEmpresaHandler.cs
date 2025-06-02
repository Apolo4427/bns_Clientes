using MediatR;
using ModuloClientes.Aplication.Command.ClienteCommands;
using ModuloClientes.Core.Ports.Repositories;

namespace ModuloClientes.Infrastructure.Persistence.Handlers.ClienteHandler
{
    public class VincularEmpresaHandler : IRequestHandler<VincularEmpresaCommand>
    {
        private readonly IClienteRepository _clienteRepo;
        private readonly IEmpresaRepository _empresaRepo;

        public VincularEmpresaHandler(IClienteRepository clienteRepository,
             IEmpresaRepository empresaRepository)
        {
            _clienteRepo = clienteRepository;
            _empresaRepo = empresaRepository;
        }
        public async Task Handle(VincularEmpresaCommand command, CancellationToken ct)
        {
            var cliente = await _clienteRepo.GetByIdAsync(command.ClienteId, ct)
                ?? throw new KeyNotFoundException($"El cliente con id: {command.ClienteId} no se ha encontrado");
            var empresa = await _empresaRepo.GetByIdAsync(command.EmpresaId)
                ?? throw new KeyNotFoundException($"La empresa con el id: {command.EmpresaId} no ha sido encontrada");

            cliente.VincularEmpresa(empresa, command.Rol, command.FechaVinculacion);

            await _clienteRepo.UpdateAsync(cliente, ct);
            // await _empresaRepo.UpdateAsync(empresa, ct); // TODO: Validar si es necesario
        }
    }
}