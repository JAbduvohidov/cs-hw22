using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace homework22.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Invalid product name")]
        [MaxLength(30)]
        [MinLength(2)]
        [Display(Name = "Product name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Invalid price")]
        [Display(Name = "Price")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Invalid category")]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public List<SelectListItem> Categories { get; set; } = new();
    }
}