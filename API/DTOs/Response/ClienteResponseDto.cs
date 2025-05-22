namespace ModuloClientes.API.DTOs.Response
{
    public class ClienteResponseDto
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public int Edad { get; set; }
        public string Correo { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public DateTime FechaNacimiento { get; set; }
        public string EstadoCivil { get; set; } = null!;
        public string EstadoTributario { get; set; } = null!;
        public string SocialSecurityNumber { get; set; } = null!;
        public List<string> Oficio { get; set; } = new(); // new
        public string Direccion { get; set; } = null!;
        public List<EmpresaClienteResponseDto> Empresas { get; set; } = new();

        public Guid? SeguroSaludId { get; set; }
        public string? NombreSeguroSalud { get; set; }
        public string RowVersion { get; set; } = null!; // base64
    }
}