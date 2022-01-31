using Microsoft.AspNetCore.Mvc;

namespace Octopus.Core.Parser.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Echo(string message)
        {
            return Ok($"Hello, {message}");
        }
    }
}
