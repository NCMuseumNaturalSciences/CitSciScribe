using System;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace CitSciScribe.Attrributes
{
    public class HasApproveAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var claims = ((ClaimsIdentity) httpContext.User.Identity).Claims;
            var canApprove = false;

            if (claims != null && claims.Count() > 0)
            {
                var canApproveClaim = claims.SingleOrDefault(m => m.Type == "CanApprove");
                if (canApproveClaim != null) canApprove = Convert.ToBoolean(canApproveClaim.Value);
            }

            return canApprove;
        }
    }
}