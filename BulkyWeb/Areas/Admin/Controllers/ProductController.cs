using BulkyDataAccess.Repository.Interf;
using BulkyModels.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: ProductController
        public ActionResult Index()
        {
            List<Product> objProductList = _unitOfWork.ProductRepo.GetAll().ToList();
            return View(objProductList);
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid) 
            {
                _unitOfWork.ProductRepo.Add(product);
                _unitOfWork.Save();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: ProductController/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null || id==0) 
            {
                return NotFound();
            }
            Product objProduct = _unitOfWork.ProductRepo.GetFirstIdOrDefaul(u=>u.Id == id);
            if (objProduct == null) 
            {
                return NotFound();
            }
            return View(objProduct);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.ProductRepo.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product objProduct = _unitOfWork.ProductRepo.GetFirstIdOrDefaul(u => u.Id == id);
            if (objProduct == null)
            {
                return NotFound();
            }
            return View(objProduct);
        }

        // POST: ProductController/Delete/5
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int? id)
        {
            Product objProduct = _unitOfWork.ProductRepo.GetFirstIdOrDefaul(u=>u.Id == id);
            if(id == null || id == 0) 
            {
                return NotFound(); 
            }
            _unitOfWork.ProductRepo.Remove(objProduct);
            _unitOfWork.Save();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
