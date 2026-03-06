using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Authorization;

[ApiController]
[Route("api/authorization")]
public class AuthorizationController : ControllerBase
{
    public AuthorizationController()
    {
        
    }

    [AllowAnonymous]
    [HttpPost("token")]
    public async Task AuthorizeUserAsync()
    {
        await Task.CompletedTask;
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task UnAuthorizeUserAsync()
    {
        await Task.CompletedTask;
    }
    
    public async Task RefreshTokenAsync()
    {
        await Task.CompletedTask;
    }
    
    public async Task ChangePasswordAsync()
    {
        await Task.CompletedTask;
    }
    
    public async Task RestorePasswordAsync()
    {
        await Task.CompletedTask;
    }
    
    public async Task ResetPasswordAsync()
    {
        await Task.CompletedTask;
    }
}