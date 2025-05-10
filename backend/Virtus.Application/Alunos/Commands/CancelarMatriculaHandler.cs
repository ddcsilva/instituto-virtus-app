using Virtus.Domain.Repositories;

namespace Virtus.Application.Alunos.Commands;

public class CancelarMatriculaHandler
{
    private readonly IAlunoRepository _repository;

    public CancelarMatriculaHandler(IAlunoRepository repository)
    {
        _repository = repository;
    }

    public async Task HandleAsync(Guid id)
    {
        var aluno = await _repository.ObterPorIdAsync(id);

        if (aluno is null)
            throw new ApplicationException("Aluno não encontrado.");

        aluno.Desistir();
        await _repository.AtualizarAsync(aluno);
    }
}
