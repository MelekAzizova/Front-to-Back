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

                var items = await context.Sliders.Select(s => new SliderListItem
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

        public async Task<IActionResult> Create(SliderCreateVM vm)
        {
            if(!ModelState.IsValid)
            {
                return View(vm);
            }
           
            
            return View();


        }

    }
}
