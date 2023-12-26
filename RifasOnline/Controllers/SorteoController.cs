using Microsoft.AspNetCore.Mvc;

namespace RifasOnline.Controllers
{
    public class SorteoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Sorteos()
        {
            return View();
        }
    }
}
