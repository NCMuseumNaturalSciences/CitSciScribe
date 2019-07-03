using System.Web.Mvc;
using CitSciScribe.Data;
using CitSciScribe.Models;

namespace CitSciScribe.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var viewModel = TranscriptionManager.GetSiteStatistics(new DBContext());
            return View(viewModel);
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Links()
        {
            return View();
        }
    }
}