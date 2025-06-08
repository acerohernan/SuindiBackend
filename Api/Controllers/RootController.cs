using Api.DTOs.Requests;
using Api.Shared;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        [HttpGet("/status")]
        public string GetStatus() => "OK";
    }
}
