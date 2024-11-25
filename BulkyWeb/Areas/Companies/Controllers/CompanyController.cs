using BulkyDataAccess.Repository.Interf;
using BulkyModels.Models;
using BulkyUtility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Areas.Companies.Controllers
{
    [Area("Companies")]
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
            List<Company> Companies = _unitOfWork.CompanyRepo.GetAll().ToList();
            return View(Companies);
        }

        // GET: CompanyController/Details/5
        public IActionResult Details(int id)
        {
            return View();
        }

        // GET: CompanyController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CompanyController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Company obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.CompanyRepo.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Company created successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        // GET: CompanyController/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0 )
            {
                return NotFound();
            }
            Company? company = _unitOfWork.CompanyRepo.GetFirstIdOrDefault(u => u.Id == id);
            if (company == null) {
                return NotFound();
            }
            return View(company);
        }

        // POST: CompanyController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Company obj)
        {
            if (!ModelState.IsValid) {
                _unitOfWork.CompanyRepo.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Company updated successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        // GET: CompanyController/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Company? objCampony = _unitOfWork.CompanyRepo.GetFirstIdOrDefault(u => u.Id == id);
            if (objCampony == null)
            {
                return NotFound();
            }
            return View(objCampony);
        }

        // POST: CompanyController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            Company? company = _unitOfWork.CompanyRepo.GetFirstIdOrDefault(u => u.Id == id);
            if (company == null)
            {
                return NotFound();
            }
            _unitOfWork.CompanyRepo.Remove(company);
            _unitOfWork.Save();
            TempData["success"] = "Company deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
