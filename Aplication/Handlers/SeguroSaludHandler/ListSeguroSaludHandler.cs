using MediatR;
using ModuloClientes.Core.Models;
using ModuloClientes.Core.Ports.Queries.SeguroSaludQueries;

namespace ModuloClientes.Aplication.Handlers
{
    public class ListSeguroSaludHandler : IRequestHandler<ListSegurosSaludQuery, IReadOnlyList<SeguroSalud>>
    {
        public Task<IReadOnlyList<SeguroSalud>> Handle(ListSegurosSaludQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}