using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Lab3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // GET: api/Home
        [HttpGet]
        public IActionResult GetHome()
        {
            return Ok(new { message = "Welcome to the API Home endpoint!" });
        }

        // GET: api/Home/privacy
        [HttpGet("privacy")]
        public IActionResult GetPrivacy()
        {
            return Ok(new { message = "This is the privacy policy of the API." });
        }

        // GET: api/Home/error
        [HttpGet("error")]
        public IActionResult GetError()
        {
            var requestId = HttpContext.TraceIdentifier;
            return StatusCode(500, new
            {
                error = "An unexpected error occurred.",
                requestId = requestId
            });
        }
    }
}
