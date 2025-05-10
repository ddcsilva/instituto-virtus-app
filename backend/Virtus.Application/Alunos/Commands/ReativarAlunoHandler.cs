// Virtus.Application/Alunos/Commands/ReativarAlunoHandler.cs
using Virtus.Domain.Repositories;

namespace Virtus.Application.Alunos.Commands;

public class ReativarAlunoHandler
{
    private readonly IAlunoRepository _repository;

    public ReativarAlunoHandler(IAlunoRepository repository)
    {
        _repository = repository;
    }

    public async Task HandleAsync(Guid id)
    {
        var aluno = await _repository.ObterPorIdAsync(id);

        if (aluno is null)
            throw new ApplicationException("Aluno não encontrado.");

        aluno.Reativar();
        await _repository.AtualizarAsync(aluno);
    }
}
