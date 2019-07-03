using System;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;

namespace CitSciScribe.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }
        public string DisplayTitle { get; set; }
        public bool IsSiteAdmin { get; set; }
        public bool IsCollectionManager { get; set; }
        public bool CanTranscribe { get; set; }
        public bool CanApprove { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            var context = new DBContext();
            var transcriptionCount = await context.Transcriptions.CountAsync(c => c.CreatedById == Id);

            // Add custom user claims here
            userIdentity.AddClaim(new Claim("CanApprove", CanApprove ? "True" : "False"));
            userIdentity.AddClaim(new Claim("CanTranscribe", CanTranscribe ? "True" : "False"));
            userIdentity.AddClaim(new Claim("Name", FirstName));
            userIdentity.AddClaim(new Claim("TranscriptionCount", transcriptionCount.ToString()));
            return userIdentity;
        }
    }

    public static class Extensions
    {
        public static void AddUpdateClaim(this ClaimsIdentity identity, string key, string value)
        {
            if (identity == null)
                return;

            // check for existing claim and remove it
            var existingClaim = identity.FindFirst(key);
            if (existingClaim != null)
                identity.RemoveClaim(existingClaim);

            // add new claim
            identity.AddClaim(new Claim(key, value));
            var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
            authenticationManager.AuthenticationResponseGrant =
                new AuthenticationResponseGrant(new ClaimsPrincipal(identity),
                    new AuthenticationProperties {IsPersistent = true});
        }

        public static void IncrementClaim(this ClaimsIdentity identity, string key)
        {
            if (identity == null)
                return;

            // check for existing claim and remove it
            var existingClaim = identity.FindFirst(key);
            var oldValue = 0;

            if (existingClaim != null)
            {
                oldValue = Convert.ToInt32(existingClaim.Value);
                identity.RemoveClaim(existingClaim);
            }

            // add new claim
            identity.AddClaim(new Claim(key, (oldValue + 1).ToString()));
            var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
            authenticationManager.AuthenticationResponseGrant =
                new AuthenticationResponseGrant(new ClaimsPrincipal(identity),
                    new AuthenticationProperties {IsPersistent = true});
        }
    }
}