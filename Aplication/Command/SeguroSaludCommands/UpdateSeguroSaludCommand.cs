using MediatR;
using System;

namespace ModuloClientes.Aplication.Command.SeguroSaludCommands
{
    /// <summary>
    /// Comando para actualizar datos de un SeguroSalud existente.
    /// Propiedades nulas no se modificarán.
    /// </summary>
    public record UpdateSeguroSaludCommand(
        Guid Id,
        byte[] RowVersion,              // token de concurrencia
        string? Proveedor = null,        // CompanyName.Value
        string? NombrePlan = null,       // PlanName.Value
        string? NumeroPoliza = null,     // PolicyNumber.Value
        DateTime? FechaInicio = null,
        DateTime? FechaFin = null,
        decimal? PrimaMensual = null
    ) : IRequest;
}
