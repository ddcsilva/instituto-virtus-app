using Moq;
using Virtus.Application.Alunos.Commands;
using Virtus.Domain.Entities;
using Virtus.Domain.Repositories;

namespace Virtus.Tests.Alunos;

public class CriarAlunoHandlerTests
{
    [Fact]
    public async Task Deve_Criar_Aluno_Quando_Comando_Eh_Valido()
    {
        // Arrange
        var alunoRepositoryMock = new Mock<IAlunoRepository>();
        var command = new CriarAlunoCommand
        {
            Nome = "Danilo Silva",
            Email = "danilo.silva@teste.com",
            DataNascimento = new DateTime(1989, 11, 30)
        };

        var handler = new CriarAlunoHandler(alunoRepositoryMock.Object);

        // Act
        var id = await handler.HandleAsync(command);

        // Assert
        Assert.NotEqual(Guid.Empty, id);

        alunoRepositoryMock.Verify(r => r.AdicionarAsync(It.Is<Aluno>(a =>
            a.Nome == command.Nome &&
            a.Email == command.Email &&
            a.DataNascimento == command.DataNascimento
        )), Times.Once);
    }

    [Fact]
    public async Task Deve_Lancar_Excecao_Quando_Comando_Eh_Invalido()
    {
        // Arrange
        var alunoRepositoryMock = new Mock<IAlunoRepository>();
        var command = new CriarAlunoCommand();

        var handler = new CriarAlunoHandler(alunoRepositoryMock.Object);

        // Act & Assert
        await Assert.ThrowsAsync<ApplicationException>(() => handler.HandleAsync(command));
        alunoRepositoryMock.Verify(r => r.AdicionarAsync(It.IsAny<Aluno>()), Times.Never);
    }
}
