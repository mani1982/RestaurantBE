using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FiounaRestaurantBE.Model;
using FiounaRestaurantBE.Model.IdentityModel;
using FiounaRestaurantBE.Model.IdentityModel.ViewModels;
using IdentityServer4.Events;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace FiounaRestaurantBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly Microsoft.Extensions.Configuration.IConfiguration _config;


        public AccountsController(SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager,
            FiounaRestaurantDbContext appIdentityDbContext,
            RoleManager<IdentityRole> roleManager,
            Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _config = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SignupRequest model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new AppUser { UserName = model.Email, FullName = model.FullName, Email = model.Email };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded) return BadRequest(result.Errors);

            await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("userName", user.UserName));
            await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("name", user.FullName));
            await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("email", user.Email));
            await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("role", model.Role));

            try
            {
                if (!await _roleManager.RoleExistsAsync(model.Role))
                {
                    var newRoleResult = await _roleManager.CreateAsync(new IdentityRole { Name = model.Role });
                    if (!newRoleResult.Succeeded)
                    {
                        throw new Exception("Cannot insert role");
                    }
                }

                var AddToRoleAsyncResult = await _userManager.AddToRoleAsync(user, model.Role);

                if (!AddToRoleAsyncResult.Succeeded)
                {
                    throw new Exception("Cannot add role to user");

                }

            }
            catch (Exception e)
            {
                throw new Exception(e?.ToString(), e?.InnerException);
            }
            return Ok(new SignupResponse(user, model.Role));
        }

        /// <summary>
        /// Handle postback from username/password login
        /// </summary>
        [HttpPost("Login")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromBody] LoginInputModel model)
        {
            // check if we are in the context of an authorization request
            //var context = await _interaction.GetAuthorizationContextAsync(model.ReturnUrl);
            var vm = new LoginViewModel();
            if (ModelState.IsValid)
            {
                // validate username/password
                var user = await _userManager.FindByNameAsync(model.Username);

                if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    //await _events.RaiseAsync(new UserLoginSuccessEvent(user.UserName, user.Id, user.FullName));

                    // only set explicit expiration here if user chooses "remember me". 
                    // otherwise we rely upon expiration configured in cookie middleware.
                    AuthenticationProperties props = null;
                    if (AccountOptions.AllowRememberLogin && model.RememberLogin)
                    {
                        props = new AuthenticationProperties
                        {
                            IsPersistent = true,
                            ExpiresUtc = DateTimeOffset.UtcNow.Add(AccountOptions.RememberMeLoginDuration)
                        };
                    };

                    var userRole = await _userManager.GetRolesAsync(user);

                    var jwt = BuildToken(userRole.FirstOrDefault());

                    // something went wrong, show form with error 
                    vm = new LoginViewModel
                    {
                        Username = model.Username,
                        RememberLogin = model.RememberLogin,
                        Tocken = jwt
                    };
                }

                ModelState.AddModelError(string.Empty, AccountOptions.InvalidCredentialsErrorMessage);
            }
            else
            {
                return BadRequest(ModelState);
            }

            return Ok(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Logout(string logoutId)
        {
            await _signInManager.SignOutAsync();
            //var context = await _interaction.GetLogoutContextAsync(logoutId);
            //return Redirect(context.PostLogoutRedirectUri);

            return Ok();
        }

        private static string GetUserName(string returnUrl)
        {
            const string parameter = "&userName=";
            return returnUrl.Contains("userName") ? returnUrl.Substring(returnUrl.IndexOf("&userName=") + parameter.Length) : null;
        }


        private string BuildToken(string role)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>();

            if (!string.IsNullOrEmpty(role))
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
                //foreach (var role in roles)
                //{
                //    claims.Add(new Claim(ClaimTypes.Role, role));
                //}
            }

            var token = new JwtSecurityToken(_config["JWT:Issuer"],
              _config["JWT:Audience"],
              
              expires: DateTime.Now.AddDays(1),
              signingCredentials: creds,
              claims: claims
              );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
