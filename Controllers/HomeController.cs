using ByteFix.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ByteFix.Controllers;

public sealed class HomeController : Controller
{
    public IActionResult Index()
    {
        return View(new DashboardViewModel());
    }

    public IActionResult Error()
    {
        return View();
    }
}
