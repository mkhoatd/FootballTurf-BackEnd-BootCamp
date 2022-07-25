using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Interfaces;

namespace WebApi.Controllers
{
    public class TurfsController : BaseApiController
    {
        [HttpGet()]
        public async Task<ActionResult<string>> GetMainTurfList()
        {
            return "no";
        }
    }
}
