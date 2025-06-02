using MediatR;
using ModuloClientes.Core.Models;
namespace ModuloClientes.Core.Ports.Queries.ClienteQueries
{
    /// <summary>
    /// Consulta para obtener un cliente por su identificador.
    /// </summary>
    public record GetClienteByIdQuery(Guid Id) : IRequest<Cliente>;

    /// <summary>
    /// Handler para GetClienteByIdQuery.
    /// </summary>
    public interface IGetClienteByIdQueryHandler
        : IQueryHandler<GetClienteByIdQuery, Cliente>
    {
    }

    /// <summary>
    /// Consulta para listar todos los clientes.
    /// </summary>
    public record ListClientesQuery(
        int PageNumber = 1,   // página 1 por defecto
        int PageSize   = 15   // 15 items por página
    ): IRequest<IReadOnlyList<Cliente>>;

    /// <summary>
    /// Handler para ListClientesQuery.
    /// </summary>
    public interface IListClientesQueryHandler
        : IQueryHandler<ListClientesQuery, IEnumerable<Cliente>>
    {
    }
}