using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using homework22.DbContext;
using homework22.Models;
using homework22.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace homework22.Controllers
{
    public class ProductController : Controller
    {
        private readonly ShopDbContext _shopDbContext;

        public ProductController(ShopDbContext shopDbContext)
        {
            _shopDbContext = shopDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string category)
        {
            List<Product> products;
            if (category == null)
                products = await _shopDbContext.Products.ToListAsync();
            else
                products = await _shopDbContext.Products.Where(p => p.Category.Name.Equals(category)).ToListAsync();
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await _shopDbContext.Categories.ToListAsync();

            return View(new ProductViewModel
            {
                Categories = categories.Select(category => new SelectListItem
                    {Text = category.Name, Value = category.Id.ToString()}).ToList()
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _shopDbContext.Products.AddAsync(new Product
            {
                Name = model.Name,
                Price = model.Price,
                CategoryId = model.CategoryId
            });

            await _shopDbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id <= 0)
                return RedirectToAction("Index");

            var product = await _shopDbContext.Products.FindAsync(id);

            if (product == null)
                return RedirectToAction("Index");

            return View(new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Categories = await _shopDbContext.Categories
                    .Select(p => new SelectListItem {Text = p.Name, Value = p.Id.ToString()}).ToListAsync()
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var category = await _shopDbContext.Categories.FindAsync(model.Id);
            if (category == null)
                return RedirectToAction("Index");

            category.Name = model.Name;
            await _shopDbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return RedirectToAction("Index");

            var product = await _shopDbContext.Products.FindAsync(id);
            if (product == null)
                return RedirectToAction("Index");

            _shopDbContext.Products.Remove(product);
            await _shopDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel
            {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}