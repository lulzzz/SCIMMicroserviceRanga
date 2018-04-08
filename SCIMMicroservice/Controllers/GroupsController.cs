using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ScimMicroservice.BLL.Interfaces;
using ScimMicroservice.Exceptions;
using ScimMicroservice.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ScimMicroservice.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class GroupsController : BaseController
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
        [HttpGet("{groupId}")]
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

        [Authorize]
        [HttpDelete("{grouId}")]
        public async Task<IActionResult> Delete(int groupId)
        {
            try
            {
                await groupService.DeleteGroup(groupId);
                return NoContent();
            }
            catch (NotFoundException)
            {
                return new NotFoundObjectResult("Group could not be found");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.StackTrace);
            }
        }

        [Authorize]
        [HttpPut("{grouId}")]
        public async Task<IActionResult> Put(int grouId, [FromBody] ScimGroup group)
        {
            try
            {
                //userDto.Id = userId;
                var grp = await groupService.UpdateGroup(group);
                AddLocationHeader("groups", grouId.ToString());
                return Ok(grp);
            }
            catch (NotFoundException)
            {
                return new NotFoundObjectResult("Group could not be found");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.StackTrace);
            }
        }

        [Authorize]
        [HttpPatch("{groupId}")]
        public async Task<IActionResult> Patch(int groupId, [FromBody] PatchRequest<ScimGroup> groupPatch)
        {
            try
            {
                if (!groupPatch.Schemas.Any()
                    || groupPatch.Schemas[0] != ScimConstants.Messages.PatchOp)
                {
                    return BadRequest("Invalid schema");
                }

                var patches = new JsonPatchDocument<ScimGroup>();

                patches.Operations.AddRange(groupPatch.Operations);

                var group = await groupService.GetGroup(groupId);

                patches.ApplyTo(group);

                var pachedGroup = await groupService.UpdateGroup(group);

                AddLocationHeader("groups", groupId.ToString());
                return Ok(pachedGroup);
            }
            catch (NotFoundException)
            {
                return new NotFoundObjectResult("User not found");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.StackTrace);
            }
        }
    }
}
