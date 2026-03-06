using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreLib.ControllerExtensions.ControllerTypes;

/// <summary>
/// API для аутентифицированного клиента
/// </summary>
[Authorize]
[Route("[controller]")]
public class ClientController : BaseController
{
    
}