using System.Text.RegularExpressions;
using ModuloClientes.Core.Models.ValueObjects.ClienteValueObjects;

namespace ModuloClientes.Core.Models.ValueObjects.SeguroSaludValueObjects
{
    /// <summary>
    /// Nombre del plan de salud. Permite letras, dígitos, espacios y (. - / ').
    /// </summary>
    public sealed class PlanName : IEquatable<PlanName>, IComparable<PlanName>
    {
        private const int MinLength = 2;
        private const int MaxLength = 100;
        private static readonly Regex PlanNameRegex = new(
            @"^[\p{L}\p{N}\s\.\-/']+$",
            RegexOptions.Compiled | RegexOptions.CultureInvariant,
            TimeSpan.FromMilliseconds(250)
        );

        public string Value { get; }

        public PlanName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("El nombre del plan es obligatorio.", nameof(value));

            var trimmed = value.Trim();
            if (trimmed.Length < MinLength)
                throw new ArgumentException($"El nombre del plan debe tener al menos {MinLength} caracteres.", nameof(value));
            if (trimmed.Length > MaxLength)
                throw new ArgumentException($"El nombre del plan no puede exceder {MaxLength} caracteres.", nameof(value));
            if (!PlanNameRegex.IsMatch(trimmed))
                throw new ArgumentException("El nombre del plan contiene caracteres inválidos.", nameof(value));

            Value = trimmed;
        }

        public override string ToString() => Value;
        public override bool Equals(object obj) => obj is PlanName other && Equals(other);
        public bool Equals(PlanName other) => other != null &&
            string.Equals(Value, other.Value, StringComparison.OrdinalIgnoreCase);
        public override int GetHashCode() => StringComparer.OrdinalIgnoreCase.GetHashCode(Value);
        public int CompareTo(PlanName other) =>
            string.Compare(Value, other?.Value, StringComparison.OrdinalIgnoreCase);

        public static bool operator ==(PlanName a, PlanName b) => a?.Equals(b) ?? b is null;
        public static bool operator !=(PlanName a, PlanName b) => !(a == b);

        public static Result<PlanName> TryCreate(string value)
        {
            try
            {
                return Result<PlanName>.Success(new PlanName(value));
            }
            catch (ArgumentException ex)
            {
                return Result<PlanName>.Failure(ex.Message);
            }
        }
    }

    /// <summary>
    /// Número de póliza. Permite letras, dígitos y guiones.
    /// </summary>
    public sealed class PolicyNumber : IEquatable<PolicyNumber>, IComparable<PolicyNumber>
    {
        private const int MinLength = 5;
        private const int MaxLength = 50;
        private static readonly Regex PolicyNumberRegex = new(
            @"^[A-Za-z0-9\-]+$",
            RegexOptions.Compiled | RegexOptions.CultureInvariant,
            TimeSpan.FromMilliseconds(250)
        );

        public string Value { get; }

        public PolicyNumber(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("El número de póliza es obligatorio.", nameof(value));

            var trimmed = value.Trim();
            if (trimmed.Length < MinLength)
                throw new ArgumentException($"El número de póliza debe tener al menos {MinLength} caracteres.", nameof(value));
            if (trimmed.Length > MaxLength)
                throw new ArgumentException($"El número de póliza no puede exceder {MaxLength} caracteres.", nameof(value));
            if (!PolicyNumberRegex.IsMatch(trimmed))
                throw new ArgumentException("El número de póliza contiene caracteres inválidos.", nameof(value));

            Value = trimmed;
        }

        public override string ToString() => Value;
        public override bool Equals(object obj) => obj is PolicyNumber other && Equals(other);
        public bool Equals(PolicyNumber other) => other != null &&
            string.Equals(Value, other.Value, StringComparison.Ordinal);
        public override int GetHashCode() => Value.GetHashCode();
        public int CompareTo(PolicyNumber other) => string.Compare(Value, other?.Value, StringComparison.Ordinal);

        public static bool operator ==(PolicyNumber a, PolicyNumber b) => a?.Equals(b) ?? b is null;
        public static bool operator !=(PolicyNumber a, PolicyNumber b) => !(a == b);

        public static Result<PolicyNumber> TryCreate(string value)
        {
            try
            {
                return Result<PolicyNumber>.Success(new PolicyNumber(value));
            }
            catch (ArgumentException ex)
            {
                return Result<PolicyNumber>.Failure(ex.Message);
            }
        }
    }
}