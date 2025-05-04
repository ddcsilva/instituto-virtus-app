using InstitutoVirtusApp.Domain.Enums;
using InstitutoVirtusApp.Domain.ValueObjects;

namespace InstitutoVirtusApp.Domain.Entities;

public class Usuario
{
    public Guid Id { get; private set; }
    public required string Nome { get; set; }
    public required Email Email { get; set; }
    public required string SenhaHash { get; set; }
    public required PerfilUsuario Perfil { get; set; }

    protected Usuario() { }

    public Usuario(string nome, Email email, string senhaHash, PerfilUsuario perfil)
    {
        Id = Guid.NewGuid();
        Nome = nome;
        Email = email;
        SenhaHash = senhaHash;
        Perfil = perfil;
    }

    public void AlterarNome(string novoNome)
    {
        if (string.IsNullOrWhiteSpace(novoNome))
            throw new ArgumentException("Nome não pode ser vazio.");

        Nome = novoNome;
    }
}
