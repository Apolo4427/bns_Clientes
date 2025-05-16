namespace ModuloClientes.API.DTOs.Create
{
    public class SeguroSaludCreateDto
    {
        /// <summary>
        /// Nombre del proveedor del seguro (p.ej., Ambetter, Cigna, BCBS).
        /// </summary>
        public string Proveedor { get; set; } = null!;

        /// <summary>
        /// Nombre específico del plan de seguro.
        /// </summary>
        public string NombrePlan { get; set; } = null!;

        /// <summary>
        /// Número de póliza.
        /// </summary>
        public string NumeroPoliza { get; set; } = null!;

        /// <summary>
        /// Fecha de inicio de la cobertura.
        /// </summary>
        public DateTime FechaInicio { get; set; }

        /// <summary>
        /// Fecha de fin de la cobertura.
        /// </summary>
        public DateTime FechaFin { get; set; }

        /// <summary>
        /// Prima mensual del plan.
        /// </summary>
        public decimal PrimaMensual { get; set; }
        
        /// <summary>
        /// Lista de IDs de clientes que ya existen y se vinculan a esta póliza.
        /// </summary>
        public IList<int> ClienteIds { get; set; } = new List<int>();
    }
}
