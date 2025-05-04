using System.Text.RegularExpressions;

namespace InstitutoVirtusApp.Domain.ValueObjects;

public sealed class Email
{
    public string Endereco { get; }

    private Email(string endereco)
    {
        Endereco = endereco;
    }

    public static Email Criar(string endereco)
    {
        if (string.IsNullOrWhiteSpace(endereco))
            throw new ArgumentException("E-mail não pode ser vazio.");

        if (!EhValido(endereco))
            throw new ArgumentException("E-mail inválido.");

        return new Email(endereco);
    }

    public static bool EhValido(string endereco)
    {
        return Regex.IsMatch(endereco, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
    }

    public override string ToString() => Endereco;

    public override bool Equals(object? obj)
    {
        return obj is Email other && Endereco.Equals(other.Endereco, StringComparison.OrdinalIgnoreCase);
    }
    public override int GetHashCode() => Endereco.ToLower().GetHashCode();
}
