using Microsoft.AspNetCore.Mvc;

namespace Hourly.Api.Controllers
{
    public class GitRepositoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
