using System.Text.RegularExpressions;
using ModuloClientes.Core.Models.ValueObjects.ClienteValueObjects;

namespace ModuloClientes.Core.Models.ValueObjects.EmpresaValueObjects
{
    /// <summary>
    /// Nombre de la empresa. Permite letras, dígitos, espacios y (& . , - ').
    /// </summary>
    public sealed class CompanyName : IEquatable<CompanyName>, IComparable<CompanyName>
    {
        private const int MinLength = 2;
        private const int MaxLength = 200;
        private static readonly Regex CompanyNameRegex = new(
            @"^[\p{L}\p{N}\s&\.\,\-']+$",
            RegexOptions.Compiled | RegexOptions.CultureInvariant,
            TimeSpan.FromMilliseconds(250)
        );

        public string Value { get; }

        public CompanyName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("El nombre de la empresa no puede estar vacío.", nameof(value));

            var trimmed = value.Trim();
            if (trimmed.Length < MinLength)
                throw new ArgumentException($"El nombre debe tener al menos {MinLength} caracteres.", nameof(value));
            if (trimmed.Length > MaxLength)
                throw new ArgumentException($"El nombre no puede exceder {MaxLength} caracteres.", nameof(value));
            if (!CompanyNameRegex.IsMatch(trimmed))
                throw new ArgumentException("El nombre contiene caracteres inválidos.", nameof(value));

            Value = trimmed;
        }

        public override string ToString() => Value;
        public override bool Equals(object obj) => obj is CompanyName other && Equals(other);
        public bool Equals(CompanyName other) => other != null &&
            string.Equals(Value, other.Value, StringComparison.OrdinalIgnoreCase);
        public override int GetHashCode() => StringComparer.OrdinalIgnoreCase.GetHashCode(Value);
        public int CompareTo(CompanyName other) =>
            string.Compare(Value, other?.Value, StringComparison.OrdinalIgnoreCase);

        public static bool operator ==(CompanyName a, CompanyName b) => a?.Equals(b) ?? b is null;
        public static bool operator !=(CompanyName a, CompanyName b) => !(a == b);

        public static Result<CompanyName> TryCreate(string value)
        {
            try
            {
                return Result<CompanyName>.Success(new CompanyName(value));
            }
            catch (ArgumentException ex)
            {
                return Result<CompanyName>.Failure(ex.Message);
            }
        }
    }

    /// <summary>
    /// Employer Identification Number (EIN). Formatos: "XX-XXXXXXX" o "XXXXXXXXX".
    /// </summary>
    public sealed class EIN : IEquatable<EIN>, IComparable<EIN>
    {
        private const int RequiredDigits = 9;
        private static readonly Regex[] Patterns = new[]
        {
            new Regex(@"^\d{2}-\d{7}$", RegexOptions.Compiled | RegexOptions.CultureInvariant, TimeSpan.FromMilliseconds(250)),
            new Regex(@"^\d{9}$",     RegexOptions.Compiled | RegexOptions.CultureInvariant, TimeSpan.FromMilliseconds(250))
        };

        public string Value { get; }

        public EIN(string ein)
        {
            if (string.IsNullOrWhiteSpace(ein))
                throw new ArgumentException("El EIN no puede estar vacío.", nameof(ein));

            var digits = new string(ein.Where(char.IsDigit).ToArray());
            if (digits.Length != RequiredDigits)
                throw new ArgumentException($"El EIN debe tener {RequiredDigits} dígitos.", nameof(ein));

            if (!Patterns.Any(p => p.IsMatch(ein)))
                throw new ArgumentException("Formato de EIN inválido. Debe ser XX-XXXXXXX o XXXXXXXXX.", nameof(ein));

            Value = digits;
        }

        /// <summary>
        /// Devuelve el EIN formateado como "XX-XXXXXXX".
        /// </summary>
        public string Formatted => $"{Value.Substring(0, 2)}-{Value.Substring(2)}";

        public override string ToString() => Formatted;
        public override bool Equals(object obj) => obj is EIN other && Equals(other);
        public bool Equals(EIN other) => other != null && Value == other.Value;
        public override int GetHashCode() => Value.GetHashCode();
        public int CompareTo(EIN other) => string.Compare(Value, other?.Value, StringComparison.Ordinal);

        public static bool operator ==(EIN a, EIN b) => a?.Equals(b) ?? b is null;
        public static bool operator !=(EIN a, EIN b) => !(a == b);

        public static Result<EIN> TryCreate(string value)
        {
            try
            {
                return Result<EIN>.Success(new EIN(value));
            }
            catch (ArgumentException ex)
            {
                return Result<EIN>.Failure(ex.Message);
            }
        }
    }
}