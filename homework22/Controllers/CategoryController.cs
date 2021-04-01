using System.Diagnostics;
using System.Threading.Tasks;
using homework22.DbContext;
using homework22.Models;
using homework22.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace homework22.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ShopDbContext _shopDbContext;

        public CategoryController(ShopDbContext shopDbContext)
        {
            _shopDbContext = shopDbContext;
        }

        public async Task<IActionResult> Index() => View(await _shopDbContext.Categories.ToListAsync());

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return RedirectToAction("Index");

            var category = await _shopDbContext.Categories.FindAsync(id);
            if (category == null)
                return RedirectToAction("Index");

            _shopDbContext.Categories.Remove(category);
            await _shopDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CategoryViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _shopDbContext.Categories.AddAsync(new Category {Name = model.Name});

            await _shopDbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id <= 0)
                return RedirectToAction("Index");

            var category = await _shopDbContext.Categories.FindAsync(id);

            if (category == null)
                return RedirectToAction("Index");

            return View(new CategoryViewModel
            {
                Id = category.Id,
                Name = category.Name,
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoryViewModel model)
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel
            {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}