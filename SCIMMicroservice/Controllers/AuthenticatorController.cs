using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ScimMicroservice.BLL.Interfaces;
using ScimMicroservice.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ScimMicroservice.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AuthenticatorController : Controller
    {
        private IConfiguration configuration;
        private IUserService userService;

        public AuthenticatorController(
            IConfiguration configuration,
            IUserService userService)
        {
            this.configuration = configuration;
            this.userService = userService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("token")]
        public async Task<IActionResult> Post([FromBody]ScimLogin loginModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var authenticated = await userService.AuthenticateUser(loginModel);

                    if (!authenticated)
                    {
                        return Unauthorized();
                    }

                    var claims = new[]
                    {
                    new Claim(JwtRegisteredClaimNames.Sub, loginModel.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                    var token = new JwtSecurityToken
                    (

                        issuer: configuration.GetValue<string>("Issuer"),
                        audience: configuration.GetValue<string>("Audience"),
                        claims: claims,
                        expires: DateTime.UtcNow.AddDays(60),
                        notBefore: DateTime.UtcNow,
                        signingCredentials: new SigningCredentials(
                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("SigningKey"))),
                                SecurityAlgorithms.HmacSha256)
                    );

                    return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return BadRequest();
        }
    }
}
