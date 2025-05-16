namespace ModuloClientes.API.DTOs.Create
{
    public class EmpresaCreateDto
    {
        public string Nombre { get; set; } = null!;
        public string EIN { get; set; } = null!;
        public string Direccion { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public string CorreoContacto { get; set; } = null!;
        public DateTime FechaConstitucion { get; set; }
    }
}
