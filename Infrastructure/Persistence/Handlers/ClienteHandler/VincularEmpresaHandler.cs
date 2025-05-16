using System.Runtime.InteropServices;
using ModuloClientes.Core.Ports.Commands.ClienteCommands;
using ModuloClientes.Core.Ports.Repositories;

namespace ModuloClientes.Infrastructure.Persistence.Handlers.ClienteHandler
{
    public class VincularEmpresaHandler : IVincularEmpresaCommandHandler
    {
        private readonly IClienteRepository _clienteRepo;
        private readonly IEmpresaRepository _empresaRepo;

        public VincularEmpresaHandler(IClienteRepository clienteRepository,
             IEmpresaRepository empresaRepository)
        {
            _clienteRepo = clienteRepository;
            _empresaRepo = empresaRepository;
        }
        public async Task HandleAsync(VincularEmpresaCommand command)
        {
            var cliente = await _clienteRepo.GetByIdAsync(command.ClienteId);
            var empresa = await _empresaRepo.GetByIdAsync(command.EmpresaId);

            cliente.VincularEmpresa(empresa, command.Rol, command.FechaVinculacion);

            await _clienteRepo.UpdateAsync(cliente);
        }
    }
}