using BulkyDataAccess.Data;
using BulkyDataAccess.Repository.Interf;
using BulkyModels.Category;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _unitOfWork.CategoryRepo.GetAll().ToList();
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
                _unitOfWork.CategoryRepo.Add(obj);
                _unitOfWork.Save();
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
            Category? objCategory = _unitOfWork.CategoryRepo.GetFirstIdOrDefaul(u => u.id == id);
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
                _unitOfWork.CategoryRepo.Update(obj);
                _unitOfWork.Save();
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
            Category? objCategory = _unitOfWork.CategoryRepo.GetFirstIdOrDefaul(u => u.id == id);
            if (objCategory == null)
            {
                return NotFound();
            }
            return View(objCategory);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Category? category = _unitOfWork.CategoryRepo.GetFirstIdOrDefaul(u => u.id == id);
            if (category == null)
            {
                return NotFound();
            }
            _unitOfWork.CategoryRepo.Remove(category);
            _unitOfWork.Save();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
