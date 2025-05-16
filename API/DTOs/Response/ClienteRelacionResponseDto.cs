using ModuloClientes.Core.Enums;

namespace ModuloClientes.API.DTOs.Response
{
    /// <summary>
    /// Forma en que se devuelve la relación entre clientes.
    /// </summary>
    public class ClienteRelacionResponseDto
    {
        public int ClienteId { get; set; }
        public string NombreCliente { get; set; } = null!;
        public int RelacionadoId { get; set; }
        public string NombreRelacionado { get; set; } = null!;
        public TipoRelacion Tipo { get; set; }
        public bool EsDependiente { get; set; }
    }
}