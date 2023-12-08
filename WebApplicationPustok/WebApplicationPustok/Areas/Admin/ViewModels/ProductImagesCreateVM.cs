namespace WebApplicationPustok.Areas.Admin.ViewModels
{
    public class ProductImagesCreateVM
    {
        public IFormFile ImagePath { get; set; }
        public int ProductId { get; set; }
        public bool IsActive { get; set; }
    }
}
