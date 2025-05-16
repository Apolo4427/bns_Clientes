namespace ModuloClientes.API.DTOs.Response
{
     public class EmpresaResponseDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string EIN { get; set; } = null!;
        public string Direccion { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public string CorreoContacto { get; set; } = null!;
        public DateTime FechaConstitucion { get; set; }
        public List<EmpresaClienteResponseDto> Clientes { get; set; } = new();
    }
}