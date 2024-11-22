using System.Collections.Generic;
using BulkyDataAccess.Repository.Interf;
using BulkyModels.Models;
using BulkyModels.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork unitOfWork , IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        // GET: ProductController
        public ActionResult Index()
        {
            List<Product> objProductList = _unitOfWork.ProductRepo.GetAll().ToList();
            IEnumerable<SelectListItem> categoryList = _unitOfWork.CategoryRepo.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.id.ToString()
            });
            return View(objProductList);
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        
        //

        // GET: ProductController/Create
        //public IActionResult Create()
        //{
        //    //IEnumerable<SelectListItem> categoryList = _unitOfWork.CategoryRepo.GetAll().Select(u => new SelectListItem
        //    //{
        //    //    Text = u.Name,
        //    //    Value = u.id.ToString()
        //    //});
        //    //ViewBag.Category = categoryList;
        //    //return View();


        //    ProductVM productVM = new()
        //    {
        //        categoryList = _unitOfWork.CategoryRepo.GetAll().Select(u => new SelectListItem
        //        {
        //            Text = u.Name,
        //            Value = u.id.ToString()
        //        }),
        //        product = new Product()
        //    };
        //    return View(productVM);




        //}
        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new()
            {
                categoryList = _unitOfWork.CategoryRepo.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.id.ToString()
                }),
                product = new Product()
            };
            if(id == null || id == 0) 
            {
                //create
                return View(productVM);
            }
            else 
            {
                //update
                productVM.product = _unitOfWork.ProductRepo.GetFirstIdOrDefaul(u => u.Id == id);
                return View(productVM);
            }
            




        }

        // POST: ProductController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Create(ProductVM productVM)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _unitOfWork.ProductRepo.Add(productVM.product);
        //        _unitOfWork.Save();
        //        TempData["success"] = "Category created successfully";
        //        return RedirectToAction("Index");
        //    }
        //    else
        //    {
        //        productVM.categoryList = _unitOfWork.CategoryRepo.GetAll().Select(u => new SelectListItem
        //        {
        //            Text = u.Name,
        //            Value = u.id.ToString()
        //        });
        //            return View(productVM);
        //    }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM productVM,IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file == null)
                {
                    string fileName = Guid.NewGuid().ToString()+Path.GetExtension(file.FileName);
                    string productPath  = Path.Combine(wwwRootPath, @"images\product");

                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName),FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    productVM.product.imageUrl = @"\images\product\" + fileName;
                }


                _unitOfWork.ProductRepo.Add(productVM.product);
                _unitOfWork.Save();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            else
            {
                productVM.categoryList = _unitOfWork.CategoryRepo.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.id.ToString()
                });
                return View(productVM);
            }


        }


        

        //// GET: ProductController/Edit/5
        //public IActionResult Edit(int? id)
        //{
        //    if (id == null || id==0) 
        //    {
        //        return NotFound();
        //    }
        //    Product objProduct = _unitOfWork.ProductRepo.GetFirstIdOrDefaul(u=>u.Id == id);
        //    if (objProduct == null) 
        //    {
        //        return NotFound();
        //    }
        //    return View(objProduct);
        //}

        //// POST: ProductController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Edit(Product obj)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _unitOfWork.ProductRepo.Update(obj);
        //        _unitOfWork.Save();
        //        TempData["success"] = "Category updated successfully";
        //        return RedirectToAction("Index");
        //    }
        //    return View();
        //}

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
