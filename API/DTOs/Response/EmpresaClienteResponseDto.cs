namespace ModuloClientes.API.DTOs.Response
{
    public class EmpresaClienteResponseDto
    {
        public int ClienteId { get; set; }
        public string NombreCliente {get; set;} = null!;
        public int EmpresaId { get; set; }
        public string NombreEmpresa {get; set;} = null!;
        public string Rol { get; set; } = null!;
        public DateTime FechaVinculacion { get; set; }
    }
}