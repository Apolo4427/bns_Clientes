namespace ModuloClientes.API.DTOs.Update
{
    public class EmpresaUpdateDto
    {
        /// <summary>
        /// Nombre de la empresa. Si es null, no se modifica.
        /// </summary>
        public string? Nombre { get; set; }

        /// <summary>
        /// EIN (Employer Identification Number). Si es null, no se modifica.
        /// </summary>
        public string? EIN { get; set; }

        /// <summary>
        /// Dirección de la empresa. Si es null, no se modifica.
        /// </summary>
        public string? Direccion { get; set; }

        /// <summary>
        /// Teléfono de contacto. Si es null, no se modifica.
        /// </summary>
        public string? Telefono { get; set; }

        /// <summary>
        /// Correo de contacto. Si es null, no se modifica.
        /// </summary>
        public string? CorreoContacto { get; set; }

        /// <summary>
        /// Fecha de constitución. Si es null, no se modifica.
        /// </summary>
        public DateTime? FechaConstitucion { get; set; }
    }
}
