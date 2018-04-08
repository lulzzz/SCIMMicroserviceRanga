using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScimMicroservice.Controllers
{
    public class BaseController : Controller
    {
        public void AddLocationHeader(string route, string routevalue = null)
        {
            var locationHeader = Request.Host + "/api/" + route;

            if (!string.IsNullOrWhiteSpace(routevalue))
            {
                locationHeader = locationHeader + "/" + routevalue;
            }

            Response.Headers.Add("Location", locationHeader);
        }
    }
}
