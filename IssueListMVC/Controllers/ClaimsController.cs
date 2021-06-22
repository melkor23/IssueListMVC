using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http;

namespace IssueListMVC.Controllers
{
    [Authorize]
    public class ClaimsController : ApiController
    {
        public IEnumerable<Claim> Get()
        {
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;

            return claims;
        }
    }
}
