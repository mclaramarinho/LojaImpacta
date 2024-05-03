using Microsoft.AspNetCore.Mvc;

namespace LojaImpacta.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction(actionName: nameof(Index), controllerName: "Products");
        }
    }
}
