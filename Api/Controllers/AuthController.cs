using Api.DTOs.Requests;
using Api.Models;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controllers
{
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IValidator<RegisterUserRequest> _registerValidator;
        private readonly IValidator<LoginRequest> _loginValidator;

        public AuthController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IValidator<RegisterUserRequest> registerValidator,
            IValidator<LoginRequest> loginValidator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _registerValidator = registerValidator;
            _loginValidator = loginValidator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
        {
            var validation = _registerValidator.Validate(request);
            if (!validation.IsValid)
            {
                return BadRequest(new
                {
                    Errors = validation.ToDictionary()
                });
            }

            var newUser = new User
            {
                UserName = request.Email,
                Email = request.Email,
                Age = request.Age,
                Nickname = request.Nickname
            };

            var result = await _userManager.CreateAsync(newUser, request.Password);
            if (!result.Succeeded) return BadRequest(result.Errors);

            return Ok(new { message = "User registered successfully." });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var validation = _loginValidator.Validate(request);
            if (!validation.IsValid)
            {
                return BadRequest(validation.ToDictionary());
            }

            var result = await _signInManager.PasswordSignInAsync(
                userName: request.Email,
                password: request.Password,
                isPersistent: true,
                lockoutOnFailure: false);

            if (!result.Succeeded) return BadRequest(new { message = "Email or password is wrong." });
            return Ok(new { message = "User logged in succesfully." });
        }

        [HttpGet("google")]
        public async Task<IActionResult> GoogleSignIn()
        {
            string redirectUrl = Url.Action("GoogleCallback");
            var properties = _signInManager.ConfigureExternalAuthenticationProperties("Google", redirectUrl);
            return new ChallengeResult("Google", properties);
        }

        [HttpGet("signin-google")]
        public async Task<IActionResult> GoogleCallback()
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info is null) return Unauthorized();

            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false);
            
            string[] userInfo = { 
                info.Principal.FindFirst(ClaimTypes.Name).Value, 
                info.Principal.FindFirst(ClaimTypes.Email).Value 
            };

            if (result.Succeeded)
            {
                return Ok(new { message = "User logged in successfully." });
            }

            User user = new User
            {
                Email = info.Principal.FindFirst(ClaimTypes.Email).Value,
                UserName = info.Principal.FindFirst(ClaimTypes.Email).Value
            };

            IdentityResult identResult = await _userManager.CreateAsync(user);
            if (!identResult.Succeeded) return Unauthorized();

            await _signInManager.SignInAsync(user, isPersistent: false);
            // TODO: redireccionar a frontend luego del login
            return Ok(new { message = "User logged in successfully."});
        }
    }
}
