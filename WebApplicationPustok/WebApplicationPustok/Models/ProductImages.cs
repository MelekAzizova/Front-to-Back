namespace WebApplicationPustok.Models
{
    public class ProductImages
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public Product? product { get; set; }
        public int ProductID {  get; set; }
        public bool IsActive { get; set; }

    }
}
