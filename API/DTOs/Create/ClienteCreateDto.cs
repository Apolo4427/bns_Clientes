namespace ModuloClientes.API.DTOs.Create
{
    public class ClienteCreateDto
    {
        // Datos personales
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public DateTime FechaNacimiento { get; set; }

        // Datos fiscales
        public string EstadoCivil { get; set; } = null!;
        public string EstadoTributario { get; set; } = null!;
        public string SocialSecurityNumber { get; set; } = null!;

        // Dirección
        public string Direccion { get; set; } = null!;

        // Oficios o profesiones
        public IList<string> Oficios { get; set; } = new List<string>();

        // (Opcional) Id de póliza de salud existente
        public int? SeguroSaludId { get; set; }
    }
}
