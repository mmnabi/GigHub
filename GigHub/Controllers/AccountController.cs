using GigHub.Configuration;
using GigHub.Core.Models;
using GigHub.Core.Resources;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GigHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseApiController
    {
        private readonly AuthSettings _authSettings;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public AccountController(
            IOptionsMonitor<AuthSettings> optionsMonitor,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager
            )
        {
            _authSettings = optionsMonitor.CurrentValue;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterResource model)
        {
            if (!ModelState.IsValid)
                return BadModelStateResponse();

            var user = new ApplicationUser()
            {
                Email = model.Email,
                UserName = model.Email,
                Name = model.Name
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                return GetErrorResult(result);

            var jwtToken = GenerateJwtToken(user);
            return OkResponse(new AuthResultResource { Token = jwtToken });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginResource model)
        {
            if (!ModelState.IsValid)
                return BadModelStateResponse();

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: false, lockoutOnFailure: false);
            var errors = new List<string>();
            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                string jwtToken = GenerateJwtToken(user);
                return OkResponse(new AuthResultResource { Token = jwtToken });
            }
            else if (result.IsLockedOut)
                errors.Add($"The user with email {model.Email} is locked out.");
            else if (result.IsNotAllowed)
                errors.Add($"The user with email {model.Email} is not allowed.");
            else if (result.RequiresTwoFactor)
                errors.Add($"The user with email {model.Email} requires Two Factor.");
            else
                errors.Add("Invalid login attempt.");
            return BadResponseWithErrors(errors);
        }

        private string GenerateJwtToken(ApplicationUser user)
        {
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_authSettings.Key));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim("Id", user.Id),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(24),
                Audience = _authSettings.Audience,
                Issuer = _authSettings.Issuer,
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            return jwtToken;
        }

        private IActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);

            if (result.Errors != null)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("RegisterError", error.Description);
                }
            }

            if (ModelState.IsValid)
                // No ModelState errors are available to send, so just return an empty BadRequest.
                return BadRequest();

            return BadModelStateResponse();
        }
    }
}
