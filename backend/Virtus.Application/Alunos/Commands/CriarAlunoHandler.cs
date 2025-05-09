using Virtus.Domain.Entities;
using Virtus.Domain.Repositories;

namespace Virtus.Application.Alunos.Commands;

public class CriarAlunoHandler
{
    private readonly IAlunoRepository _alunoRepository;

    public CriarAlunoHandler(IAlunoRepository alunoRepository)
    {
        _alunoRepository = alunoRepository;
    }

    public async Task<Guid> HandleAsync(CriarAlunoCommand command)
    {
        if (!command.EhValido(out var erros))
            throw new ApplicationException(string.Join("; ", erros));

        var aluno = new Aluno(command.Nome, command.Email, command.DataNascimento);
        await _alunoRepository.AdicionarAsync(aluno);
        return aluno.Id;
    }
}
