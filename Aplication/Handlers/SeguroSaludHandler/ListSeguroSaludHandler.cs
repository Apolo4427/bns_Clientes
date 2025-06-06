using MediatR;
using ModuloClientes.Core.Models;
using ModuloClientes.Core.Ports.IRepositories;
using ModuloClientes.Core.Ports.Queries.SeguroSaludQueries;

namespace ModuloClientes.Aplication.Handlers
{
    public class ListSeguroSaludHandler : IRequestHandler<ListSegurosSaludQuery, IReadOnlyList<SeguroSalud>>
    {
        private readonly ISeguroSaludRepository _repo;

        public ListSeguroSaludHandler(ISeguroSaludRepository repository)
        {
            _repo = repository;
        }
        public async Task<IReadOnlyList<SeguroSalud>> Handle(ListSegurosSaludQuery request, CancellationToken cancellationToken)
        {
            return await _repo.ListAsync(request.PageNumber, request.PageSize, cancellationToken);
        }
    }
}