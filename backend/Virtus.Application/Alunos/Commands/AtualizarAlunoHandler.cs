using Virtus.Domain.Repositories;

namespace Virtus.Application.Alunos.Commands;

public class AtualizarAlunoHandler
{
    private readonly IAlunoRepository _repository;

    public AtualizarAlunoHandler(IAlunoRepository repository)
    {
        _repository = repository;
    }

    public async Task HandleAsync(Guid id, AtualizarAlunoCommand command)
    {
        if (!command.EhValido(out var erros))
            throw new ApplicationException(string.Join("; ", erros));

        var aluno = await _repository.ObterPorIdAsync(id);
        if (aluno is null)
            throw new ApplicationException("Aluno não encontrado.");

        aluno.AtualizarDados(command.Nome, command.Email, command.DataNascimento);
        await _repository.AtualizarAsync(aluno);
    }
}
