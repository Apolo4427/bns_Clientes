using ModuloClientes.Core.Enums;

namespace ModuloClientes.API.DTOs.Create
{
    /// <summary>
    /// Datos necesarios para crear una relación entre dos clientes.
    /// </summary>
    public class ClienteRelacionCreateDto
    {
        public int ClienteId { get; set; }
        public int RelacionadoId { get; set; }
        public TipoRelacion Tipo { get; set; }
        public bool EsDependiente { get; set; }
    }
}