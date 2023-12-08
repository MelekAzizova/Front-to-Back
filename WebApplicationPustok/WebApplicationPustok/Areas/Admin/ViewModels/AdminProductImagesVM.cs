using WebApplicationPustok.Models;

namespace WebApplicationPustok.Areas.Admin.ViewModels
{
    public class AdminProductImagesVM
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public Product? product { get; set; }

        public bool IsActive { get; set; }
    }
}
