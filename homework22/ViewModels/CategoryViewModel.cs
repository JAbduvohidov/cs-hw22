using System.ComponentModel.DataAnnotations;

namespace homework22.ViewModels
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Invalid category name")]
        [Display(Name = "Category name")]
        [MaxLength(30)]
        [MinLength(2)]
        public string Name { get; set; }
    }
}