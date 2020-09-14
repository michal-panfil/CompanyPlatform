using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace CompanyWebApi.Helpers
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {

        public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
               ILoggerFactory logger,
                UrlEncoder encoder,
               ISystemClock clock)
            : base(options, logger, encoder, clock) { }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var entpoint = Context.GetEndpoint();
            if (entpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null)
            {
                return AuthenticateResult.NoResult();

            }

            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return AuthenticateResult.Fail("Missing Authorisation Header");
            }

            var isAuthorize = false;

            try
            {
                var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
                var credentials = Encoding.UTF8.GetString(credentialBytes).Split(new[] { ':' }, 2);
                var username = credentials[0];
                var password = credentials[1];

                isAuthorize = username.Equals("username") && password.Equals("password");

            }  
            catch 
            {
                return AuthenticateResult.Fail("Invalid Authorisation Header");
            }

            if (!isAuthorize)
            {
                return AuthenticateResult.Fail("Invalid Username or Password");
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, "username"),
                new Claim(ClaimTypes.Name, "username")
            };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);

        }
    }
}
