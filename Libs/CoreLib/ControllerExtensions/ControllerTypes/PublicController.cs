using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreLib.ControllerExtensions.ControllerTypes;

/// <summary>
/// Открытые методы API
/// </summary>
[AllowAnonymous]
[Route("[controller]")]
public abstract class PublicController : BaseController
{
    
}