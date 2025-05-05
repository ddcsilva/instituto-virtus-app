using System.Diagnostics;
using InstitutoVirtusApp.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InstitutoVirtusApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TesteController : ControllerBase
{
    private readonly IUserContext _userContext;

    public TesteController(IUserContext userContext)
    {
        _userContext = userContext;
    }

    [HttpGet("usuario")]
    public IActionResult ObterUsuario()
    {
        Debug.WriteLine($"Id: {_userContext.UserId}");
        Debug.WriteLine($"Email: {_userContext.Email}");
        Debug.WriteLine($"Nome: {_userContext.Name}");
        return Ok(new
        {
            Id = _userContext.UserId,
            Email = _userContext.Email,
            Name = _userContext.Name ?? "Nome não cadastrado"
        });
    }
}
