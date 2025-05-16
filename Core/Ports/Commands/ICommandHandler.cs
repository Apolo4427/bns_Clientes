namespace ModuloClientes.Core.Ports.Commands
{
    /// <summary>
    /// Handler genérico para comandos que no devuelven resultados.
    /// </summary>
    /// <typeparam name="TCommand">Tipo de comando.</typeparam>
    public interface ICommandHandler<in TCommand>
    {
        Task HandleAsync(TCommand command);
    }

    /// <summary>
    /// Handler genérico para comandos que devuelven un resultado.
    /// </summary>
    /// <typeparam name="TCommand">Tipo de comando.</typeparam>
    /// <typeparam name="TResult">Tipo de resultado devuelto.</typeparam>
    public interface ICommandHandler<in TCommand, TResult>
    {
        Task<TResult> HandleAsync(TCommand command);
    }
}