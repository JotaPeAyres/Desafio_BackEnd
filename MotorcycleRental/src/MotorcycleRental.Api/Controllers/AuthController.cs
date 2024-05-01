using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MotorcycleRental.Api.Extensions;
using MotorcycleRental.Api.ViewModels;
using MotorcycleRental.Bussiness.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MotorcycleRental.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[Controller]")]
    public class AuthController : MainController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppSettings _appSettings;
        public AuthController(INotifier notifier,
                            UserManager<IdentityUser> userManager,
                            SignInManager<IdentityUser> signInManager,
                            IOptions<AppSettings> appSettings) : base(notifier)
        {

            _userManager = userManager;
            _signInManager = signInManager;
            _appSettings = appSettings.Value;

        }

        [AllowAnonymous]
        [HttpPost("register-admin")]
        public async Task<ActionResult> RegisterAdmin(RegisterUserViewModel registerUser)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var user = new IdentityUser
            {
                UserName = registerUser.Email,
                Email = registerUser.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, registerUser.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Admin");
                await _signInManager.SignInAsync(user, false);
                return CustomResponse(await GenerateJwt(registerUser.Email));
            }

            foreach (var error in result.Errors)
            {
                NotifyError(error.Description);
            }

            return CustomResponse(registerUser);
        }

        [AllowAnonymous]
        [HttpPost("register-user")]
        public async Task<ActionResult> RegisterUser(RegisterUserViewModel registerUser)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var user = new IdentityUser
            {
                UserName = registerUser.Email,
                Email = registerUser.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, registerUser.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Driver");
                await _signInManager.SignInAsync(user, false);
                return CustomResponse(await GenerateJwt(registerUser.Email));
            }

            foreach (var error in result.Errors)
            {
                NotifyError(error.Description);
            }

            return CustomResponse(registerUser);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginUserViewModel loginUser)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var result = await _signInManager.PasswordSignInAsync(loginUser.Email, loginUser.Password, false, true);

            if (result.Succeeded)
            {
                return CustomResponse(await GenerateJwt(loginUser.Email));
            }
            else if (result.IsLockedOut)
            {
                NotifyError("User temporarily blocked due to invalid attempts");
                return CustomResponse(loginUser);
            }
            else
            {
                NotifyError("Incorrect username or password");
                return CustomResponse(loginUser);
            }
        }

        private async Task<string> GenerateJwt(string userName)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(userName);

                if (user == null)
                    return null;

                var handler = new JwtSecurityTokenHandler();

                var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("9eb4500d-fe84-4448-9536-25b8b1966499" ?? string.Empty));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                DateTime currentTimeUtc = DateTime.UtcNow;

                var expiration = currentTimeUtc.AddHours(8);

                //var claimsUser = await GenerateClaims(user);

                var tokenDescription = new SecurityTokenDescriptor
                {
                    //Subject = claimsUser,
                    SigningCredentials = creds,
                    Expires = DateTime.UtcNow.AddHours(4)
                };

                var token = handler.CreateToken(tokenDescription);

                return handler.WriteToken(token);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}