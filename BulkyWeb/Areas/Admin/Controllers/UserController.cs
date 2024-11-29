using System.Collections.Generic;
using BulkyDataAccess.Data;
using BulkyDataAccess.Repository.Interf;
using BulkyModels;
using BulkyModels.Models;
using BulkyModels.ViewModels;
using BulkyUtility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = StaticDetails.Role_Company)]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _db;
        public UserController(ApplicationDbContext db)
        {
            _db = db;
        }
        // GET: UserController
        public IActionResult Index()
        {

            return View();
        }


            #region API CALLS
            [HttpGet]
            public IActionResult GetAll()
            {
                List<ApplicationUser> objUserList = _db.applicationUsers.Include(u=>u.company).ToList();
                return Json(new { data = objUserList });
            }

            [HttpDelete]
            public IActionResult Delete(int? id)
            {
                return Json(new { success = true, message = "Delete Successful" });
            }
            #endregion



        }
    } 

