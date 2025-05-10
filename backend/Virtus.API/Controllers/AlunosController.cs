using Microsoft.AspNetCore.Mvc;
using Virtus.Application.Alunos.Commands;
using Virtus.Application.Alunos.DTOs;
using Virtus.Domain.Repositories;

namespace Virtus.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AlunosController : ControllerBase
{
    private readonly IAlunoRepository _repository;
    private readonly CriarAlunoHandler _handler;

    public AlunosController(CriarAlunoHandler handler, IAlunoRepository repository)
    {
        _handler = handler;
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> ListarAlunosAsync()
    {
        var alunos = await _repository.ListarTodosAsync();

        var resultado = alunos.Select(a => new AlunoDTO
        {
            Id = a.Id,
            Nome = a.Nome!,
            Email = a.Email!
        });

        return Ok(resultado);
    }

    [HttpGet("{id}", Name = "ObterAlunoPorId")]
    public async Task<IActionResult> ObterAlunoPorIdAsync(Guid id)
    {
        var aluno = await _repository.ObterPorIdAsync(id);

        if (aluno == null) return NotFound();

        return Ok(new AlunoDTO
        {
            Id = aluno.Id,
            Nome = aluno.Nome!,
            Email = aluno.Email!
        });
    }

    [HttpPost]
    public async Task<IActionResult> CriarAlunoAsync([FromBody] CriarAlunoCommand command)
    {
        try
        {
            var id = await _handler.HandleAsync(command);

            return CreatedAtRoute("ObterAlunoPorId", new { id }, new { id });
        }
        catch (ApplicationException ex)
        {
            return BadRequest(new { erros = ex.Message.Split("; ") });
        }
    }
}
