using System.ComponentModel.DataAnnotations;

namespace Pustok_AzMB.ViewModel.SliderVM
{
    public class SliderListItem
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        
        public string Title { get; set; }
        
        public string Description { get; set; }
        public bool IsLeft { get; set; }
    }
}
