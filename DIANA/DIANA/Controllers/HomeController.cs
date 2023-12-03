using DIANA.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DIANA.Controllers
{
    public class HomeController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }

        
    }
}
