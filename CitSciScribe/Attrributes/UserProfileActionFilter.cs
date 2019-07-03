using System;
using System.Linq;
using System.Security.Claims;
using System.Web.Mvc;

namespace CitSciScribe.Attrributes
{
    public class UserProfileActionFilter : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            var claims = ((ClaimsIdentity) filterContext.RequestContext.HttpContext.User.Identity).Claims;
            var canApprove = false;
            var canTranscribe = false;
            var name = "";
            var transcriptionCount = 0;

            if (claims != null && claims.Count() > 0)
            {
                var canApproveClaim = claims.SingleOrDefault(m => m.Type == "CanApprove");
                if (canApproveClaim != null) canApprove = Convert.ToBoolean(canApproveClaim.Value);

                var canTranscribeClaim = claims.SingleOrDefault(m => m.Type == "CanTranscribe");
                if (canTranscribeClaim != null) canTranscribe = Convert.ToBoolean(canTranscribeClaim.Value);

                var nameClaim = claims.SingleOrDefault(m => m.Type == "Name");
                if (nameClaim != null) name = nameClaim.Value;

                var transcriptionCountClaim = claims.SingleOrDefault(m => m.Type == "TranscriptionCount");
                if (transcriptionCountClaim != null)
                    transcriptionCount = Convert.ToInt32(transcriptionCountClaim.Value);
            }

            filterContext.Controller.ViewBag.CanApprove = canApprove;
            filterContext.Controller.ViewBag.CanTranscribe = canTranscribe;
            filterContext.Controller.ViewBag.Name = name;
            filterContext.Controller.ViewBag.TranscriptionCount = transcriptionCount;
            base.OnResultExecuting(filterContext);
        }
    }
}