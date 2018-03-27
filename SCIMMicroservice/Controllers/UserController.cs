using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ScimMicroservice.BLL.Interfaces;
using ScimMicroservice.Models;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ScimMicroservice.Controllers
{
    //[ApiVersion("0.1")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private IUserService userService;     
        public const string RetrieveUserRouteName = @"User";

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ScimUser userDto)
        {
            try
            {
                var user = await userService.CreateUser(userDto);
                return Created(Request.Host.Value + "/api/user/" + user.Id, user);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.InnerException.StackTrace);
            }
        }

        protected void SetContentLocationHeader(
            HttpResponseMessage response,
            string routeName,
            object routeValues = null)
        {
            Response.Headers.Add("Location",  Request.Host.Value + "/api/" + routeName);
        }
    }
}
