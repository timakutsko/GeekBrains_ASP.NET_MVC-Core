using Microsoft.AspNetCore.Mvc;

namespace EmployeesWebApplication.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
