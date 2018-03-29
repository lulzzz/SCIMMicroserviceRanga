using Microsoft.AspNetCore.Mvc;

namespace ScimMicroservice.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class GroupsController : Controller
    {
    }
}
