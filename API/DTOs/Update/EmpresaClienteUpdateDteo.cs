namespace ModuloClientes.API.DTOs.Update
{
    public class EmpresaClienteUpdateDto
    {
        /// <summary>
        /// Nuevo rol en la empresa (p.ej. "Dueño", "Socio").
        /// Si es null, no se modifica.
        /// </summary>
        public string? Rol { get; set; }

        /// <summary>
        /// Nueva fecha de vinculación. Si es null, no se modifica.
        /// </summary>
        public DateTime? FechaVinculacion { get; set; }
    }
}
