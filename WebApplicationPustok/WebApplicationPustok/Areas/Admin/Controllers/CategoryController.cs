using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using WebApplicationPustok.Context;
using WebApplicationPustok.ViewModel.CategoryVM;

namespace Pustok_AzMB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        PustokDbContext _pd {  get; }

        public async Task<IActionResult> Index()
        {
           
            var items = await _pd.Categories.Select(c => new CategoryListItemVM
            {
                Name = c.Name,
                Id = c.Id,
                IsDeleted = c.IsDeleted,
                ParentId = c.ParentId,

            }).ToListAsync();
            return View(items);

        }


        public async Task<IActionResult> Create(CategoryCreateVM vm)
        {
          
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            if (await _pd.Categories.AnyAsync(x => x.Name == vm.Name))
            {
                ModelState.AddModelError("Name", "Name already exist ");
                return View(vm);
            }
            await _pd.Categories.AddAsync(new WebApplicationPustok.Models.Category
            {
                Name = vm.Name,
                ParentId = vm.ParentId
            });
            await _pd.SaveChangesAsync();



            return RedirectToAction(nameof(Index));


        }
    }
}
