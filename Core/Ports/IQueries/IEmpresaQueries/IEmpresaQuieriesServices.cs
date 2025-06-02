using MediatR;
using ModuloClientes.Core.Models;
namespace ModuloClientes.Core.Ports.Queries.EmpresaQueries
{
    /// <summary>
    /// Consulta para obtener un empresa por su identificador.
    /// </summary>
    public record GetEmpresaByIdQuery(Guid Id) : IRequest<Empresa>;

    /// <summary>
    /// Handler para GetEmpresaByIdQuery.
    /// </summary>
    public interface IGetEmpresaByIdQueryHandler : IQueryHandler<GetEmpresaByIdQuery, Empresa>
    {}

    /// <summary>
    /// Consulta para listar todos las empresas.
    /// </summary>
    public record ListEmpresasQuery(
        int PageNumber =  1,
        int PageSize = 15
    ):IRequest<IReadOnlyList<Empresa>>;

    /// <summary>
    /// Handler pada ListEmpresasQuery
    /// <summary> 
    public interface IListEmpresasQueryHandler : IQueryHandler<ListEmpresasQuery, IEnumerable<Empresa>>
    {}
}