using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationPustok.Areas.Admin.ViewModels;
using WebApplicationPustok.Context;
using WebApplicationPustok.Helpers;
using WebApplicationPustok.Models;

namespace WebApplicationPustok.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PoductImgController : Controller
    {
        public PoductImgController(PustokDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }

        PustokDbContext _db {  get; set; }
        IWebHostEnvironment _env { get; }
        public async  Task<IActionResult> Index()
        {
            var items = await _db.ProductImages.Select(s => new AdminProductImagesVM
            {
                Id = s.Id,
                ImagePath = s.ImagePath
            }).ToListAsync();
            return View(items);

           
        }

        public IActionResult Create()
        {
            ViewBag.Products = _db.Products;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductImagesCreateVM vm)
        {
            ViewBag.Products = _db.Products;
            
            if (!ModelState.IsValid) return View(vm);
            ProductImages pi = new ProductImages
            {
                ProductID = vm.ProductId,
                ImagePath = await vm.ImagePath.SaveAsync(PathConstants.ProductImages)
            };
            await _db.ProductImages.AddAsync(pi);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}
