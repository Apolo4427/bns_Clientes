namespace ModuloClientes.Core.Ports.Queries
{
    /// <summary>
    /// Handler genérico para consultas (queries) que devuelven un resultado.
    /// </summary>
    /// <typeparam name="TQuery">Tipo de consulta.</typeparam>
    /// <typeparam name="TResult">Tipo de dato devuelto.</typeparam>
    public interface IQueryHandler<in TQuery, TResult>
    {
        Task<TResult> HandleAsync(TQuery query);
    }
}