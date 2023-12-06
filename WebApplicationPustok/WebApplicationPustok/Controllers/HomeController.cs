using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using WebApplicationPustok.Context;

namespace WebApplicationPustok.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(PustokDbContext pd)
        {
            _pd = pd;
        }

        PustokDbContext _pd {  get; }
        
        
        public async Task<IActionResult> Index()
        {
           
                var slider = await _pd.Sliders.ToListAsync();
                return View(slider);  
          
        }

        
    }
}
