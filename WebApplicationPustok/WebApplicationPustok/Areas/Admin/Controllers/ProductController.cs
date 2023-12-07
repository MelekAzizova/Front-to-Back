using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using WebApplicationPustok.Areas.Admin.ViewModels;
using WebApplicationPustok.Context;

using WebApplicationPustok.ViewModel.ProductVM;
using WebApplicationPustok.ViewModel.SliderVM;

namespace WebApplicationPustok.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
         PustokDbContext _db;
        public ProductController(PustokDbContext dbContext)
        {
                _db = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            var products =  _db.Products.Include(i => i.productImages).ToList();
            AdminProductListItemVM items = new AdminProductListItemVM();

         var x = await _db.Products.Select(p => new AdminProductListItemVM
          { 
                Id = p.Id,
                Name = p.Name,
                Title = p.Title,
                Description = p.Description,
                CostPrice = p.CostPrice,
                Discount = p.Discount,
                Category = p.Category,
              
                IsDeleted = p.IsDeleted,
                Quantity = p.Quantity,
                SellPrice = p.SellPrice ,
                ProductCode=p.ProductCode,
                
                CategoryId=p.CategoryId

      
         }).ToListAsync();
           
            return View(x);
        }
           
            public IActionResult Create()
            {
               

                ViewBag.Categories = _db.Categories.ToList();
                return View();
            }

        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateVM vm)
        {
            ViewBag.Categories = _db.Categories.ToList();
            if (!ModelState.IsValid)
            {
               
                return View(vm);
            }
            if (await _db.Products.AnyAsync(x => x.Name == vm.Name))
            {
                ModelState.AddModelError("Name", "Name already exist ");

                return View(vm);
            }
            await _db.Products.AddAsync(new WebApplicationPustok.Models.Product
            {
             
               
                Name = vm.Name
                
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

