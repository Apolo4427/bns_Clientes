namespace ModuloClientes.API.DTOs.Response
{
    public class SeguroSaludResponseDto
    {
        public int Id { get; set; }
        public string Proveedor { get; set; } = null!;
        public string NombrePlan { get; set; } = null!;
        public string NumeroPoliza { get; set; } = null!;
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public decimal PrimaMensual { get; set; }
        public List<ClienteResponseDto> Clientes { get; set; } = new();
    }
}