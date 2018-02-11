using System.Web.Mvc;

namespace Misitu.Web.Controllers
{
    public class AboutController : MisituControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}