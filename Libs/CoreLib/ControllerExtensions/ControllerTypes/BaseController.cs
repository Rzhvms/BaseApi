using Microsoft.AspNetCore.Mvc;

namespace CoreLib.ControllerExtensions.ControllerTypes;

/// <summary>
/// Базовый класс контроллера
/// </summary>
[ApiController]
[Route("api")]
public abstract class BaseController : ControllerBase
{
    
}