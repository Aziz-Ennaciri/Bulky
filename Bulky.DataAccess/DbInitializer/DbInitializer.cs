using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BulkyDataAccess.Data;
using BulkyModels;
using BulkyUtility;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BulkyDataAccess.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _db;

        public DbInitializer(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext db)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _db = db;
        }
        public void Initialize()
        {
            //migrations if they are not applied

            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0) { 
                    _db.Database.Migrate();
                }
            }
            catch(Exception ex) { }

            //create roles if they are not created 


            if (!_roleManager.RoleExistsAsync(StaticDetails.Role_Customer).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(StaticDetails.Role_Customer)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(StaticDetails.Role_Company)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(StaticDetails.Role_Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(StaticDetails.Role_Employee)).GetAwaiter().GetResult();
            }
            //if roles are not created, then we will create admin user as well

            _userManager.CreateAsync(new ApplicationUser
            {
                 UserName = "admin@testing.com",
                 Email = "admin@testing.com",
                 Name ="Admin Test",
                 PhoneNumber = "1234567890",
                 StreetAddress = "Sale al jadida ",
                 State = "Rabat",
                 PostalCode = "12345",
                 City = "sale   "
            },"Test1234*").GetAwaiter().GetResult();

            ApplicationUser applicationUser = _db.applicationUsers.FirstOrDefault(u => u.Email == "admin@testing.com");
            _userManager.AddToRoleAsync(applicationUser, StaticDetails.Role_Admin).GetAwaiter().GetResult();
        }
    }
}
