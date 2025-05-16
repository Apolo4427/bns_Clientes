namespace ModuloClientes.API.DTOs.Update
{
    /// <summary>
    /// DTO para actualización parcial de Cliente.
    /// Todas las propiedades son opcionales; solo se modificarán las que se envíen.
    /// </summary>
    public class ClienteUpdateDto
    {
        /// <summary>
        /// Nuevo nombre del cliente.
        /// </summary>
        public string? Nombre { get; set; }

        /// <summary>
        /// Nuevo apellido del cliente.
        /// </summary>
        public string? Apellido { get; set; }

        /// <summary>
        /// Nuevo correo electrónico.
        /// </summary>
        public string? Correo { get; set; }

        /// <summary>
        /// Nuevo teléfono de contacto.
        /// </summary>
        public string? Telefono { get; set; }

        /// <summary>
        /// Por si se equivocaron asignando la fecha de nacimiento 
        /// </summary>
        public DateTime FechaDeNacimiento { get; set;}

        /// <summary>
        /// Nueva dirección.
        /// </summary>
        public string? Direccion { get; set; }

        /// <summary>
        /// Nuevo estado civil.
        /// </summary>
        public string? EstadoCivil { get; set; }

        /// <summary>
        /// Nuevo estado tributario.
        /// </summary>
        public string? EstadoTributario { get; set; }

        /// <summary>
        /// Nueva lista de oficios
        /// </summary>
        public IList<string> Oficios { get; set; } = new List<string>();

    }
}
