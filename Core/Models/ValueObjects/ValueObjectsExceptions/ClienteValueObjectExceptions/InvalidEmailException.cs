namespace ModuloClientes.Core.Models.ValueObjects.ValueObjectsExceptions.ClienteValueObjectsExceptions
{
    public sealed class InvalidEmailException : ArgumentException
    {
        public InvalidEmailException(){ }
        public InvalidEmailException(string message)                  : base(message)               { }
        public InvalidEmailException(string message, string paramName) : base(message, paramName)   { }
        public InvalidEmailException(string message, Exception inner)  : base(message, inner) { }
    }
}