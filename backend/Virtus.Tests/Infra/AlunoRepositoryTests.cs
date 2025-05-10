using Microsoft.EntityFrameworkCore;
using Virtus.Domain.Entities;
using Virtus.Infra.Data;
using Virtus.Infra.Repositories;

namespace Virtus.Tests.Infra;

public class AlunoRepositoryTests
{
    private VirtusDbContext CriarDbContext()
    {
        var options = new DbContextOptionsBuilder<VirtusDbContext>()
            .UseSqlite("Filename=:memory:")
            .Options;

        var context = new VirtusDbContext(options);
        context.Database.OpenConnection();
        context.Database.EnsureCreated();

        return context;
    }

    [Fact]
    public async Task Deve_adicionar_e_buscar_aluno_por_id()
    {
        using var context = CriarDbContext();
        var repository = new AlunoRepository(context);

        var aluno = new Aluno("Maria", "maria@virtus.org", new DateTime(1999, 10, 1));
        await repository.AdicionarAsync(aluno);

        var alunoEncontrado = await repository.ObterPorIdAsync(aluno.Id);

        Assert.NotNull(alunoEncontrado);
        Assert.Equal("Maria", alunoEncontrado!.Nome);
    }

    [Fact]
    public async Task Deve_atualizar_dados_do_aluno()
    {
        using var context = CriarDbContext();
        var repository = new AlunoRepository(context);

        var aluno = new Aluno("José", "jose@virtus.org", new DateTime(1985, 5, 10));
        await repository.AdicionarAsync(aluno);

        aluno.AtualizarDados("José Atualizado", "jose@virtus.org", aluno.DataNascimento);
        await repository.AtualizarAsync(aluno);

        var atualizado = await repository.ObterPorIdAsync(aluno.Id);
        Assert.Equal("José Atualizado", atualizado!.Nome);
    }

    [Fact]
    public async Task Deve_remover_aluno()
    {
        using var context = CriarDbContext();
        var repository = new AlunoRepository(context);

        var aluno = new Aluno("Ana", "ana@virtus.org", new DateTime(2002, 3, 14));
        await repository.AdicionarAsync(aluno);

        await repository.RemoverAsync(aluno);
        var resultado = await repository.ObterPorIdAsync(aluno.Id);

        Assert.Null(resultado);
    }
}
