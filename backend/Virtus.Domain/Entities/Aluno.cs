using Virtus.Domain.Enums;

namespace Virtus.Domain.Entities;

public class Aluno
{
    public Guid Id { get; private set; }
    public string? Nome { get; private set; }
    public string? Email { get; private set; }
    public DateTime DataNascimento { get; private set; }
    public StatusAluno Status { get; private set; }

    public Aluno(string nome, string email, DateTime dataNascimento)
    {
        Id = Guid.NewGuid();
        Nome = nome ?? throw new ArgumentNullException(nameof(nome));
        Email = email ?? throw new ArgumentNullException(nameof(email));
        DataNascimento = dataNascimento;
        Status = StatusAluno.Ativo;
    }

    public void AtualizarDados(string nome, string email, DateTime dataNascimento)
    {
        Nome = nome;
        Email = email;
        DataNascimento = dataNascimento;
    }

    public void Desistir() => Status = StatusAluno.Desistente;

    public void InscreverEmListaEspera() => Status = StatusAluno.ListaEspera;

    public void Reativar() => Status = StatusAluno.Ativo;
}