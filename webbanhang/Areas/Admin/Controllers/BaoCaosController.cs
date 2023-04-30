using Microsoft.AspNetCore.Mvc;

namespace webbanhang.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BaoCaosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
