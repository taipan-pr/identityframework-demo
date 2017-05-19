using System.Security.Claims;
using System.Threading.Tasks;
using Autofac;
using Microsoft.Owin.Security.OAuth;
using WebApi.Identity.Managers;

namespace WebApi.Identity.Providers
{
    public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            return Task.FromResult(context.Validated());
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var autofac = context.OwinContext.Environment.GetComponentContext();
            var userManager = autofac.Resolve<IUserManager>();
            var user = await userManager.FindByNameAsync(context.UserName);

            if (user == null)
            {
                context.SetError("Invalid User");
                return;
            }
            if (!await userManager.CheckPasswordAsync(user, context.Password))
            {
                context.SetError("Invalid UserName or Password");
                return;
            }

            var claimIdentity = new ClaimsIdentity(await userManager.GetClaimsAsync(user.Id), context.Options.AuthenticationType);
            claimIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            claimIdentity.AddClaim(new Claim(ClaimTypes.GivenName, user.UserName));
            claimIdentity.AddClaim(new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"));
            context.Validated(claimIdentity);
        }
    }
}
