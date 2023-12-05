namespace Pustok_AzMB.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public int ParentId { get; set; }
    }
}
