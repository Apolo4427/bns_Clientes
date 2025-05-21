namespace ModuloClientes.API.DTOs.Create
{
     public class EmpresaClienteCreateDto
    {
        public Guid ClienteId { get; set; }          // FK del cliente
        public Guid EmpresaId { get; set; }          // FK de la empresa
        public string Rol { get; set; } = null!;    // “Dueño”, “Socio”, etc.
        public DateTime FechaVinculacion { get; set; }
    }
}