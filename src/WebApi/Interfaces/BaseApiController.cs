using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
 
namespace WebApi.Interfaces;

[Authorize]
[Route("Api/[controller]/[action]")]
[ApiController]
public abstract class BaseApiController : ControllerBase
{
    
}