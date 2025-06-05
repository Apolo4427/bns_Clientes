using MediatR;
using Microsoft.EntityFrameworkCore;
using ModuloClientes.Aplication.Command.SeguroSaludCommands;
using ModuloClientes.Core.Models.ValueObjects.EmpresaValueObjects;
using ModuloClientes.Core.Models.ValueObjects.SeguroSaludValueObjects;
using ModuloClientes.Core.Ports.IRepositories;

namespace ModuloClientes.Aplication.Handlers.SeguroSaludHandler
{
    public class UpdateSeguroSaludHandler : IRequestHandler<UpdateSeguroSaludCommand>
    {
        private readonly ISeguroSaludRepository _repo;

        public UpdateSeguroSaludHandler(ISeguroSaludRepository repo)
        {
            _repo = repo;
        }
        public async Task Handle(UpdateSeguroSaludCommand request, CancellationToken cancellationToken)
        {
            var seguroSalud = await _repo.GetByIdAsync(request.Id, cancellationToken)
                ?? throw new KeyNotFoundException(
                    $"El seguro con id {request.Id} no ha sido encontrado"
                );

            if (!seguroSalud.RowVersion.SequenceEqual(request.RowVersion))
                throw new DbUpdateConcurrencyException(
                    $"El seguro con id {request.Id} ya ha sido modificado por otro proceso, por favor refresque la pagina he intente de nuevo"
                );

            // Actualización de propiedades básicas
            if (!string.IsNullOrWhiteSpace(request.Proveedor))
                seguroSalud.CambiarProveedor(new CompanyName(request.Proveedor));
            
            if (!string.IsNullOrWhiteSpace(request.NombrePlan))
                seguroSalud.CambiarNombrePlan(new PlanName(request.NombrePlan));
            
            if (!string.IsNullOrWhiteSpace(request.NumeroPoliza))
                seguroSalud.CambiarNumeroPoliza(new PolicyNumber(request.NumeroPoliza));
            
            if (request.FechaInicio.HasValue)
                seguroSalud.CambiarFechaInicio(request.FechaInicio.Value);
            
            if (request.FechaFin.HasValue)
                seguroSalud.RenovarPoliza(request.FechaFin.Value);
            
            if (request.PrimaMensual.HasValue)
                seguroSalud.CambiarPrimaMensual(request.PrimaMensual.Value);

            try
            {
                await _repo.UpdateAsync(seguroSalud, cancellationToken);           
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new DbUpdateConcurrencyException(
                    $"El seguro de id { request.Id } ya ha sido modificado por otro proceso"
                );
            }
            
        }
    }
}