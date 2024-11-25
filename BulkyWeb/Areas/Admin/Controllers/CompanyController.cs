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
    [Authorize(Roles = StaticDetails.Role_Company)]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: CompanyController
        public IActionResult Index()
        {
            List<Company> objCompanyList = _unitOfWork.CompanyRepo.GetAll().ToList();

            return View(objCompanyList);
        }

        // GET: CompanyController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //

        // GET: CompanyController/Create
        //public IActionResult Create()
        //{
        //    //IEnumerable<SelectListItem> categoryList = _unitOfWork.CategoryRepo.GetAll().Select(u => new SelectListItem
        //    //{
        //    //    Text = u.Name,
        //    //    Value = u.id.ToString()
        //    //});
        //    //ViewBag.Category = categoryList;
        //    //return View();


        //    CompanyVM CompanyVM = new()
        //    {
        //        categoryList = _unitOfWork.CategoryRepo.GetAll().Select(u => new SelectListItem
        //        {
        //            Text = u.Name,
        //            Value = u.id.ToString()
        //        }),
        //        Company = new Company()
        //    };
        //    return View(CompanyVM);




        //}
        public IActionResult Upsert(int? id)
        {
            // Check if it's an edit operation (id is provided and valid)
            if (id == null || id == 0)
            {
                return View(new Company());
            }
            else
            {
                Company company = _unitOfWork.CompanyRepo.GetFirstIdOrDefault(u => u.Id == id);
                return View(company);
            }

        }


        // POST: CompanyController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Create(CompanyVM CompanyVM)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _unitOfWork.CompanyRepo.Add(CompanyVM.Company);
        //        _unitOfWork.Save();
        //        TempData["success"] = "Category created successfully";
        //        return RedirectToAction("Index");
        //    }
        //    else
        //    {
        //        CompanyVM.categoryList = _unitOfWork.CategoryRepo.GetAll().Select(u => new SelectListItem
        //        {
        //            Text = u.Name,
        //            Value = u.id.ToString()
        //        });
        //            return View(CompanyVM);
        //    }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Company company)
        {
            if (ModelState.IsValid)
            {
                if (company.Id == 0)
                {
                    _unitOfWork.CompanyRepo.Add(company);
                }
                else
                {
                    _unitOfWork.CompanyRepo.Update(company);
                }
                _unitOfWork.Save();
                TempData["success"] = "Company created successfully";
                return RedirectToAction("Index");
            }

            else
            {
                return View(company);

            }
        }








            //// GET: CompanyController/Edit/5
            //public IActionResult Edit(int? id)
            //{
            //    if (id == null || id==0) 
            //    {
            //        return NotFound();
            //    }
            //    Company objCompany = _unitOfWork.CompanyRepo.GetFirstIdOrDefault(u=>u.Id == id);
            //    if (objCompany == null) 
            //    {
            //        return NotFound();
            //    }
            //    return View(objCompany);
            //}

            //// POST: CompanyController/Edit/5
            //[HttpPost]
            //[ValidateAntiForgeryToken]
            //public IActionResult Edit(Company obj)
            //{
            //    if (ModelState.IsValid)
            //    {
            //        _unitOfWork.CompanyRepo.Update(obj);
            //        _unitOfWork.Save();
            //        TempData["success"] = "Category updated successfully";
            //        return RedirectToAction("Index");
            //    }
            //    return View();
            //}

            // GET: CompanyController/Delete/5
            //public ActionResult Delete(int? id)
            //{
            //    if (id == null || id == 0)
            //    {
            //        return NotFound();
            //    }
            //    Company objCompany = _unitOfWork.CompanyRepo.GetFirstIdOrDefault(u => u.Id == id);
            //    if (objCompany == null)
            //    {
            //        return NotFound();
            //    }
            //    return View(objCompany);
            //}

            //// POST: CompanyController/Delete/5
            //[HttpPost, ActionName("Delete")]
            //[ValidateAntiForgeryToken]
            //public IActionResult DeletePost(int? id)
            //{
            //    Company objCompany = _unitOfWork.CompanyRepo.GetFirstIdOrDefault(u => u.Id == id);
            //    if (id == null || id == 0)
            //    {
            //        return NotFound();
            //    }
            //    _unitOfWork.CompanyRepo.Remove(objCompany);
            //    _unitOfWork.Save();
            //    TempData["success"] = "Category deleted successfully";
            //    return RedirectToAction("Index");
            //}




            #region API CALLS
            [HttpGet]
            public IActionResult GetAll()
            {
                List<Company> objCompanyList = _unitOfWork.CompanyRepo.GetAll().ToList();
                return Json(new { data = objCompanyList });
            }

            [HttpDelete]
            public IActionResult Delete(int? id)
            {

            Company? company = _unitOfWork.CompanyRepo.GetFirstIdOrDefault(u => u.Id == id);
            if (company == null)
            {
                return NotFound();
            }
            _unitOfWork.CompanyRepo.Remove(company);
            _unitOfWork.Save();

                return Json(new { success = true, message = "Delete Successful" });
            }
            #endregion



        }
    } 

