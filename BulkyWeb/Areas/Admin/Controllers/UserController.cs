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
                
            var userRoles = _db.UserRoles.ToList();
            var roles = _db.Roles.ToList();


            foreach (var user in objUserList) 
            {
                var roleId =userRoles.FirstOrDefault(u=>u.UserId == user.Id).RoleId;
                user.Role = roles.FirstOrDefault(u => u.Id ==roleId).Name;
                    
                if (user.company == null) 
                {
                    user.company = new() { Name = ""};
                }
            }
                
            return Json(new { data = objUserList });
        }

        [HttpPost]
        public IActionResult LockUnlock([FromBody]string id)
        {
            var objFrmDb = _db.applicationUsers.FirstOrDefault(u => u.Id == id);
            if (objFrmDb == null)
            {
                return Json(new { success = false, message = "Error while locking / Unlocking" });
            }
            if (objFrmDb.LockoutEnd != null && objFrmDb.LockoutEnd>DateTime.Now) {
                //User is currently locked and we need to unlock them
                objFrmDb.LockoutEnd = DateTime.Now;
            }
            else
            {
                objFrmDb.LockoutEnd = DateTime.Now.AddYears(1000);
            }
            _db.SaveChanges();

            return Json(new { success = true, message = "Operation Successful" });
        }
        #endregion
         


        }
    } 

