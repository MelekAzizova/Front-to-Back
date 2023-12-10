using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using WebApplicationPustok.Areas.Admin.ViewModels;
using WebApplicationPustok.Context;
using WebApplicationPustok.Helpers;
using WebApplicationPustok.Models;
using WebApplicationPustok.ViewModel.ProductVM;


namespace WebApplicationPustok.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
         PustokDbContext _db;
        IWebHostEnvironment _env;
        public ProductController(PustokDbContext dbContext, IWebHostEnvironment env = null)
        {
            _db = dbContext;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            //var products =  _db.Products.Include(i => i.productImages).ToList();
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
                SellPrice = p.SellPrice,
                ProductCode = p.ProductCode,
                ImgFile = p.ImagrUrl,
   

                CategoryId = p.CategoryId


            }).ToListAsync();
           
            return View(x);
        }
           
            public IActionResult Create()
            {

                // ViewBag.Categories = _db.Categories;

               ViewBag.Categories = _db.Categories.ToList();
                return View();
            }

        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateVM vm)
           
        {
            
            if (!vm.ImgFile.IsCorrectType("image"))
            {
                ModelState.AddModelError("ImgFile", "Wrong file type");
            }
            //if (!vm.ImgFile.IsValidSize(1f))
            //{
            //    ModelState.AddModelError("ImgFile", "Wrong file size");
            //}
            ViewBag.Categories = _db.Categories.ToList();
            if (!ModelState.IsValid)
            {
               
                return View(vm);
            }
            if (await _db.Products.AnyAsync(x => x.Name == vm.Name))
            {
                ModelState.AddModelError("Name", "Name already exist");

                return View(vm);
            }


            Product product = new Product
            {
                Name = vm.Name,
                Title = vm.Title,
                Description = vm.Description,
                CostPrice = vm.CostPrice,
                Discount = vm.Discount,

                Quantity = vm.Quantity,
                SellPrice = vm.SellPrice,
                ProductCode = vm.ProductCode,

                CategoryId = vm.CategoryId,
                ImagrUrl = await vm.ImgFile.SaveAsync(PathConstants.Product)

            };
            await _db.Products.AddAsync(product);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null || id <= 0) return BadRequest(); 
           
            var data = await _db.Products.FindAsync(id);
            if (data == null) return NotFound();
            return View(new ProductUpdateVM
            {

                Name = data.Name,
                Title = data.Title,
                Description = data.Description,
                CostPrice = data.CostPrice,
                Discount = data.Discount,
                Quantity = data.Quantity,
                SellPrice = data.SellPrice,
                ProductCode = data.ProductCode,

                CategoryId = data.CategoryId

            });

        }
        [HttpPost]

        public async Task<IActionResult> Update(int? id, ProductUpdateVM vm)
        {
            if (id == null || id <= 0) return BadRequest();
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
           
            var data = await _db.Products.FindAsync(id);
            if (data == null) return NotFound();
            data.Name = vm.Name;
            data.Title = vm.Title;
            data.Description = vm.Description;
            data.CostPrice = vm.CostPrice;
            data.Discount = vm.Discount;

            data.Quantity = vm.Quantity;
            data.SellPrice = vm.SellPrice;
            data.ProductCode = vm.ProductCode;

            data.CategoryId = vm.CategoryId;


            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));




        }

        public async Task<IActionResult> Delete(int? id)
        {

            if (id == null) return BadRequest();

            var data = await _db.Products.FindAsync(id);
            if (data == null) return NotFound();
            _db.Products.Remove(data);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
    }

}

