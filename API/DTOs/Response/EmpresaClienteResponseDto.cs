namespace ModuloClientes.API.DTOs.Response
{
    public class EmpresaClienteResponseDto
    {
        public Guid ClienteId { get; set; }
        public string NombreCliente { get; set; } = null!;
        public Guid EmpresaId { get; set; }
        public string NombreEmpresa { get; set; } = null!;
        public string Rol { get; set; } = null!;
        public DateTime FechaVinculacion { get; set; }
        public byte[] RowVersion { get; private set; } = null!; // Base64
    }
}