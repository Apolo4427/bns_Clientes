

namespace ModuloClientes.API.DTOs.Update
{
    public class SeguroSaludUpdateDto
    {
        /// <summary>
        /// Nuevo nombre del proveedor (Ambetter, Cigna, BCBS, etc.).
        /// Si es null, no se modifica.
        /// </summary>
        public string? Proveedor { get; set; }

        /// <summary>
        /// Nuevo nombre del plan. Si es null, no se modifica.
        /// </summary>
        public string? NombrePlan { get; set; }

        /// <summary>
        /// Nuevo número de póliza. Si es null, no se modifica.
        /// </summary>
        public string? NumeroPoliza { get; set; }

        /// <summary>
        /// Nueva fecha de inicio de la póliza. Si es null, no se modifica.
        /// </summary>
        public DateTime? FechaInicio { get; set; }

        /// <summary>
        /// Nueva fecha de fin de la póliza. Si es null, no se modifica.
        /// </summary>
        public DateTime? FechaFin { get; set; }

        /// <summary>
        /// Nueva prima mensual. Si es null, no se modifica.
        /// </summary>
        public decimal? PrimaMensual { get; set; }

        public string RowVersion { get; set; } = null!;
    }
}
