using Microsoft.AspNetCore.Mvc;
using TurnBookiWeb.Data;
using TurnBookiWeb.Models;

namespace TurnBookiWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext context;

        public CategoryController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            var categories = this.context.Categories.ToList();
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Create")]
        public IActionResult CreatePost(Category category)
        {
            if (!string.IsNullOrEmpty(category.Name) && this.context.Categories.Any(c => c.Name.ToLower() == category.Name.ToLower()))
            {
                ModelState.AddModelError("", "Category name already exists.");
            }
            
            if (ModelState.IsValid)
            {
                this.context.Categories.Add(category);
                this.context.SaveChanges();
                TempData["success"] = "Category created successfully.";
                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult Update(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var category = this.context.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Update")]
        public IActionResult UpdatePost(Category category)
        {
            if (!string.IsNullOrEmpty(category.Name) &&
                this.context.Categories.Any(c => c.Name.ToLower() == category.Name.ToLower() && c.Id != category.Id))
            {
                ModelState.AddModelError("", "Category name already exists.");
            }

            if (ModelState.IsValid)
            {
                this.context.Categories.Update(category);
                this.context.SaveChanges();
                TempData["success"] = "Category updated successfully.";
                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var category = this.context.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            var category = this.context.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }

            this.context.Categories.Remove(category);
            this.context.SaveChanges();
            TempData["success"] = "Category deleted successfully.";

            return RedirectToAction("Index");
        }
    }
}
