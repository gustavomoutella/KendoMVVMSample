using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Vsol.Api.Shared.Domain;

namespace Vsol.Api.Controllers
{
    public class BaseController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        public Guid? IdUser
        {
            get
            {
                var claim = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

                if (claim == null)
                    return null;

                return new Guid(claim.Value);
            }
        }
    }
}
