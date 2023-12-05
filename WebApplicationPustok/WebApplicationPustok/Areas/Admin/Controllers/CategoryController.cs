using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pustok_AzMB.Context;
using Pustok_AzMB.ViewModel.CategoryVM;

namespace Pustok_AzMB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        

        public async Task<IActionResult> Index()
        {
            using PustokDbContext context= new PustokDbContext();
                var items = await context.Categories.Select(c => new CategoryListItemVM
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
            using PustokDbContext context = new PustokDbContext();
            if (ModelState.IsValid)
            {
                return View(vm);
            }
            if (await context.Categories.AnyAsync(x => x.Name == vm.Name))
            {
                ModelState.AddModelError("Name", "Name already exist ");
                return View(vm);
            }
            await context.Categories.AddAsync(new Models.Category
            {
                Name = vm.Name,
                ParentId = vm.ParentId
            });
            await context.SaveChangesAsync();



            return RedirectToAction(nameof(Index));


        }
    }
}
