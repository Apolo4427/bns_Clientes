namespace ModuloClientes.Core.Models.ValueObjects.ValueObjectsExceptions.ClienteValueObjectsExceptions
{
    public class InvalidSSNException : ArgumentException
    {
        public InvalidSSNException() { }
        public InvalidSSNException(string message)                  : base(message)               { }
        public InvalidSSNException(string message, string paramName) : base(message, paramName)   { }
        public InvalidSSNException(string message, Exception inner)  : base(message, inner)       { }
    }
}