using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pustok_AzMB.Context;
using Pustok_AzMB.Models;
using Pustok_AzMB.ViewModel.SliderVM;
using System.Security.AccessControl;

namespace Pustok_AzMB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        public async Task<IActionResult> Index()
        {
            using(PustokDbContext context=new PustokDbContext())
            {

                var items = await context.Sliders.Select(s => new SliderListItemVM
                {
                    Title = s.Title,
                    Description = s.Description,
                    ImageUrl = s.ImageUrl,
                    IsLeft = s.IsLeft,
                    Id = s.Id
                }).ToListAsync();
                return View(items);
            }

        }

        public IActionResult Create()
        {
              
            return View();
   
        }
        [HttpPost]

        public async Task<IActionResult> Create(Slider slider)
        {
            if(!ModelState.IsValid)
            {
                return View(slider);
            }
            using PustokDbContext context = new PustokDbContext();
            await context.Sliders.AddAsync(slider);
            await context.SaveChangesAsync(); 
           
            
            return View();


        }

    }
}
