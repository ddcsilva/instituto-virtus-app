using Microsoft.AspNetCore.Mvc;
using Virtus.Application.Alunos.Commands;

namespace Virtus.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AlunosController : ControllerBase
{
    private readonly CriarAlunoHandler _handler;

    public AlunosController(CriarAlunoHandler handler)
    {
        _handler = handler;
    }

    [HttpPost]
    public async Task<IActionResult> CriarAlunoAsync([FromBody] CriarAlunoCommand command)
    {
        try
        {
            var id = await _handler.HandleAsync(command);
            return CreatedAtAction(nameof(ObterAlunoAsync), new { id }, null);
        }
        catch (ApplicationException ex)
        {
            return BadRequest(new { erros = ex.Message.Split("; ") });
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ObterAlunoAsync(Guid id)
    {
        return Ok();
    }
}