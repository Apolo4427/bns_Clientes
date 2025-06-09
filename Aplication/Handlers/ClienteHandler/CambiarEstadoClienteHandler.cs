using MediatR;
using ModuloClientes.Aplication.Command.ClienteCommands;
using ModuloClientes.Core.Enums;
using ModuloClientes.Core.Ports.IRepositories;

namespace ModuloClientes.Aplication.Handlers.ClienteHandler
{
    public class CambiarEstadoclienteHandler
        : IRequestHandler<CambiarEstadoClienteCommand, EstadoCliente>
    {
        private readonly IClienteRepository _repo;

        public CambiarEstadoclienteHandler(IClienteRepository repository)
        {
            _repo = repository;
        }
        public Task<EstadoCliente> Handle(CambiarEstadoClienteCommand request, CancellationToken cancellationToken)
        {
            
        }
    }
}