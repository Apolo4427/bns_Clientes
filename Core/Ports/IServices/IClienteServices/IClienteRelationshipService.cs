using ModuloClientes.Aplication.Command.ClienteCommands;
using ModuloClientes.Core.Enums;

namespace ModuloClientes.Core.Ports.IServices.IClienteServices
{
    public interface IClienteRelationshipService
    {
        /// <summary>
        /// Vincula bidireccionalmente dos clientes a partir de un comando.
        /// </summary>
        Task VincularRelacionAsync(AddClienteRelacionCommand command,
                                    CancellationToken cancellationToken = default);

        /// <summary>
        /// Desvincula bidireccionalmente dos clientes a partir de un comando.
        /// </summary>
        Task DesvincularRelacionAsync(
            DeleteClienteRelacionCommand command,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Cambia el tipo de relación en ambas entidades a partir de un comando.
        /// </summary>
        Task CambiarTipoRelacionAsync(
            ChangeClienteRelacionTypeCommand command,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Cambia la dependencia de la relación en ambas entidades a partir de un comando.
        /// </summary>
        Task CambiarDependenciaRelacionAsync(
            ChangeClienteDependenciaCommand command,
            CancellationToken cancellationToken = default);
    }
}