using Microsoft.EntityFrameworkCore;
using Virtus.Domain.Entities;
using Virtus.Domain.Repositories;
using Virtus.Infra.Data;

namespace Virtus.Infra.Repositories;

public class AlunoRepository : IAlunoRepository
{
    private readonly VirtusDbContext _context;

    public AlunoRepository(VirtusDbContext context)
    {
        _context = context;
    }

    public async Task<Aluno?> ObterPorIdAsync(Guid id)
    {
        return await _context.Alunos.FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<IEnumerable<Aluno>> ListarTodosAsync()
    {
        return await _context.Alunos.ToListAsync();
    }

    public async Task AdicionarAsync(Aluno aluno)
    {
        await _context.Alunos.AddAsync(aluno);
        await _context.SaveChangesAsync();
    }

    public async Task AtualizarAsync(Aluno aluno)
    {
        _context.Alunos.Update(aluno);
        await _context.SaveChangesAsync();
    }

    public async Task RemoverAsync(Aluno aluno)
    {
        _context.Alunos.Remove(aluno);
        await _context.SaveChangesAsync();
    }
}
