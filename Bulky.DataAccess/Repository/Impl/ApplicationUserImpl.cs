using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BulkyDataAccess.Data;
using BulkyDataAccess.Repository.Interf;
using BulkyModels;

namespace BulkyDataAccess.Repository.Impl
{
    public class ApplicationUserImpl : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private readonly ApplicationDbContext _db;
        public ApplicationUserImpl(ApplicationDbContext db) : base(db) 
        {
            _db = db;
        }
    }
}
