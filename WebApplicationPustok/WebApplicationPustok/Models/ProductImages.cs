namespace Pustok_AzMB.Models
{
    public class ProductImages
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public Product product { get; set; }
        public int ProductId { get; set; }
        public bool IsActive { get; set; }

    }
}
