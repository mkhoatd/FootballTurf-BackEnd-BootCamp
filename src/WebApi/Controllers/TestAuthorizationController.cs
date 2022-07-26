using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Domain.Enum;
using WebApi.Interfaces;

namespace WebApi.Controllers;

public class TestAuthorizationController : BaseApiController
{
    [HttpGet()]
    [Authorize(Roles = nameof(UserRole.ProductOwner))]
    public ActionResult<string> CheckIfYouAreProductOwner()
    {
        return "Yes";
    }
}