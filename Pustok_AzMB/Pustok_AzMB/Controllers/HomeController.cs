using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pustok_AzMB.Context;
using System.Diagnostics;

namespace Pustok_AzMB.Controllers
{
    public class HomeController : Controller
    {
        
        
        public async Task<IActionResult> Index()
        {
            using (PustokDbContext db=new PustokDbContext())
            {
                var slider = await db.Sliders.ToListAsync();
                return View(slider);  
            }
        }

        
    }
}
