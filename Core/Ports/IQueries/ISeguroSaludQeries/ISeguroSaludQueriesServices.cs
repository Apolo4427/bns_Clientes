using MediatR;
using ModuloClientes.Core.Models;
using System;

namespace ModuloClientes.Core.Ports.Queries.SeguroSaludQueries
{
    /// <summary>
    /// Consulta para obtener un SeguroSalud por su identificador.
    /// </summary>
    public record GetSeguroSaludByIdQuery(Guid Id) : IRequest<SeguroSalud>;

    /// <summary>
    /// Handler para GetSeguroSaludByIdQuery.
    /// </summary>
    public interface IGetSeguroSaludByIdQueryHandler
        : IQueryHandler<GetSeguroSaludByIdQuery, SeguroSalud>
    {
    }
    
    /// <summary>
    /// Consulta para listar todos los Seguros de Salud (con paginación).
    /// </summary>
    public record ListSegurosSaludQuery(
        int PageNumber = 1,   // página 1 por defecto
        int PageSize   = 15   // 15 items por página
    ) : IRequest<IReadOnlyList<SeguroSalud>>;
    
    /// <summary>
    /// Handler para ListSegurosSaludQuery.
    /// </summary>
    public interface IListSegurosSaludQueryHandler
        : IQueryHandler<ListSegurosSaludQuery, IReadOnlyList<SeguroSalud>>
    {
    }
}
