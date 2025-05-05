using InstitutoVirtusApp.API.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InstitutoVirtusApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UsuariosController : ControllerBase
{
    [HttpGet]
    public IActionResult ObterUsuarioAtual()
    {
        var uid = User.GetUid();
        var email = User.GetEmail();
        var name = User.GetName();

        return Ok(new
        {
            Uid = uid,
            Email = email,
            Name = name
        });
    }
}
