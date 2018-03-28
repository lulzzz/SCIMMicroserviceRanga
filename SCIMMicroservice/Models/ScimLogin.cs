using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScimMicroservice.Models
{
    public class ScimLogin
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
