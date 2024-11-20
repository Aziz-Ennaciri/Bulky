using BulkyDataAccess.Data;
using BulkyDataAccess.Repository.Interf;
using BulkyModels.Category;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepositiry;
        public CategoryController(ICategoryRepository db) { _categoryRepositiry = db; }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _categoryRepositiry.GetAll().ToList();
            return View(objCategoryList);
        }
        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (ModelState.IsValid) 
            {
                _categoryRepositiry.Add(obj);
                _categoryRepositiry.Save();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0) 
            {
                return NotFound();
            }
            Category? objCategory = _categoryRepositiry.GetFirstIdOrDefaul(u=>u.id==id);
            if (objCategory == null) 
            {
                return NotFound();
            }
            return View(objCategory);
        }
        [HttpPost]
        public IActionResult Edit(Category obj) 
        {
            if (ModelState.IsValid)
            {
                _categoryRepositiry.Update(obj);
                _categoryRepositiry.Save();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }


        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? objCategory = _categoryRepositiry.GetFirstIdOrDefaul(u => u.id == id);
            if (objCategory == null)
            {
                return NotFound();
            }
            return View(objCategory);
        }
        [HttpPost,ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Category? category = _categoryRepositiry.GetFirstIdOrDefaul(u => u.id == id);
            if (category == null) 
            {
                return NotFound();
            }
            _categoryRepositiry.Remove(category);
            _categoryRepositiry.Save();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
