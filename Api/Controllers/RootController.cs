using Api.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    public class RootController : ControllerBase
    {
        private readonly ILogger<RootController> _logger;
        private readonly IConfigurationHelper _configuration;

        public RootController(ILogger<RootController> logger, IConfigurationHelper configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet("/status")]
        public string GetStatus() => "OK";
    }
}
