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
    public class UsersController : Controller
    {
        private IUserService userService;     
        public const string RetrieveUserRouteName = @"Users";

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        /// <summary>
        /// To create new resources, clients send HTTP POST requests to the
        /// Tresource endpoint, such as "/Users" or "/Groups", as defined by the
        /// Tassociated resource type endpoint discovery(see Section 4).
        /// 
        /// The server SHALL process attributes according to the following
        /// mutability rules:
        /// 
        /// o In the request body, attributes whose mutability is "readOnly"
        /// (see Sections 2.2 and 7 of[RFC7643]) SHALL be ignored.
        /// 
        /// o Attributes whose mutability is "readWrite" (see Section 2.2 of
        /// [RFC7643]) and that are omitted from the request body MAY be
        /// assumed to be not asserted by the client.The service provider
        /// MAY assign a default value to non-asserted attributes in the final
        /// resource representation.
        /// 
        /// o Service providers MAY take into account whether or not a client
        /// has access to all of the resource's attributes when deciding
        /// whether or not non-asserted attributes should be defaulted.
        /// 
        /// o Clients that intend to override existing or server-defaulted
        /// values for attributes MAY specify "null" for a single-valued
        /// attribute or an empty array "[]" for a multi-valued attribute to
        /// clear all values.
        /// 
        /// When the service provider successfully creates the new resource, an
        /// HTTP response SHALL be returned with HTTP status code 201 (Created).
        /// The response body SHOULD contain the service provider's
        /// representation of the newly created resource.The URI of the created
        /// resource SHALL include, in the HTTP "Location" header and the HTTP
        /// body, a JSON representation[RFC7159] with the attribute
        /// "meta.location".  Since the server is free to alter and/or ignore
        /// POSTed content, returning the full representation can be useful to
        /// the client, enabling it to correlate the client's and server's views
        /// of the new resource.
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ScimUser userDto)
        {
            try
            {
                var user = await userService.CreateUser(userDto);
                return Created(Request.Host.Value + "/api/users/" + user.Id, user);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.InnerException.StackTrace);
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var users = await userService.GetUsers();
                AddLocationHeader("users");
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.StackTrace);
            }
        }

        /// <summary>
        /// To retrieve a known resource, clients send GET requests to the
        /// resource endpoint, e.g., "/Users/{id}", "/Groups/{id}", or
        /// "/Schemas/{id}", where "{id}" is a resource identifier(for example,
        /// the value of the "id" attribute).
        ///
        /// If the resource exists, the server responds with HTTP status code 200
        /// (OK) and includes the result in the body of the response.
        ///
        /// The example below retrieves a single User via the "/Users" endpoint.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("{userId}")]
        public async Task<IActionResult> Get(int userId)
        {
            try
            {
                var users = await userService.GetUser(userId);
                AddLocationHeader("users", userId.ToString());
                return Ok(users);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.StackTrace);
            }
        }

        /// <summary>
        /// Clients request resource removal via DELETE.  Service providers MAY
        /// choose not to permanently delete the resource but MUST return a 404
        /// (Not Found) error code for all operations associated with the
        /// previously deleted resource.Service providers MUST omit the
        /// resource from future query results.In addition, the service
        /// provider SHOULD NOT consider the deleted resource in conflict
        /// calculation.  For example, if a User resource is deleted, a CREATE
        /// request for a User resource with the same userName as the previously
        /// deleted resource SHOULD NOT fail with a 409 error due to userName
        /// conflict.
        /// 
        /// In response to a successful DELETE, the server SHALL return a
        /// In response to a successful DELETE, the server SHALL return a
        /// successful HTTP status code 204 (No Content).  A non-normative
        /// example response:
        /// </summary>
        /// <param name="userId">int userId</param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete("{userId}")]
        public async Task<IActionResult> Delete(int userId)
        {
            try
            {
                await userService.Delete(userId);
                return NoContent();
            }
            catch(NotFoundException)
            {
                return new NotFoundObjectResult("User not found");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.StackTrace);
            }
        }

        /// <summary>
        /// HTTP PUT is used to replace a resource's attributes.  For example,
        /// clients that have previously retrieved the entire resource in advance
        /// and revised it MAY replace the resource using an HTTP PUT.Because
        ///
        /// SCIM resource identifiers are assigned by the service provider, HTTP
        /// PUT MUST NOT be used to create new resources.
        ///
        /// As the operation's intent is to replace all attributes, SCIM clients
        /// MAY send all attributes, regardless of each attribute's mutability.
        /// The server will apply attribute-by-attribute replacements according
        /// to the following attribute mutability rules:
        /// </summary>
        /// <param name="userId">int userId</param>
        /// <param name="userDto">ScimUser userDto</param>
        /// <returns></returns>
        [Authorize]
        [HttpPut("{userId}")]
        public async Task<IActionResult> Put(int userId, [FromBody] ScimUser userDto)
        {
            try
            {
                //userDto.Id = userId;
                var user = await userService.UpdateUser(userDto);
                AddLocationHeader("users", userId.ToString());
                return Ok(user);
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

        /// <summary>
        /// HTTP PATCH is an OPTIONAL server function that enables clients to
        /// update one or more attributes of a SCIM resource using a sequence of
        /// operations to "add", "remove", or "replace" values.Clients may
        /// discover service provider support for PATCH by querying the service
        /// provider configuration(see Section 4).
        /// The general form of the SCIM PATCH request is based on JSON Patch
        /// [RFC6902].  One difference between SCIM PATCH and JSON Patch is that
        /// SCIM servers do not support array indexing and do not support
        /// [RFC6902] operation types relating to array element manipulation,
        /// such as "move".
        /// 
        /// The body of each request MUST contain the "schemas" attribute with
        /// the URI value of "urn:ietf:params:scim:api:messages:2.0:PatchOp".
        /// 
        /// The body of an HTTP PATCH request MUST contain the attribute
        /// "Operations", whose value is an array of one or more PATCH
        /// operations.Each PATCH operation object MUST have exactly one "op"
        /// member, whose value indicates the operation to perform and MAY be one
        /// of "add", "remove", or "replace".  The semantics of each operation
        /// are defined in the following subsections.
        /// 
        /// The following is an example representation of a PATCH request showing
        /// the basic JSON structure (non-normative):
        /// </summary>
        /// <param name="userId">int userId</param>
        /// <param name="patches">JsonPatchDocument<ScimUser> patches</param>
        /// <returns></returns>
        [Authorize]
        [HttpPatch("{userId}")]
        public async Task<IActionResult> Patch(int userId, [FromBody] PatchRequest<ScimUser> userPatch)
        {
            try
            {
                if (!userPatch.Schemas.Any()
                    || userPatch.Schemas[0] != ScimConstants.Messages.PatchOp)
                {
                    return BadRequest("Invalid schema");
                }

                var patches = new JsonPatchDocument<ScimUser>();

                patches.Operations.AddRange(userPatch.Operations);

                var user = await userService.GetUser(userId);

                patches.ApplyTo(user);

                var pachedUser = await userService.UpdateUser(user);

                foreach(var schema in pachedUser.Schemas)
                {
                    schema.Replace("ResourceType", "user");
                }

                AddLocationHeader("users", userId.ToString());
                return Ok(pachedUser);
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

        private void AddLocationHeader(string route, string routevalue = null)
        {
            var locationHeader =  Request.Host + "/api/" + route;

            if (!string.IsNullOrWhiteSpace(routevalue))
            {
                locationHeader = locationHeader + "/" + routevalue;
            }

            Response.Headers.Add("Location", locationHeader);
        }
    }
}
