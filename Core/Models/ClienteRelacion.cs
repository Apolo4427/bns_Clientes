using ModuloClientes.Core.Enums;

namespace ModuloClientes.Core.Models
{
    public class ClienteRelacion
    {
        public int ClienteId { get; private set; } //TODO: cambiar int por Guid en los ids
        public Cliente Cliente { get; private set; }

        public int RelacionadoId { get; private set; }
        public Cliente Relacionado { get; private set; }

        public TipoRelacion Tipo { get; private set; }
        public byte[] RowVersion { get; private set; }

        /// <summary>
        /// Indica si la persona relacionada es dependiente financiero.
        /// </summary>
        public bool EsDependiente { get; private set; }

        private ClienteRelacion() { }

        public ClienteRelacion(Cliente cliente, Cliente relacionado, TipoRelacion tipo, bool esDependiente)
        {
            Cliente = cliente ?? throw new ArgumentNullException("EL cliente no puede ser un valor nulo",nameof(cliente));
            Relacionado = relacionado ?? throw new ArgumentNullException("El relacionado no puede ser un valor nulo",nameof(relacionado));
            ClienteId = cliente.Id;
            RelacionadoId = relacionado.Id;
            Tipo = tipo;
            EsDependiente = esDependiente;
        }

        /// <summary>
        /// Actualiza la marca de dependencia.
        /// </summary>
        public void CambiarDependencia(bool esDependiente)
        {
            EsDependiente = esDependiente;
        }

        public void CambiarTipoRelacion(TipoRelacion NuevoTipoRelacion)
        {
            if(!Enum.IsDefined(NuevoTipoRelacion))
                throw new ArgumentOutOfRangeException(nameof(NuevoTipoRelacion),
                    "El tipo de relacion enviado no es valido");
            Tipo = NuevoTipoRelacion;
        }
    }
}