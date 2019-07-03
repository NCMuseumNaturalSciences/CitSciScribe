using System;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace CitSciScribe.Attrributes
{
    public class HasTranscribeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var claims = ((ClaimsIdentity) httpContext.User.Identity).Claims;
            var canTranscribe = false;
            if (claims != null && claims.Count() > 0)
            {
                var canTranscribeClaim = claims.SingleOrDefault(m => m.Type == "CanTranscribe");
                if (canTranscribeClaim != null) canTranscribe = Convert.ToBoolean(canTranscribeClaim.Value);
            }

            return canTranscribe;
        }
    }
}