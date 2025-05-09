using Virtus.Domain.Entities;

namespace Virtus.Domain.Repositories;

public interface IAlunoRepository
{
    Task<Aluno?> ObterPorIdAsync(Guid id);
    Task<IEnumerable<Aluno>> ListarTodosAsync();
    Task AdicionarAsync(Aluno aluno);
    Task AtualizarAsync(Aluno aluno);
    Task RemoverAsync(Aluno aluno);
}
