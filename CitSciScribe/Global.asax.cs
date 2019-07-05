using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using CitSciScribe.Attrributes;

namespace CitSciScribe
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            //System.Data.Entity.Database.SetInitializer<DBContext>(null);
            GlobalFilters.Filters.Add(new UserProfileActionFilter(), 0);
            //AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}