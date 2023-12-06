using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebApplicationPustok.Areas.Admin.ViewModels;
using WebApplicationPustok.Context;
using WebApplicationPustok.Models;
using WebApplicationPustok.ViewModel.ProductVM;
using WebApplicationPustok.ViewModel.SliderVM;

namespace WebApplicationPustok.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
         PustokDbContext _db;
        public ProductController(PustokDbContext dbContext)
        {
                _db = dbContext;
        }

        public IActionResult Index()
        {
            var products =  _db.Products.Include(i => i.productImages).ToList();    
            AdminProductListItemVM items = new AdminProductListItemVM();

          _db.Products.Select(p => new AdminProductListItemVM
          { 
                Id = p.Id,
                Name = p.Name,
                Title = p.Title,
                Description = p.Description,
                CostPrice = p.CostPrice,
                Discount = p.Discount,
                Category = p.Category,
                ImageUrl = p.ImageUrl,
                IsDeleted = p.IsDeleted,
                Quantity = p.Quantity,
                SellPrice = p.SellPrice ,
                ProductCode=p.ProductCode,
                ProductId=p.ProductId,
                CategoryId=p.CategoryId

      
         });
           
            return View(products);
        }
           
            public IActionResult Create()
            {
               

                ViewBag.Categories = _db.Categories;
                return View();
            }

        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateVM vm)
        {
          
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            if (await _db.Categories.AnyAsync(x => x.Name == vm.Name))
            {
                ModelState.AddModelError("Name", "Name already exist ");
                return View(vm);
            }
            await _db.Categories.AddAsync(new WebApplicationPustok.Models.Category
            {
                Name = vm.Name,
                
            });
            await _db.SaveChangesAsync();



            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null || id <= 0) return BadRequest();
           
            var data = await _db.Sliders.FindAsync(id);
            if (data == null) return NotFound();
            return View(new SliderUpdateVM
            {

                Title = data.Title,
                Description = data.Description,
                ImageUrl = data.ImageUrl,
                Position = data.IsLeft switch
                {
                    true => 1,
                    false => 0
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
           
            var data = await _db.Sliders.FindAsync(id);
            if (data == null) return NotFound();
            data.Title = vm.Title;
            data.Description = vm.Description;
            data.ImageUrl = vm.ImageUrl;
            data.IsLeft = vm.Position switch
            {
                1 => true,
                0 => false
            };
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));




        }
    }

}
}
