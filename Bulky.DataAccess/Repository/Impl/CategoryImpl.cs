using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BulkyDataAccess.Data;
using BulkyDataAccess.Repository.Interf;
using BulkyModels.Category;

namespace BulkyDataAccess.Repository.Impl
{
    public class CategoryImpl : Repository<Category> , ICategoryRepository
    {
        private readonly ApplicationDbContext _db;
        public CategoryImpl(ApplicationDbContext db) : base(db) 
        {
            _db = db;
        }
        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Category obj)
        {
            _db.Categories.Update(obj);
        }
    }
}
