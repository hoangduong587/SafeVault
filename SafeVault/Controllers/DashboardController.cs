using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SafeVault.Controllers
{
    [Authorize] // Protects all actions in this controller
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}