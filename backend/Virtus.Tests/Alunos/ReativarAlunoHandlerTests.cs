using Moq;
using Virtus.Application.Alunos.Commands;
using Virtus.Domain.Entities;
using Virtus.Domain.Enums;
using Virtus.Domain.Repositories;

namespace Virtus.Tests.Alunos;

public class ReativarAlunoHandlerTests
{
    [Fact]
    public async Task Deve_Reativar_Aluno_Com_Status_Correto()
    {
        // Arrange
        var aluno = new Aluno("Paula", "paula@virtus.org", new DateTime(1995, 4, 20));
        aluno.Desistir();

        var repoMock = new Mock<IAlunoRepository>();
        repoMock.Setup(r => r.ObterPorIdAsync(aluno.Id)).ReturnsAsync(aluno);

        var handler = new ReativarAlunoHandler(repoMock.Object);

        // Act
        await handler.HandleAsync(aluno.Id);

        // Assert
        Assert.Equal(StatusAluno.Ativo, aluno.Status);
        repoMock.Verify(r => r.AtualizarAsync(aluno), Times.Once);
    }

    [Fact]
    public async Task Deve_Lancar_Excecao_Se_Aluno_Nao_Existir()
    {
        var repoMock = new Mock<IAlunoRepository>();
        repoMock.Setup(r => r.ObterPorIdAsync(It.IsAny<Guid>())).ReturnsAsync((Aluno?)null);

        var handler = new ReativarAlunoHandler(repoMock.Object);

        await Assert.ThrowsAsync<ApplicationException>(() =>
            handler.HandleAsync(Guid.NewGuid()));
    }
}
