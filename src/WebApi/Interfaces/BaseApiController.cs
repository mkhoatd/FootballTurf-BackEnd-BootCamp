using Microsoft.AspNetCore.Mvc;
 
namespace WebApi.Interfaces;

[Route("Api/[controller]/[action]")]
[ApiController]
public abstract class BaseApiController : ControllerBase
{
    
}