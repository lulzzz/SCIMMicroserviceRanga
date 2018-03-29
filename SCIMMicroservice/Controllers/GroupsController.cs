using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScimMicroservice.BLL.Interfaces;
using ScimMicroservice.Models;
using System;
using System.Threading.Tasks;

namespace ScimMicroservice.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class GroupsController : Controller
    {
        private IGroupService groupService;
        public const string RetrieveUserRouteName = @"Users";

        public GroupsController(IGroupService groupService)
        {
            this.groupService = groupService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ScimGroup groupDto)
        {
            try
            {
                var user = await groupService.CreateGroup(groupDto);
                return Created(Request.Host.Value + "/api/groups/" + user.Id, user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.StackTrace);
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Get(int groupId)
        {
            try
            {
                var user = await groupService.GetGroup(groupId);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.StackTrace);
            }
        }
    }
}
