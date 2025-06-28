using ModuloClientes.Core.Enums;

namespace ModuloClientes.Core.Models
{
    public static class ClienteRelationshipServiceHelper
    {
        public static void VincularBidireccional(
            Cliente a,
            Cliente b,
            TipoRelacion tipo,
            bool esDependiente)
        {
            a.AgregarRelacionUnidireccional(b, tipo, esDependiente);

            var tipoInv = tipo switch
            {
                TipoRelacion.Padre    => TipoRelacion.Hijo,
                TipoRelacion.Hijo     => TipoRelacion.Padre,
                TipoRelacion.Conyuge  => TipoRelacion.Conyuge,
                TipoRelacion.Otro     => TipoRelacion.Otro,
                _                      => tipo
            };

            var depInv = tipoInv == TipoRelacion.Hijo;

            b.AgregarRelacionUnidireccional(a, tipoInv, depInv);
        }
    }
}