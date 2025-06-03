using MediatR;

namespace ModuloClientes.Aplication.Command.SeguroSaludCommands
{
    /// <summary>
    /// Comando para crear un nuevo Seguro de salud.
    /// </summary>
    public record CreateSeguroSaludCommand(
        string Proveedor,
        string NombrePlan,
        string NumeroPoliza,
        DateTime FechaInicio,
        DateTime FechaFin,
        decimal PrimaMensual
    ) :IRequest<Guid>;
}