using Microsoft.AspNetCore.Mvc;

namespace CoreLib.ControllerExtensions.ControllerTypes;

/// <summary>
/// API для межсервисного взаимодействия
/// </summary>
[Route("[controller]")]
public abstract class InternalController : BaseController
{
    
}