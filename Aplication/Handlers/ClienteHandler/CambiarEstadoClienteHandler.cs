using MediatR;
using Microsoft.EntityFrameworkCore;
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
        public async Task<EstadoCliente> Handle(CambiarEstadoClienteCommand request, CancellationToken cancellationToken)
        {
            var cliente = await _repo.GetByIdAsync(request.Id, cancellationToken)
                ?? throw new KeyNotFoundException(
                    $"El cliente con el id {request.Id} no ha sido encontrado"
                );

            var nuevoEstado = cliente.Estado == EstadoCliente.Activo
                ? EstadoCliente.Prospecto
                : EstadoCliente.Activo;

            cliente.CambiarEstado(nuevoEstado);

            try
            {
                await _repo.UpdateAsync(cliente, cancellationToken);
                return cliente.Estado;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new DbUpdateConcurrencyException(
                    $"El cliente con id {cliente.Id} ya ha sido modificado por otro proceso"
                );
            }
        }
    }
}