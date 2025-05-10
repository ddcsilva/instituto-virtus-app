using Moq;
using Virtus.Application.Alunos.Commands;
using Virtus.Domain.Entities;
using Virtus.Domain.Repositories;

namespace Virtus.Tests.Alunos;

public class AtualizarAlunoHandlerTests
{
    [Fact]
    public async Task Deve_Atualizar_Aluno_Quando_Dados_Sao_Validos()
    {
        // Arrange
        var aluno = new Aluno("João", "joao@virtus.org", new DateTime(1990, 1, 1));
        var repositoryMock = new Mock<IAlunoRepository>();
        repositoryMock.Setup(r => r.ObterPorIdAsync(aluno.Id)).ReturnsAsync(aluno);

        var handler = new AtualizarAlunoHandler(repositoryMock.Object);
        var command = new AtualizarAlunoCommand
        {
            Nome = "João Atualizado",
            Email = "joao@virtus.org",
            DataNascimento = aluno.DataNascimento
        };

        // Act
        await handler.HandleAsync(aluno.Id, command);

        // Assert
        repositoryMock.Verify(r => r.AtualizarAsync(It.Is<Aluno>(a => a.Nome == "João Atualizado")), Times.Once);
    }

    [Fact]
    public async Task Deve_Lancar_Excecao_Se_Aluno_Nao_Existir()
    {
        // Arrange
        var repositoryMock = new Mock<IAlunoRepository>();
        repositoryMock.Setup(r => r.ObterPorIdAsync(It.IsAny<Guid>())).ReturnsAsync((Aluno?)null);

        // Act
        var handler = new AtualizarAlunoHandler(repositoryMock.Object);
        var command = new AtualizarAlunoCommand { Nome = "A", Email = "b@c.com", DataNascimento = DateTime.Today };

        // Assert
        await Assert.ThrowsAsync<ApplicationException>(() =>
            handler.HandleAsync(Guid.NewGuid(), command));
    }

    [Fact]
    public async Task Deve_Lancar_Excecao_Se_Comando_For_Invalido()
    {
        // Arrange
        var repositoryMock = new Mock<IAlunoRepository>();
        var handler = new AtualizarAlunoHandler(repositoryMock.Object);
        var command = new AtualizarAlunoCommand();

        // Act & Assert
        await Assert.ThrowsAsync<ApplicationException>(() =>
            handler.HandleAsync(Guid.NewGuid(), command));
    }
}
