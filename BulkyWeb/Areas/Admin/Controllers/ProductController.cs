using System.Collections.Generic;
using BulkyDataAccess.Repository.Interf;
using BulkyModels.Models;
using BulkyModels.ViewModels;
using BulkyUtility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = StaticDetails.Role_Admin)]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        // GET: ProductController
        public IActionResult Index()
        {
            List<Product> objProductList = _unitOfWork.ProductRepo.GetAll(includeProperties: "Category").ToList();

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
            // Initialize the ProductVM object
            ProductVM productVM = new()
            {
                categoryList = _unitOfWork.CategoryRepo.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.id.ToString()
                }),
                product = new Product() // Create a new product instance by default
            };

            // Check if it's an edit operation (id is provided and valid)
            if (id != null && id > 0)
            {
                // Fetch the product using the provided ID
                productVM.product = _unitOfWork.ProductRepo.GetFirstIdOrDefault(u => u.Id == id);

                // Handle the case where the product isn't found
                if (productVM.product == null)
                {
                    return NotFound(); // Return a 404 response if the product doesn't exist
                }
            }

            return View(productVM); // Pass the view model to the view
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
        public IActionResult Upsert(ProductVM productVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string productPath = Path.Combine(wwwRootPath, @"images\product");

                // Handle file upload and replace image if a new file is provided
                if (file != null)
                {
                    // Generate a unique file name
                    string fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);

                    // Ensure directory exists
                    if (!Directory.Exists(productPath))
                    {
                        Directory.CreateDirectory(productPath);
                    }

                    // Remove old image if exists
                    if (!string.IsNullOrEmpty(productVM.product.imageUrl))
                    {
                        string oldImagePath = Path.Combine(wwwRootPath, productVM.product.imageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    // Save new image file
                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    // Update the imageUrl in the product model
                    productVM.product.imageUrl = @"\images\product\" + fileName;
                }

                // Add or update the product in the database
                if (productVM.product.Id == 0)
                {
                    _unitOfWork.ProductRepo.Add(productVM.product);
                    TempData["success"] = "Product created successfully.";
                }
                else
                {
                    _unitOfWork.ProductRepo.Update(productVM.product);
                    TempData["success"] = "Product updated successfully.";
                }

                // Save changes to the database
                _unitOfWork.Save();

                // Redirect to the product list
                return RedirectToAction("Index");
            }

            // Reload categories if validation fails
            productVM.categoryList = _unitOfWork.CategoryRepo.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.id.ToString()
            });

            return View(productVM);
        }








        //// GET: ProductController/Edit/5
        //public IActionResult Edit(int? id)
        //{
        //    if (id == null || id==0) 
        //    {
        //        return NotFound();
        //    }
        //    Product objProduct = _unitOfWork.ProductRepo.GetFirstIdOrDefault(u=>u.Id == id);
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
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }
        //    Product objProduct = _unitOfWork.ProductRepo.GetFirstIdOrDefault(u => u.Id == id);
        //    if (objProduct == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(objProduct);
        //}

        //// POST: ProductController/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public IActionResult DeletePost(int? id)
        //{
        //    Product objProduct = _unitOfWork.ProductRepo.GetFirstIdOrDefault(u => u.Id == id);
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }
        //    _unitOfWork.ProductRepo.Remove(objProduct);
        //    _unitOfWork.Save();
        //    TempData["success"] = "Category deleted successfully";
        //    return RedirectToAction("Index");
        //}




        #region API CALLS
        [HttpGet]
            public IActionResult GetAll()
            {
                List<Product> objProductList = _unitOfWork.ProductRepo.GetAll(includeProperties: "Category").ToList();
                return Json(new { data = objProductList });
            }

            [HttpDelete]
            public IActionResult Delete(int? id)
            {
                var productToBeDeleted = _unitOfWork.ProductRepo.GetFirstIdOrDefault(u => u.Id == id);
                if (productToBeDeleted == null)
                {
                    return Json(new { success = false, message = "Error while deleting" });
                }
                var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, productToBeDeleted.imageUrl.TrimStart('\\'));
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }

                _unitOfWork.ProductRepo.Remove(productToBeDeleted);
                _unitOfWork.Save();

                return Json(new { success = true, message = "Delete Successful" });
            }
            #endregion



        }
    } 
