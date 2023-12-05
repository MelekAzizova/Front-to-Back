using Microsoft.AspNetCore.Mvc;
using Pustok_AzMB.Context;

namespace Pustok_AzMB.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
       

        public IActionResult Index()
        {
           
            return View();
        }
    }
}
