namespace Pustok_AzMB.ViewModel.CategoryVM
{
    public class CategoryListItemVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public int ParentId { get; set; }
    }
}
