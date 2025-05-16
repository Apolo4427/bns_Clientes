using ModuloClientes.Core.Enums;

namespace ModuloClientes.API.DTOs.Update
{
    /// <summary>
    /// Propiedades opcionales para actualizar una relación existente.
    /// </summary>
    public class ClienteRelacionUpdateDto
    {
        /// <summary>
        /// Nuevo tipo de relación (si se desea cambiar).
        /// </summary>
        public TipoRelacion? Tipo { get; set; }

        /// <summary>
        /// Indica si el relacionado pasa a ser dependiente o no.
        /// </summary>
        public bool? EsDependiente { get; set; }
    }
}