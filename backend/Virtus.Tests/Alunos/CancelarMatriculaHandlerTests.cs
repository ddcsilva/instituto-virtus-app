using Moq;
using Xunit;
using System;
using System.Threading.Tasks;
using Virtus.Application.Alunos.Commands;
using Virtus.Domain.Entities;
using Virtus.Domain.Enums;
using Virtus.Domain.Repositories;

namespace Virtus.Tests.Alunos;

public class CancelarMatriculaHandlerTests
{
    [Fact]
    public async Task Deve_Cancelar_Matricula_Quando_Aluno_Existir()
    {
        // Arrange
        var aluno = new Aluno("Carlos", "carlos@virtus.org", new DateTime(2000, 5, 10));
        var repoMock = new Mock<IAlunoRepository>();
        repoMock.Setup(r => r.ObterPorIdAsync(aluno.Id)).ReturnsAsync(aluno);

        var handler = new CancelarMatriculaHandler(repoMock.Object);

        // Act
        await handler.HandleAsync(aluno.Id);

        // Assert
        Assert.Equal(StatusAluno.Desistente, aluno.Status);
        repoMock.Verify(r => r.AtualizarAsync(aluno), Times.Once);
    }

    [Fact]
    public async Task Deve_Lancar_Excecao_Quando_Aluno_Nao_Existir()
    {
        // Arrange
        var repoMock = new Mock<IAlunoRepository>();
        repoMock.Setup(r => r.ObterPorIdAsync(It.IsAny<Guid>())).ReturnsAsync((Aluno?)null);

        var handler = new CancelarMatriculaHandler(repoMock.Object);

        // Act & Assert
        await Assert.ThrowsAsync<ApplicationException>(() =>
            handler.HandleAsync(Guid.NewGuid()));
    }
}
