using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace IssueListMVC.Providers
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            //TODO: create client secret to validate clients
             context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            //TODO: create repo  for user validation and datable with username and password, add roles...
            if (context.UserName != "user" || context.Password != "pass")
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }

          

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim("user", context.UserName));
            identity.AddClaim(new Claim("role", "user"));

          
            context.Validated(identity);

        }
    }
}