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
            using (PustokDbContext context = new PustokDbContext())
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

        public async Task<IActionResult> Create(SliderCreateVM vm)
        {
            if (vm.Position < -1 || vm.Position > 1)
            {
                ModelState.AddModelError("Position", "Wrong input");
            }

            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            using PustokDbContext db = new PustokDbContext();
            Slider slider = new Slider
            {
                Title = vm.Title,
                Description = vm.Description,
                ImageUrl = vm.ImageUrl,
                IsLeft = vm.Position switch
                {
                    1 => true,
                    0 => false
                }


            };
            await db.Sliders.AddAsync(slider);
            await db.SaveChangesAsync();
            return View();


        }
        public async Task<IActionResult> Delete(int? id)
        {
            
            if (id == null) return BadRequest();
            using PustokDbContext pd = new();
            var data = await pd.Sliders.FindAsync(id);
            if (data == null) return NotFound();
            pd.Sliders.Remove(data);
            await pd.SaveChangesAsync();
            //TempData["Response"] = new
            //{
            //    Icon = "Success",
            //    Title = "Data deleted succesfully"

            //};
            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null || id<=0) return BadRequest();
            using PustokDbContext pd = new();
            var data = await pd.Sliders.FindAsync(id);
            if(data==null) return NotFound();
            return View(new SliderUpdateVM
            {
               
                Title=data.Title,
                Description=data.Description,
                ImageUrl=data.ImageUrl,
                Position=data.IsLeft switch
                {
                     true=>1,
                     false=>0
                }
            });

        }
        [HttpPost]

        public async Task<IActionResult> Update(int? id, SliderUpdateVM vm)
        {
            if (id == null || id <= 0) return BadRequest();
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            using PustokDbContext pd = new();
            var data = await pd.Sliders.FindAsync(id);
            if (data == null) return NotFound();
            data.Title = vm.Title;
            data.Description = vm.Description;
            data.ImageUrl = vm.ImageUrl;
            data.IsLeft = vm.Position switch
            {
                1 => true,
                0 => false
            };
            await pd.SaveChangesAsync();
            return RedirectToAction(nameof(Index));




        }
    }

    

       






        //public async Task<IActionResult> Create(Slider slider)
        //{
        //    if(!ModelState.IsValid)
        //    {
        //        return View(slider);
        //    }
        //    using PustokDbContext context = new PustokDbContext();
        //    await context.Sliders.AddAsync(slider);
        //    await context.SaveChangesAsync(); 


        //    return View();


        //}

    }


