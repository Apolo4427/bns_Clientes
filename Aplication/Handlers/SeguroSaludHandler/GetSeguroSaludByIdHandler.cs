using MediatR;
using ModuloClientes.Core.Models;
using ModuloClientes.Core.Ports.IRepositories;
using ModuloClientes.Core.Ports.Queries.SeguroSaludQueries;

namespace ModuloClientes.Aplication.Handlers
{
    public class GetSeguroSaludByIdHandler : IRequestHandler<GetSeguroSaludByIdQuery, SeguroSalud>
    {
        private readonly ISeguroSaludRepository _repo;

        public GetSeguroSaludByIdHandler(ISeguroSaludRepository seguroSaludRepository)
        {
            _repo = seguroSaludRepository;
        }
        public Task<SeguroSalud> Handle(GetSeguroSaludByIdQuery query, CancellationToken cancellationToken)
        {
            var seguroSalud = _repo.GetByIdAsync(query.Id, cancellationToken)
                ?? throw new KeyNotFoundException(
                    $"El seguro de id {query.Id} no ha sido encontrado"
                );
            return seguroSalud;
        }
    }
}